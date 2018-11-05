using FlightSeatArranger.Models.Enums;

namespace FlightSeatArranger.Models
{
    public class Seat
    {
        public int Row { get; set; } //front to rear
        public int Column { get; set; } //left to right
        public SeatType Type { get; set; }
        public int? Guest { get; set; }
    }
}
