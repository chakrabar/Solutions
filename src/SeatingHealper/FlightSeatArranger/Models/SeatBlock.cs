using System.Collections.Generic;

namespace FlightSeatArranger.Models
{
    public class SeatBlock
    {
        public int BlockIndex { get; set; } //left to right
        public int RowWidth { get; set; } //number of seat per row
        public int TotalRows { get; set; } //number of rows
        public IEnumerable<Seat> Seats { get; set; }
    }
}
