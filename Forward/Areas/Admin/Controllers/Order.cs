using Forward.Data.ViewModel;
using Forward.Models;
using Forward.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forward.Areas.Admin.Controllers
{
    [Authorize(Roles = WebSiteRole.Role_Admin + "," + WebSiteRole.Role_Employee1 + "," + WebSiteRole.Role_Employee2 + "," + WebSiteRole.Role_Employee3)]
    [Area("Admin")]
    public class Order : Controller
    {
        private IUnitOfWork _unitOfWork;
        public Order(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            if (HttpContext.User.IsInRole(WebSiteRole.Role_Admin))
            {
                OrderVM vm = new OrderVM()
                {
                    ListOfOrders = _unitOfWork.OrderHeader.GetAll(includeProperies: "Product")
                };
                return View(vm);
            }
            else if (HttpContext.User.IsInRole(WebSiteRole.Role_Employee1))
            {
                OrderVM vm = new OrderVM()
                {
                    ListOfOrders = _unitOfWork.OrderHeader.GetAll(x => x.OrderStatus == OrderStatus.Status_0, includeProperies: "Product")
                };
                return View(vm);
            }
            else if (HttpContext.User.IsInRole(WebSiteRole.Role_Employee2))
            {
                OrderVM vm = new OrderVM()
                {
                    ListOfOrders = _unitOfWork.OrderHeader.GetAll(x => x.OrderStatus == OrderStatus.Status_1, includeProperies: "Product")
                };
                return View(vm);
            }
            else if (HttpContext.User.IsInRole(WebSiteRole.Role_Employee3))
            {
                OrderVM vm = new OrderVM()
                {
                    ListOfOrders = _unitOfWork.OrderHeader.GetAll(x => x.OrderStatus == OrderStatus.Status_2, includeProperies: "Product")
                };
                return View(vm);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult OrderUser(string id)
        {
            if (HttpContext.User.IsInRole(WebSiteRole.Role_Admin))
            {
                OrderVM vm = new OrderVM()
                {
                    ListOfOrders = _unitOfWork.OrderHeader.GetAll(x => x.AppUserId == id, includeProperies: "Product"),
                    OrderHeader = _unitOfWork.OrderHeader.GetT(x => x.AppUserId == id, includeProperies: "AppUser")
                };
                if (vm.ListOfOrders.Count() == 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(vm);
                }
            }
            else if (HttpContext.User.IsInRole(WebSiteRole.Role_Employee1))
            {
                OrderVM vm = new OrderVM()
                {
                    ListOfOrders = _unitOfWork.OrderHeader.GetAll(x => x.OrderStatus == OrderStatus.Status_0 && x.AppUserId == id, includeProperies: "Product"),
                    OrderHeader = _unitOfWork.OrderHeader.GetT(x => x.AppUserId == id, includeProperies: "AppUser")
                };
                if (vm.ListOfOrders.Count() == 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(vm);
                }
            }
            else if (HttpContext.User.IsInRole(WebSiteRole.Role_Employee2))
            {
                OrderVM vm = new OrderVM()
                {
                    ListOfOrders = _unitOfWork.OrderHeader.GetAll(x => x.OrderStatus == OrderStatus.Status_1 && x.AppUserId == id, includeProperies: "Product"),
                    OrderHeader = _unitOfWork.OrderHeader.GetT(x => x.AppUserId == id, includeProperies: "AppUser")
                };
                if (vm.ListOfOrders.Count() == 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(vm);
                }
            }
            else if (HttpContext.User.IsInRole(WebSiteRole.Role_Employee3))
            {
                OrderVM vm = new OrderVM()
                {
                    ListOfOrders = _unitOfWork.OrderHeader.GetAll(x => x.OrderStatus == OrderStatus.Status_2 && x.AppUserId == id, includeProperies: "Product"),
                    OrderHeader = _unitOfWork.OrderHeader.GetT(x => x.AppUserId == id, includeProperies: "AppUser")
                };
                if (vm.ListOfOrders.Count() == 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(vm);
                }
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = WebSiteRole.Role_Employee1)]
        public IActionResult OrderSubmit1(int id)
        {
            var item = _unitOfWork.OrderHeader.GetT(x => x.Id == id);
            var item2 = _unitOfWork.OrderFinish.GetT(x => x.OrderId == id);

            item.OrderStatus = OrderStatus.Status_1;
            item2.OrderStatus = OrderStatus.Status_1;
            _unitOfWork.OrderFinish.Update(item2);
            _unitOfWork.save();
            _unitOfWork.OrderHeader.Update(item);
            _unitOfWork.save();
            return RedirectToAction(nameof(OrderUser), new { id = item.AppUserId });
        }
        [Authorize(Roles = WebSiteRole.Role_Employee2)]
        public IActionResult OrderSubmit2(int id)
        {
            var claims = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            var user = _unitOfWork.AppUser.GetT(x => x.Id == claims.Value);
            var item = _unitOfWork.OrderHeader.GetT(x => x.Id == id);
            var item2 = _unitOfWork.OrderFinish.GetT(x => x.OrderId == id);

            item.OrderStatus = OrderStatus.Status_2;
            item2.OrderStatus = OrderStatus.Status_2;
            _unitOfWork.OrderFinish.Update(item2);
            _unitOfWork.save();
            _unitOfWork.OrderHeader.Update(item);
            _unitOfWork.save();
            return RedirectToAction(nameof(OrderUser), new { id = item.AppUserId });
        }
        [Authorize(Roles = WebSiteRole.Role_Employee3)]
        public IActionResult OrderSubmit4(int id)
        {
            var claims = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            var user = _unitOfWork.AppUser.GetT(x => x.Id == claims.Value);
            var item = _unitOfWork.OrderHeader.GetT(x => x.Id == id);
            var item2 = _unitOfWork.OrderFinish.GetT(x => x.OrderId == id);

            item.OrderStatus = OrderStatus.Status_3;
            item2.OrderStatus = OrderStatus.Status_3;
            _unitOfWork.OrderFinish.Update(item2);
            _unitOfWork.save();
            _unitOfWork.OrderHeader.Delete(item);
            _unitOfWork.save();
            return RedirectToAction(nameof(OrderUser), new { id = item.AppUserId });
        }
        [Authorize(Roles = WebSiteRole.Role_Admin)]
        public IActionResult Delete(int id)
        {
            var claims = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            var user = _unitOfWork.AppUser.GetT(x => x.Id == claims.Value);
            var item = _unitOfWork.OrderHeader.GetT(x => x.Id == id);
            var item2 = _unitOfWork.OrderFinish.GetT(x => x.OrderId == id);

            item2.OrderStatus = OrderStatus.Status_5;
            _unitOfWork.OrderFinish.Update(item2);
            _unitOfWork.save();
            _unitOfWork.OrderHeader.Delete(item);
            _unitOfWork.save();
            return RedirectToAction(nameof(OrderUser), new { id = item.AppUserId });
        }
    }
}
