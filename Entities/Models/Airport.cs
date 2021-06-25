using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Airport : Entity
    {
        [Required(ErrorMessage = "Required field!")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public string City { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public string AirportName { get; set; }
    }
}
