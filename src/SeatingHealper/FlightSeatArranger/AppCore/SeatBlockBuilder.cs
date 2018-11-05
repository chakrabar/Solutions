using FlightSeatArranger.Models;
using FlightSeatArranger.Models.Enums;
using System.Collections.Generic;

namespace FlightSeatArranger.AppCore
{
    public class SeatBlockBuilder
    {
        public static SeatBlock Build(int rows, int columns, int blockIndex, int totalBlocks)
        {
            var block = new SeatBlock
            {
                TotalRows = rows,
                RowWidth = columns,
                BlockIndex = blockIndex
            };

            var seats = new List<Seat>();
            for (int i = 0; i < block.TotalRows; i++) //i = row
            {
                for (int j = 0; j < block.RowWidth; j++) //j = column
                {
                    seats.Add(new Seat
                    {
                        Row = i,
                        Column = j,
                        Type = GetSeatType(j, columns, blockIndex, totalBlocks)
                    });
                }
            }

            block.Seats = seats;
            return block;
        }

        public static SeatType GetSeatType(int column, int rowWidth, int blockIndex, int totalBlocks)
        {
            if ((column == 0 && blockIndex == 0)
                || ((blockIndex == totalBlocks - 1) && (column == rowWidth - 1)))
                return SeatType.Window;

            if (column == 0 || column == rowWidth - 1)
                return SeatType.Aisle;

            return SeatType.Middle;
        }
    }
}
