using Forward.Data.ViewModel;
using Forward.Models;
using Forward.Models.model;
using Forward.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Forward.Controllers
{
    [Area("Customer")]
    public class Home : Controller
    {
        private IUnitOfWork _unitOfWork;

        public Home(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(string? Category)
        {
            if (Category != null)
            {
                var itemsOrCategory = new ProductVM()
                {
                    Products = _unitOfWork.Product.GetAll(x => x.Category.Name == Category),
                    Category = _unitOfWork.Category.GetAll()
                };
                ViewData["Title"] = "Головна - " + Category;
                return View(itemsOrCategory);
            }
            var items = new ProductVM()
            {
                Products = _unitOfWork.Product.GetAll(includeProperies: "Category"),
                Category = _unitOfWork.Category.GetAll()
            };
            ViewData["Title"] = "Головна";
            return View(items);
        }
        [HttpGet]
        public IActionResult Details(int? ProductId)
        {
            if (ProductId == null)
            {
                return RedirectToAction(nameof(ErrorSearch));
            }
            else
            {
                if (ModelState.IsValid)
                {
                    Cart cart = new Cart()
                    {
                        Product = _unitOfWork.Product.GetT(x => x.Id == ProductId, includeProperies: "Category"),
                        Count = 1,
                        ProductId = (int)ProductId
                    };
                    if (cart.Product == null)
                    {
                        return RedirectToAction(nameof(ErrorSearch));
                    }
                    return View(cart);
                }
                else
                {
                    return RedirectToAction(nameof(ErrorSearch));
                }
            }
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Details(Cart cart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            cart.AppUserId = claims.Value;
            cart.Count = 1;

            var cartItem = _unitOfWork.Cart.GetT(x => x.ProductId == cart.ProductId && x.AppUserId == claims.Value);
            if (cartItem == null)
            {
                _unitOfWork.Cart.Add(cart);
                _unitOfWork.save();
            }
            return RedirectToAction(nameof(Index));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ErrorSearch()
        {
            return View();
        }
    }
}