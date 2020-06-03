using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GameOfLife
{
    public class GameOfLife
    {

        private static List<string> inputCoords = new List<string>();
        private static int numOfGenerations;
        private GameBoard newBoard;

        public static List<string> InputCoords
        {
            get => inputCoords;
        }

        /*
         * Runs the game when called
         */                                                                         
        public void Run()
        {
            GetUserInput();
            newBoard = new GameBoard();
            newBoard.SetupBoardArray();
            PlayGame();
            Console.WriteLine();
        }
        
        /*
         * Plays the game until the number of given generations has been played through
         */
        private void PlayGame()
        {
            Console.Clear();
            Console.CursorVisible = false;
            newBoard.CreateBoard();
            do
            {
               newBoard.DrawOrganisms();
                SetupNextGen();
                numOfGenerations--;
            } while (numOfGenerations > 0);
        }

        /*
         * Gets the starting coordinates and number of desired generations from the player
         */
        private static void GetUserInput()
        {
            Console.Write("Input coordinates for where you would like your organisms to start - format (x,y): ");
            var userSetCoords = Console.ReadLine();
            SetStartingCoords(userSetCoords);
            Console.Write("How many generations would you like the game to play?:");
            numOfGenerations = Convert.ToInt32(Console.ReadLine());
        }

        /*
         * Sets up the board array for the next generation by iterating through the array and checking the neighbors organism status.
         * The boards organisms are first set to a temporary status so each organism for the next gen can be calculated before changes are actually made.
         * We then iterate through the board again, and set all of the temporary organisms to their final status for that generation.
         */
        private void SetupNextGen()
        {
            int count;
            Organism currOrganism;
            var currBoard = newBoard.BoardState;
            for (int y = 1; y < currBoard.GetLength(1) - 1; y++)
            {
                for (int x = 1; x < currBoard.GetLength(0) - 1; x++)
                {
                    currOrganism = currBoard[x, y];
                    count = 0;
                    count = CheckNeighbors(x, y, currBoard);

                    currBoard[x, y] = SetTempOrganismStatus(count, currOrganism);
                }
            }
            for (int y = 1; y < currBoard.GetLength(1) - 1; y++)
            {
                for (int x = 1; x < currBoard.GetLength(0) - 1; x++)
                {
                    currOrganism = currBoard[x, y];

                    currBoard[x, y] = SetFinalOrganismStatus(currOrganism);
                }
            }

        }

        /* Helper method to check the surrounding organisms in the board array and determine the number of living neighbors
         * @param currXPos is the current x coordinate that we are checking
         * @param currYPos is the current y coordinate that we are checking
         * @param currBoard is the array that we working on
         */
        private int CheckNeighbors(int currXPos, int currYPos, Organism[,]currBoard)
        {
            int count = 0;
            int startPosX = (currXPos - 1 < newBoard.MIN_X) ? currXPos : currXPos - 1;
            int startPosY = (currYPos - 1 < newBoard.MIN_Y) ? currYPos : currYPos - 1;
            int endPosX = (currXPos + 1 > newBoard.MAX_X) ? currXPos : currXPos + 1;
            int endPosY = (currYPos + 1 > newBoard.MAX_Y) ? currYPos : currYPos + 1;

            for (int rowNum = startPosX; rowNum <= endPosX; rowNum++)
            {
                for (int colNum = startPosY; colNum <= endPosY; colNum++)
                {
                    if (rowNum == currXPos && colNum == currYPos)
                    {
                        continue;
                    }
                    else
                    {
                        if (currBoard[rowNum, colNum] == Organism.Live || currBoard[rowNum, colNum] == Organism.Dying)
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        /*Helper method to set the Organisms to their temporary status based on its current status and the status of its neighbors.
         * We use a temporary status so that the next organism calculates its next state based on the current gen status of each neighbor
         * @param count is the number of living neighbors that the current organism has
         * @param currOrganism is the current status of the organism that we are checking
         */
        private Organism SetTempOrganismStatus(int count, Organism currOrganism)
        {
                    if (currOrganism == Organism.Live)
                    {
                        if (count < 2)
                        {
                            currOrganism = Organism.Dying;
                        }
                        else if (count > 3)
                        {
                            currOrganism = Organism.Dying;
                        }
                        else
                        {
                            return currOrganism;
                        }
                    }
                    else if (currOrganism == Organism.Dead)
                    {
                        if (count == 3)
                        {
                            currOrganism = Organism.Gestating;
                        }
                    }

            return currOrganism;
        }

        /*Helper method to set the final status of each organism from their temporary status to their final status
         * @param currOrganism is the current status of the organism we are checking
         */
        private Organism SetFinalOrganismStatus(Organism currOrganism)
        {
            if (currOrganism == Organism.Gestating)
            {
                currOrganism = Organism.Live;
            }
            else if (currOrganism == Organism.Dying)
            {
                currOrganism = Organism.Dead;
            }

            return currOrganism;
        }

        /* Helper method to take the user input coordinates and turn them into a more useable format.
         * 
         */
        private static void SetStartingCoords(string userInput)
        {
            inputCoords = userInput.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            for (int i = 0; i < inputCoords.Count; i++)
            {
                inputCoords[i] = inputCoords[i].Replace("(", string.Empty).Replace(")", string.Empty);
                inputCoords[i].Trim();
            }
        }
    }
}
