using FlightSeatArranger.AppCore;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    [TestFixture]
    public class FlightBuilderTests
    {
        [Test]
        public void Builder_should_build_complete_flight()
        {
            var flight = FlightBuilder.Build(new[,] { { 3, 2 }, { 3, 4 }, { 2, 3 } }, "FlighhtName");

            Assert.IsNotNull(flight);
            Assert.Equals(flight.SeatsBlocks.Count(), 3);
            Assert.Equals(flight.TotalSeats, 24);
        }
    }
}
