using FlightSeatArranger.Models;
using System;

namespace FlightSeatArranger.AppCore.SeatAllocator
{
    public class SeatingManager
    {
        public static int ArrangeGuests(Flight flight, int guests)
        {
            var guestId = 1;
            while (guestId <= guests && guestId <= flight.TotalSeats)
            {
                var allocatedSeat = GuestSeatAllocator.AllocateBestSeat(flight.SeatsBlocks);
                if (allocatedSeat == null)
                    throw new ApplicationException("Application encountered a problem!");
                allocatedSeat.Guest = guestId;
                guestId++;
            }

            return guests - (guestId - 1);
        }
    }
}
