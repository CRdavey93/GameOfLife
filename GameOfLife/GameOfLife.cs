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

        public void Run()
        {
            GetUserInput();
            newBoard = new GameBoard();
            newBoard.SetupBoardArray();
            PlayGame();
        }
        
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

        private static void GetUserInput()
        {
            Console.Write("Input coordinates for where you would like your organisms to start - format (x,x): ");
            var userSetCoords = Console.ReadLine();
            SetStartingCoords(userSetCoords);
            Console.WriteLine(inputCoords[1]);
            Console.Write("How many generations would you like the game to play?:");
            numOfGenerations = Convert.ToInt32(Console.ReadLine());
        }

        private void SetupNextGen()
        {
            var currentBoard = newBoard.BoardState;

        }

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
