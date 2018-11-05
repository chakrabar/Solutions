using FlightSeatArranger.AppCore;
using FlightSeatArranger.Models.Enums;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    [TestFixture]
    public class SeatBlockBuilderTests
    {
        [Test]
        public void Builder_should_build_correct_block()
        {
            var seatBlock = SeatBlockBuilder.Build(3, 4, 2, 3);

            Assert.IsNotNull(seatBlock);
            Assert.Equals(seatBlock.BlockIndex, 2);
            Assert.Equals(seatBlock.RowWidth, 4);
            Assert.Equals(seatBlock.TotalRows, 3);

            Assert.IsNotNull(seatBlock.Seats);
            Assert.Equals(seatBlock.Seats.Count(s => s.Type == SeatType.Aisle), 3);
            Assert.Equals(seatBlock.Seats.Count(s => s.Type == SeatType.Window), 3);
            Assert.Equals(seatBlock.Seats.Count(s => s.Type == SeatType.Middle), 6);
        }
    }
}
