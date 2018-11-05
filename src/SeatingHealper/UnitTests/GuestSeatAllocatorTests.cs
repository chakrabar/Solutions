using FlightSeatArranger.AppCore.SeatAllocator;
using FlightSeatArranger.Models;
using FlightSeatArranger.Models.Enums;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTests
{
    [TestFixture]
    public class GuestSeatAllocatorTests
    {
        [Test]
        public void Should_get_right_seat()
        {
            var seats = new List<Seat>
            {
                new Seat { Column = 0, Row = 0, Type = SeatType.Window, Guest = null },
                new Seat { Column = 1, Row = 0, Type = SeatType.Middle, Guest = null },
                new Seat { Column = 2, Row = 0, Type = SeatType.Aisle, Guest = 2 },
                new Seat { Column = 0, Row = 1, Type = SeatType.Window, Guest = null },
                new Seat { Column = 1, Row = 1, Type = SeatType.Middle, Guest = null },
                new Seat { Column = 2, Row = 1, Type = SeatType.Aisle, Guest = null }
            };

            var seatBlock = new SeatBlock
            {
                BlockIndex = 0,
                RowWidth = 3,
                TotalRows = 2,
                Seats = seats
            };

            var seat = GuestSeatAllocator.AllocateBestSeat(new[] { seatBlock });

            Assert.IsNotNull(seat);
            Assert.Equals(seat.Row, 1);
            Assert.Equals(seat.Column, 2);
        }
    }
}
