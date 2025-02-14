using System.ComponentModel.DataAnnotations;

namespace GentsOutfits_Authentication_.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Package detail is required")]
        [StringLength(200, ErrorMessage = "Package detail cannot be longer than 200 characters")]
        public string PakageDetail { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative integer")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Feature is required")]
        public string Feature { get; set; }

        public string Images { get; set; }
    }
}
