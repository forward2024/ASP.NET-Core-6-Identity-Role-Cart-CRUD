using Forward.Data.ViewModel;
using Forward.Models;
using Forward.Models.model;
using Forward.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forward.Areas.Customer.Controllers
{
    [Authorize]
    [Area("Customer")]
    public class Cart : Controller
    {
        private IUnitOfWork _unitOfWork;
        public CartVM vm { get; set; }
        public Cart(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claims = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);

            vm = new CartVM()
            {
                ListOfCart = _unitOfWork.Cart.GetAll(x => x.AppUserId == claims.Value, includeProperies: "Product"),
                OrderHeader = new OrderHeader()
            };

            foreach (var Item in vm.ListOfCart)
            {
                vm.OrderHeader.TotalPrice += (Item.Product.Price * Item.Count);
            }
            vm.OrderHeader.AppUser = _unitOfWork.AppUser.GetT(x => x.Id == claims.Value);
            vm.OrderHeader.Name = vm.OrderHeader.AppUser.Name;
            vm.OrderHeader.Phone = vm.OrderHeader.AppUser.Phone;
            vm.OrderHeader.Address = vm.OrderHeader.AppUser.Address;
            vm.OrderHeader.City = vm.OrderHeader.AppUser.City;
            vm.OrderHeader.Email = vm.OrderHeader.AppUser.Email;
            return View(vm);
        }

        public IActionResult plus(int id)
        {
            var cart = _unitOfWork.Cart.GetT(x => x.Id == id);
            _unitOfWork.Cart.Increment(cart, 1);
            _unitOfWork.save();
            return RedirectToAction("Index");
        }
        public IActionResult minus(int id)
        {
            var cart = _unitOfWork.Cart.GetT(x => x.Id == id);
            if (cart.Count <= 1)
            {
                _unitOfWork.Cart.Delete(cart);
            }
            else
            {
                _unitOfWork.Cart.Decrement(cart, 1);
            }
            _unitOfWork.save();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var cart = _unitOfWork.Cart.GetT(x => x.Id == id);
            _unitOfWork.Cart.Delete(cart);
            _unitOfWork.save();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Order(CartVM vm)
        {
            var claims = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);

            vm.ListOfCart = _unitOfWork.Cart.GetAll(x => x.AppUserId == claims.Value, includeProperies: "Product");
            vm.OrderHeader.DateTimeOfOrder = DateTime.Now;
            vm.OrderHeader.AppUserId = claims.Value;

            foreach (var item in vm.ListOfCart)
            {

                var order = new OrderHeader()
                {
                    AppUserId = claims.Value,
                    ProductId = item.Product.Id,
                    DateTimeOfOrder = DateTime.Now,
                    Name = vm.OrderHeader.Name,
                    Phone = vm.OrderHeader.Phone,
                    Address = vm.OrderHeader.Address,
                    City = vm.OrderHeader.City,
                    Email = vm.OrderHeader.Email,
                    TotalPrice = item.Count * item.Product.Price,
                    CountProduct = item.Count
                };
                _unitOfWork.OrderHeader.Add(order);
                _unitOfWork.save();
                var orderFinish = new OrderFinish()
                {
                    OrderId = order.Id,
                    AppUserId = claims.Value,
                    ProductId = item.Product.Id,
                    DateTimeOfOrder = DateTime.Now,
                    Name = vm.OrderHeader.Name,
                    Phone = vm.OrderHeader.Phone,
                    Address = vm.OrderHeader.Address,
                    City = vm.OrderHeader.City,
                    Email = vm.OrderHeader.Email,
                    TotalPrice = item.Count * item.Product.Price,
                    CountProduct = item.Count,
                    ProductName = item.Product.Name,
                    ProductImage = item.Product.ImageUrl
                };
                
                _unitOfWork.OrderFinish.Add(orderFinish);
                _unitOfWork.save();
                _unitOfWork.Cart.Delete(item);
                _unitOfWork.save();
            }

            return RedirectToAction("Index");
        }

        public IActionResult MyOrders()
        {
            var claims = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            OrderVM vm = new OrderVM()
            {
                orderFinishes = _unitOfWork.OrderFinish.GetAll(x => x.AppUserId == claims.Value)
            };
            return View(vm);
        }

        public IActionResult Cancel(int id)
        {
            var item = _unitOfWork.OrderFinish.GetT(x => x.Id == id);
            var item2 = _unitOfWork.OrderHeader.GetT(x => x.Id == item.OrderId);

            item.OrderStatus = OrderStatus.Status_4;
            _unitOfWork.OrderFinish.Update(item);
            _unitOfWork.save();
            _unitOfWork.OrderHeader.Delete(item2);
            _unitOfWork.save();

            return RedirectToAction(nameof(MyOrders));
        }
    }
}
