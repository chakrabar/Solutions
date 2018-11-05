using System.Collections.Generic;

namespace FlightSeatArranger.Models
{
    public class Flight
    {
        public string FlightId { get; set; }
        public int TotalSeats { get; set; }
        public IEnumerable<SeatBlock> SeatsBlocks { get; set; }
    }
}
