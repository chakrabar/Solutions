using FlightSeatArranger.Models;
using System;
using System.Collections.Generic;

namespace FlightSeatArranger.AppCore
{
    public class FlightSeatPlanner
    {
        public static IEnumerable<SeatBlock> GetSeatsPlan(int[,] seatDataInput)
        {
            ValidateInput(seatDataInput);

            var totalSeatBlocks = seatDataInput.GetLength(0);
            var allSeatBlocks = new List<SeatBlock>();
            var seatBlockIndex = 0;

            //each seat block
            for (int i = 0; i < totalSeatBlocks; i++)
            {
                var columns = seatDataInput[i, 0];
                var rows = seatDataInput[i, 1];
                allSeatBlocks
                    .Add(
                        SeatBlockBuilder.Build(rows, columns, seatBlockIndex, totalSeatBlocks)
                    );
                seatBlockIndex++;
            }

            return allSeatBlocks;
        }

        private static void ValidateInput(int[,] seatDataInput)
        {
            if (seatDataInput == null || seatDataInput.GetLength(1) != 2)
                throw new ArrayTypeMismatchException("Seat data was null");
            if (seatDataInput.GetLength(1) != 2)
                throw new ArrayTypeMismatchException("Seat data was invalid");
        }
    }
}
