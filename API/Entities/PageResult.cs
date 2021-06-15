using System.Collections.Generic;
using API.Entities.DataTransferObjects.Flights;

namespace API.Entities
{
    public class PageResult
    {
        public PageResult(List<FlightOutDto> items)
        {
            Page = 0;
            TotalItems = items.Count;
            Items = items;
        }

        public int Page { get; set; }
        public int TotalItems { get; set; }
        public List<FlightOutDto> Items { get; set; }
    }
}
