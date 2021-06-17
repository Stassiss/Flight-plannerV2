using System.ComponentModel.DataAnnotations;

namespace API.Entities.Models
{
    public class Airport
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public string City { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public string AirportName { get; set; }
    }
}
