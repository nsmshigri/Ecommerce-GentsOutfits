using System.ComponentModel.DataAnnotations;

namespace GentsOutfits_Authentication_.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }  // No validation needed

        public string PlacedAt { get; set; }  // No validation needed

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(50, ErrorMessage = "City cannot be longer than 50 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        [RegularExpression("^(?!0+$)\\d+$", ErrorMessage = "Postal Code must be a positive numeric value greater than zero.")]
        public string PostalCode { get; set; }

        public string Country { get; set; }  // No validation needed
    }
}
