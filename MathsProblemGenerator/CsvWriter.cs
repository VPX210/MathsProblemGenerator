using MathsProblem;
using System;
using System.IO;

namespace MathsProblemGenerator
{
    public class CsvWriter : IWriter
    {
        public string Description => "Write to a comma separated list file";

        public int MaxNumX => 1000;

        public int MaxNumY => 1000;

        public int NumX { get; set; }
        public int NumY { get; set; }

        public void Run(IMathsProblem problemGenerator)
        {
            var questionFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "MathsProblemGenerator.csv");
            Console.WriteLine($"Writing question CSV file to:{questionFilePath}");
            var answerFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "MathsProblemGeneratorAnswers.csv");
            Console.WriteLine($"Writing answer CSV file to:{answerFilePath}");

            using (var questionCsv = new StreamWriter(questionFilePath))
            using (var answerCsv = new StreamWriter(answerFilePath))
            {
                for (var yLoop = 0; yLoop < NumY; ++yLoop)
                {
                    for (var xLoop = 0; xLoop < NumX; ++xLoop)
                    {
                        problemGenerator.GenerateNextProblem(out var quesion, out var answer);
                        questionCsv.Write($"{string.Join(",", quesion)},");
                        answerCsv.Write($"{string.Join(",", answer)},");

                        if (xLoop + 1 < NumX)
                        {
                            questionCsv.Write(" ,");
                            answerCsv.Write(" ,");
                        }
                    }
                    questionCsv.WriteLine("");
                    answerCsv.WriteLine("");
                }
            }
        }
    }
}
