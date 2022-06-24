using Forward.Models.model;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Forward.Data.ViewModel
{
    public class ProductVM
    {
        public Product Product { get; set; } = new Product();
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public IEnumerable<Category> Category { get; set; } = new List<Category>();
        [ValidateNever]
        public IEnumerable<SelectListItem>? Categories { get; set; }
    }
}
