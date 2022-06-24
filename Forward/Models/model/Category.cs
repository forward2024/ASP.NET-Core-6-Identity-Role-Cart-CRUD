using System.ComponentModel.DataAnnotations;

namespace Forward.Models.model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
