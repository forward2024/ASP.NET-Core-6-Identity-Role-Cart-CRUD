using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Forward.Models.model
{
    public class Cart
    {
        public int Id { get; set; }
        [RegularExpression("0-9")]
        public int ProductId { get; set; }
        [ValidateNever]
        public Product Product { get; set; }
        [ValidateNever]
        public string AppUserId { get; set; }
        [ValidateNever]
        public AppUser AppUser { get; set; }
        public int Count { get; set; }
    }
}
