using FlightSeatArranger.Models;
using System.Linq;

namespace FlightSeatArranger.AppCore
{
    public class FlightBuilder
    {
        public static Flight Build(int[,] seatDataInput, string flightId = null)
        {
            var allSeatBlocks = FlightSeatPlanner.GetSeatsPlan(seatDataInput);
            return new Flight
            {
                SeatsBlocks = allSeatBlocks,
                FlightId = flightId,
                TotalSeats = allSeatBlocks.Sum(sb => sb.TotalRows * sb.RowWidth)
            };
        }
    }
}
