using FlightSeatArranger.AppCore;
using FlightSeatArranger.AppCore.SeatAllocator;
using FlightSeatArranger.Visualizer;
using System;

namespace FlightSeatArranger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("    Welcome to flight seat arranger    ");
            var seats = InputReaders.GetSeatConfig();
            var guests = InputReaders.GetGuestCount();

            var flight = FlightBuilder.Build(seats, "CoolFlight");
            var waitingGuests = SeatingManager.ArrangeGuests(flight, guests);

            var printLines = SeatMapPrinter.PrintSeatingChart(flight);
            foreach (var line in printLines)
            {
                Console.WriteLine(line);
            }

            Console.ReadLine();
        }
    }
}
