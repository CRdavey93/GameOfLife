using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class GameBoard
    {
        //Size of the game
        const int _width = 52;
        const int _height = 52;

        //A 2D array used to manipulate the visual board
        private static Organism[,] boardState = new Organism[_width, _height];

        public Organism[,] BoardState
        {
            get => boardState;
        }

        //Create the game board and populate it with our starting Organisms
        public void CreateBoard()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (InsideOfRectangle(x, y, _width, _height))
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("*");
                    }
                }
                Console.WriteLine();
            }
        }

        public void DrawOrganisms()
        {
            for (int y = 1; y < _height-1; y++)
            {
                for (int x = 1; x < _width-1; x++)
                {
                    Console.SetCursorPosition(x, y);
                    if (HasLiveOrganism(x, y))
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
            }
        }

        //Helper method to check if our current location is inside the game board
        private bool InsideOfRectangle(int x, int y, int width, int height)
        {
            var topAndLeft = y > 0 && x > 0;
            var bottomAndRight = y < height - 1 && x < width - 1;
            var inBoundaries = topAndLeft && bottomAndRight;

            return inBoundaries;
        }

        //Helper method to check if the given location has an organism or not
        private bool HasLiveOrganism(int x, int y)
        {
            if (boardState[x, y] == Organism.Live)
            {
                return true;
            }
            return false;
        }

        public void SetupBoardArray()
        {
            var inputCoords = GameOfLife.InputCoords;
            int firstCoord = 0;
            int secondCoord = 0;

            for (int i = 0; i < inputCoords.Count; i++)
            {
                var results = inputCoords[i].Split(',');
                Int32.TryParse(results[0], out firstCoord);
                Int32.TryParse(results[1], out secondCoord);
                boardState[firstCoord, secondCoord] = Organism.Live;

            }
        }
    }
}
