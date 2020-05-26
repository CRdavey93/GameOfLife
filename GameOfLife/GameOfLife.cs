using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class GameOfLife
    {
        enum Organism { live, dead, gestating, dying};

        public void Play()
        {
            GetUserInput();
        }
        private static void GetUserInput()
        {
            Console.Write("Please input coordinates for where you would like your organisms to start: ");
            var a = Console.ReadLine();
            Console.WriteLine(a);
        }
    }
}
