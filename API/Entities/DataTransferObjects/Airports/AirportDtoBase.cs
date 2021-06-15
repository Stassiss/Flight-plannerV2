using System.Text.Json.Serialization;

namespace API.Entities.DataTransferObjects.Airports
{
    public abstract class AirportDtoBase
    {
        public string Country { get; set; }
        public string City { get; set; }

        [JsonPropertyName("airport")]
        public string AirportName { get; set; }
    }
}
