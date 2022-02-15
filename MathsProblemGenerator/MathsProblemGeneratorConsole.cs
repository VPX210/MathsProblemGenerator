using MathsProblem;
using System;
using System.Collections.Generic;

namespace MathsProblemGenerator
{
    public class MathsProblemGeneratorConsole
    {
        private List<IMathsProblem> m_problemTypes = new List<IMathsProblem>();
        private List<IWriter> m_writers = new List<IWriter>();

        // todo use autofac 
        public MathsProblemGeneratorConsole()
        {
            // todo do this as attributes
            m_writers.Add(new AdHocConsoleWriter());
            m_writers.Add(new XlsxWriter());
            m_writers.Add(new CsvWriter());

            m_problemTypes.Add(new NegPosAddition());
            m_problemTypes.Add(new NegPosMultiply());
            m_problemTypes.Add(new NegPosDivide());
        }

        private IMathsProblem SelectProblemType()
        {
            Console.WriteLine("Problem Type:");
            for (var index = 0; index < m_problemTypes.Count; ++index)
            {
                Console.WriteLine($"  {index} : {m_problemTypes[index].Description}");
            }
            var selection = ConsoleHelper.AskValue("Select the problem type", 0, m_problemTypes.Count - 1, 0);
            return m_problemTypes[selection];
        }

        private void InitialiseProblem(IMathsProblem mathsProblem)
        {
            AskABAnswerValues(out var qMin, out var qMax, out var aMin, out var aMax);
            mathsProblem.Initialise(qMin, qMax, aMin, aMax);
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
                writer.NumX = ConsoleHelper.AskValue("Enter number of questions wide", 1, writer.MaxNumX, 3);
            }

            if (writer.MaxNumY > 1)
            {
                writer.NumY = ConsoleHelper.AskValue("Enter number of questions high", 1, writer.MaxNumY, 30);
            }
        }

        public void Run()
        {
            // The IMathsProblem
            var mathsProblem = SelectProblemType();
            InitialiseProblem(mathsProblem);

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
