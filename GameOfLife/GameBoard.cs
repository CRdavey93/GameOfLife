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

        //Determine the borders of the game
        const int _min_x = 1;
        const int _min_y = 1;
        const int _max_x = _width - 2;
        const int _max_y = _height - 2;

        public int MIN_X
        {
            get => _min_x;
        }

        public int MIN_Y
        {
            get => _min_y;
        }

        public int MAX_X
        {
            get => _max_x;
        }

        public int MAX_Y
        {
            get => _max_y;
        }

        //A 2D array used to manipulate the visual board
        private static Organism[,] boardState = new Organism[_width, _height];

        public Organism[,] BoardState
        {
            get => boardState;
        }

        //Create the game board
        public void CreateBoard()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (InsideOfRectangle(x, y))
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

        /*
         * Draw the Organisms to the board.
         */
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

        /* Helper method to check if our current location is inside the game board
         * @param x the current x coordinate
         * @param y the current y coordinate
         */
        private bool InsideOfRectangle(int x, int y)
        {
            var topAndLeft = y > 0 && x > 0;
            var bottomAndRight = y < _height - 1 && x < _width - 1;
            var inBoundaries = topAndLeft && bottomAndRight;

            return inBoundaries;
        }

        /*Helper method to check if the given location has an organism or not
         * @param x the current x coordinate
         * @param y the current y coordinate
         */
        private bool HasLiveOrganism(int x, int y)
        {
            if (boardState[x, y] == Organism.Live)
            {
                return true;
            }
            return false;
        }

        /*
         * Method used to setup the initial board array given the input coordinates.
         */
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
