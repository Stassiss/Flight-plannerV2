using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities.DataTransferObjects.Airports
{
    public abstract class AirportDtoBase
    {
        [Required(ErrorMessage = "Required field!")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public string City { get; set; }

        [Required(ErrorMessage = "Required field!")]
        [JsonPropertyName("airport")]
        public string AirportName { get; set; }
    }
}
