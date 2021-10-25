using System;
using System.IO;
using MathsProblem;

namespace MathsProblemGenerator
{
    class Program
    {
        static int ReadValue(int defaultVal)
        {
            try 
            { 
                return int.Parse(Console.ReadLine());
            }
            catch( Exception )
            {
                Console.WriteLine($"Using default value:{defaultVal}");
                return defaultVal;
            }
        }

        static void AskABAnswerValues( out int qMin, out int qMax, out int aMin, out int aMax)
        {
            Console.WriteLine("Enter 'a' and 'b' minimum value");
            qMin = ReadValue(-9);
            Console.WriteLine("Enter 'a' and 'b' max value");
            qMax = ReadValue(9);
            Console.WriteLine("Enter answer min value");
            aMin = ReadValue(qMin);
            Console.WriteLine("Enter answer max value");
            aMax = ReadValue(qMax);
        }

        static bool AskGenerateCsv( ) 
        {
            Console.WriteLine("Do you want to create a comma separated value file? y / n");
            var key = Console.ReadKey().KeyChar;
            Console.WriteLine("");
            return key == 'y' || key == 'Y';
        }

        static bool AskCsv(out int csvX, out int csvY)
        {
            csvX = csvY = 0;
            if (!AskGenerateCsv()) 
                return false;
            Console.WriteLine("Enter the number of questions wide");
            csvX = ReadValue(1);
            if (csvX <= 0)
                csvX = 1;
            Console.WriteLine("Enter the number of questions high");
            csvY = ReadValue(1);
            if (csvY <= 0)
                csvY = 1;
            return true;
        }

        static void RunAdHocQuestions(NegPosAddition negPosAddition)
        {
            char key = ' ';
            while (key != 'q')
            {
                Console.WriteLine("\nEnter to show answer");
                negPosAddition.GenerateNextProblem(out var question, out var answer);
                Console.WriteLine(string.Join(" ", question));

                key = Console.ReadKey().KeyChar;
                Console.WriteLine(string.Join(" ", answer));
                Console.WriteLine("Press Q to quit - Enter to generate next problem");
                key = Console.ReadKey().KeyChar;
            }
        }

        private static void RunCsvQuestions(NegPosAddition negPosAddition, int csvX, int csvY)
        {
            var questionFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "MathsProblemGenerator.csv");
            Console.WriteLine($"Writing question CSV file to:{questionFilePath}");
            var answerFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "MathsProblemGeneratorAnswers.csv");
            Console.WriteLine($"Writing answer CSV file to:{answerFilePath}");

            using (var questionCsv = new StreamWriter(questionFilePath))
            using (var answerCsv = new StreamWriter(answerFilePath))
            {
                for (var yLoop = 0; yLoop < csvY; ++yLoop)
                {
                    for (var xLoop = 0; xLoop < csvX; ++xLoop)
                    {
                        negPosAddition.GenerateNextProblem(out var quesion, out var answer);
                        questionCsv.Write($"{string.Join(",", quesion)},");
                        answerCsv.Write($"{string.Join(",", answer)},");

                        if (xLoop + 1 < csvX)
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

        static void Main(string[] args)
        {
            Console.WriteLine("Format is a + b = answer");
            AskABAnswerValues(out var qMin, out var qMax, out var aMin, out var aMax);
            var negPosAddition = new NegPosAddition(qMin, qMax, aMin, aMax);

            if( AskCsv(out var csvX, out var csvY) )
            {
                RunCsvQuestions(negPosAddition, csvX, csvY);
                return;
            }

            RunAdHocQuestions(negPosAddition);
        }

    }
}
