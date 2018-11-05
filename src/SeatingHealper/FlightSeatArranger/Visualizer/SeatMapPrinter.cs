using FlightSeatArranger.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightSeatArranger.Visualizer
{
    public class SeatMapPrinter
    {
        public static IEnumerable<string> PrintSeatingChart(Flight flight)
        {
            var allPrintedRows = new List<string>();
            var maxBlockLength = flight.SeatsBlocks.Max(sb => sb.TotalRows);

            for (int i = 0; i < maxBlockLength; i++)
            {
                var rowNumber = i + 1;
                var printedRow = new StringBuilder();
                foreach (var block in flight.SeatsBlocks)
                {
                    var printedCurrentBlockRow = block.TotalRows < rowNumber
                        ? PrintEmptyBlockRow(block.RowWidth)
                        : PrintBlockRow(block.Seats.Where(s => s.Row == i));

                    printedRow.Append(printedCurrentBlockRow + " ");
                }
                allPrintedRows.Add(printedRow.ToString().TrimEnd());
            }

            return allPrintedRows;
        }

        public static string PrintBlockRow(IEnumerable<Seat> seats)
        {
            var sb = new StringBuilder();
            foreach (var seat in seats)
            {
                var guestLiteral = seat.Guest == null ? 
                    "XX" : seat.Guest.ToString().PadLeft(2, '0');
                sb.Append($" {guestLiteral} ");
            }
            return $"[{sb.ToString().Trim()}]";
        }

        public static string PrintEmptyBlockRow(int blockWidth)
        {
            var sb = new StringBuilder();
            var seats = Enumerable.Range(1, blockWidth).Select(s => "  ");
            return $"  {string.Join(' ', seats)}  ";
        }
    }
}
