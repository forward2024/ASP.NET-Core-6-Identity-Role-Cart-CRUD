using Forward.Data.ViewModel;
using Forward.Models;
using Forward.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Forward.Areas.Admin.Controllers
{
    [Authorize(Roles = WebSiteRole.Role_Admin)]
    [Area("Admin")]
    public class Product : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _hostEnvironment;

        public Product(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index(int id)
        {
            if (id == null)
            {
                ProductVM product = new ProductVM();
                product.Products = _unitOfWork.Product.GetAll(includeProperies: "Category");
                return View(product);
            }
            else
            {
                ProductVM product = new ProductVM();
                product.Product = _unitOfWork.Product.GetT(x => x.Id == id);
                if (product.Products.Count() != 0)
                {
                    return View(product);
                }
                else
                {
                    product.Products = _unitOfWork.Product.GetAll(includeProperies: "Category");
                    return View(product);
                }
            }
        }

        [HttpGet]
        public IActionResult CreateUpdate(int id)
        {
            ProductVM vm = new ProductVM()
            {
                Product = new(),
                Categories = _unitOfWork.Category.GetAll().Select(x =>
                new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Product = _unitOfWork.Product.GetT(x => x.Id == id);
                if (vm.Product == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(vm);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(ProductVM vm, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string fileName = string.Empty;
                if (file != null)
                {
                    string uploadir = Path.Combine(_hostEnvironment.WebRootPath, "ProductImage");
                    fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                    string FilePath = Path.Combine(uploadir, fileName);

                    if (vm.Product.ImageUrl != null)
                    {
                        var ImagePath = Path.Combine(_hostEnvironment.WebRootPath, vm.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(ImagePath))
                        {
                            System.IO.File.Delete(ImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(FilePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    vm.Product.ImageUrl = @"\ProductImage\" + fileName;
                }
                if (vm.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(vm.Product);
                    TempData["Success"] = "Product created Done!";
                }
                else
                {
                    _unitOfWork.Product.Update(vm.Product);
                    TempData["Success"] = "Product Update Done!";
                }
                _unitOfWork.save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var product = _unitOfWork.Product.GetT(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Delete(product);
            _unitOfWork.save();
            TempData["success"] = "Product Delete!";
            return RedirectToAction("Index");
        }
    }
}
