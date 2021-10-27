using MathsProblem;
using System;
using System.Collections.Generic;

namespace MathsProblemGenerator
{
    public class MathsProblemGeneratorConsole
    {
        private List<IWriter> m_writers = new List<IWriter>();

        public MathsProblemGeneratorConsole()
        {
            // todo do this as attributes
            m_writers.Add(new AdHocConsoleWriter());
            m_writers.Add(new XlsxWriter());
            m_writers.Add(new CsvWriter());
        }

        private IWriter SelectWriter()
        {
            Console.WriteLine("Writers:");
            for (var index = 0; index < m_writers.Count; ++index )
            {
                Console.WriteLine($"  {index} : {m_writers[index].Description}");
            }
            var selection = ConsoleHelper.AskValue("Select the writer to use", 0, m_writers.Count - 1, 0);
            return m_writers[selection];
        }

        private void SetWriterQuestionWidthHeight(IWriter writer)
        {
            if( writer.MaxNumX > 1)
            {
                writer.NumX = ConsoleHelper.AskValue("Enter number of questions wide", 1, 1000, 3);
            }

            if (writer.MaxNumY > 1)
            {
                writer.NumX = ConsoleHelper.AskValue("Enter number of questions high", 1, 1000, 30);
            }
        }

        public void Run()
        {
            // todo this to be defined by the IMathsProblem
            Console.WriteLine("Format is a + b = answer");
            AskABAnswerValues(out var qMin, out var qMax, out var aMin, out var aMax);
            var mathsProblem = new NegPosAddition(qMin, qMax, aMin, aMax);

            // The Writers
            var writer = SelectWriter();
            SetWriterQuestionWidthHeight(writer);
            writer.Run(mathsProblem);
        }

        
        private void AskABAnswerValues(out int qMin, out int qMax, out int aMin, out int aMax)
        {
            Console.WriteLine("Enter 'a' and 'b' minimum value");
            qMin = ConsoleHelper.ReadValue(-9);
            Console.WriteLine("Enter 'a' and 'b' max value");
            qMax = ConsoleHelper.ReadValue(9);
            Console.WriteLine("Enter answer min value");
            aMin = ConsoleHelper.ReadValue(qMin);
            Console.WriteLine("Enter answer max value");
            aMax = ConsoleHelper.ReadValue(qMax);
        }
    }
}
