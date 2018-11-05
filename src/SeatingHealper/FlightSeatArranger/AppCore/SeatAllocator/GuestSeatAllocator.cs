using FlightSeatArranger.Models;
using FlightSeatArranger.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightSeatArranger.AppCore.SeatAllocator
{
    public class GuestSeatAllocator
    {
        //allocates block by block
        public static Seat AllocateBestSeat(IEnumerable<SeatBlock> allSeats)
        {
            var seatFinders = new List<Func<IEnumerable<Seat>, Seat>>
            {
                FindAisleSeat,
                FindWindowSeat,
                FindMiddleSeat
            };

            var maxRowLength = allSeats.Max(block => block.TotalRows);

            foreach (var finder in seatFinders)
            {
                for (int i = 0; i < maxRowLength; i++) //per row
                {
                    foreach (var block in allSeats.Where(b => b.TotalRows - 1 >= i))
                    {
                        //get all seats in current row
                        //from each available blocks
                        //from left to right (order by column)
                        var currentBlockRow = block.Seats
                            .Where(s => s.Row == i)
                            .OrderBy(s1 => s1.Column);
                        var allocatedSeat = finder(currentBlockRow);
                        if (allocatedSeat != null)
                            return allocatedSeat;
                    }
                }
            }

            return null;
        }

        public static Seat FindAisleSeat(IEnumerable<Seat> seats)
        {
            return FindFirstAvailableSeat(seats, SeatType.Aisle);
        }

        public static Seat FindWindowSeat(IEnumerable<Seat> seats)
        {
            return FindFirstAvailableSeat(seats, SeatType.Window);
        }

        public static Seat FindMiddleSeat(IEnumerable<Seat> seats)
        {
            return FindFirstAvailableSeat(seats, SeatType.Middle);
        }

        public static Seat FindFirstAvailableSeat(IEnumerable<Seat> seats, SeatType seatType)
        {
            //seats are arranged front to back, left to right
            return seats
                .FirstOrDefault(s => s.Type == seatType
                    && s.Guest == null);
        }
    }
}
