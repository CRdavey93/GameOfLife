using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class GameBoard
    {
        public void CreateBoard(int width, int height)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write((InsideOfRectangle(x, y, width, height)) ? " " : "*");
                }
                Console.WriteLine();
            }
        }

        private bool InsideOfRectangle(int x, int y, int width, int height)
        {
            var topAndLeft = y > 0 && x > 0;
            var bottomAndRight = y < height - 1 && x < width - 1;
            var inBoundaries = topAndLeft && bottomAndRight;

            return inBoundaries;
        }

    }
}
