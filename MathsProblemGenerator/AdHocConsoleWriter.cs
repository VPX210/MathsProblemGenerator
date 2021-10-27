using MathsProblem;
using System;

namespace MathsProblemGenerator
{
    public class AdHocConsoleWriter : IWriter
    {
        public string Description => "Write to the console";

        public int MaxNumX => 1;

        public int MaxNumY => 1;

        public int NumX { get; set; }
        public int NumY { get; set; }

        public void Run(IMathsProblem problemGenerator)
        {
            char key = ' ';
            while (key != 'q')
            {
                Console.WriteLine("\nEnter to show answer");
                problemGenerator.GenerateNextProblem(out var question, out var answer);
                Console.WriteLine(string.Join(" ", question));

                key = Console.ReadKey().KeyChar;
                Console.WriteLine(string.Join(" ", answer));
                Console.WriteLine("Press Q to quit - Enter to generate next problem");
                key = Console.ReadKey().KeyChar;
            }
        }
    }
}
