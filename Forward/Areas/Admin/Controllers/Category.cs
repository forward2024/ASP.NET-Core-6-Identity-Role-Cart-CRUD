using Forward.Data.ViewModel;
using Forward.Models;
using Forward.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forward.Areas.Admin.Controllers
{
    [Authorize(Roles = WebSiteRole.Role_Admin)]
    [Area("Admin")]
    public class Category : Controller
    {
        private IUnitOfWork _unitOfWork;

        public Category(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            CategoryVM categoryVM = new CategoryVM();
            categoryVM.categories = _unitOfWork.Category.GetAll();
            return View(categoryVM);
        }

        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            CategoryVM vm = new CategoryVM();
            if (id == null)
            {
                return View(vm);
            }
            else
            {
                vm.category = _unitOfWork.Category.GetT(x => x.Id == id);
                if (vm.category == null)
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
        public IActionResult CreateUpdate(CategoryVM vm)
        {
            if (vm.category.Id != 0)
            {
                _unitOfWork.Category.Update(vm.category);
            }
            else
            {
                _unitOfWork.Category.Add(vm.category);
            }

            _unitOfWork.save();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            var category = _unitOfWork.Category.GetT(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Delete(category);
            _unitOfWork.save();
            TempData["success"] = "Category Delete!";
            return RedirectToAction("Index");
        }
    }
}
