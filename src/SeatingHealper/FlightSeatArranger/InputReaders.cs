using System;
using System.Linq;

namespace FlightSeatArranger
{
    public class InputReaders
    {
        public static int[,] GetSeatConfig()
        {
            Console.WriteLine("Enter seat configuration in following form : ");
            Console.WriteLine("[ [ 3, 2 ], [ 4, 3 ], [ 2, 3 ], [ 3, 4 ] ]");
            Console.WriteLine("Spaces are optional, comma & bracket required...");
            string data = Console.ReadLine();

            data = data.Replace(" ", "");
            var parts = data.Split("],[").Select(p => p.Replace("[", "").Replace("]", ""));
            var config = parts.Select(p => p.Split(',').Select(c => int.Parse(c)));
            var seats = new int[config.Count(), 2];
            for (int i = 0; i < config.Count(); i++)
            {
                var arr = config.ElementAt(i);
                for (int j = 0; j < 2; j++)
                {
                    seats[i, j] = arr.ElementAt(j);
                }
            }

            return seats;
        }

        public static int GetGuestCount()
        {
            Console.WriteLine("Enter number of guests...");
            string guestCount = Console.ReadLine();
            var guests = int.Parse(guestCount.Trim());
            return guests;
        }
    }
}
