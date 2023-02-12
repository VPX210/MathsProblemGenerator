using System;
using System.Collections.Generic;

namespace MathsProblem
{
    public class SimLinearBasic : IMathsProblem
    {
        private int m_min;
        private int m_max;
        private int m_ansMin;
        private int m_ansMax;
        private readonly Random m_random = new Random();

        public string BlankSeparator { get; set; }

        public string Description => "Simultaneous Linear Basic Equations: a x b = answer1, a + b = answer2";
        public string FileNameSummary => "SimBasic";

        public void Initialise(int min, int max, int ansMin, int ansMax)
        {

            m_min = min;
            m_max = max;
            if (m_min > m_max)
                m_min = m_max;

            m_ansMin = ansMin;
            if (m_ansMin > m_min)
                m_ansMin = m_min;

            m_ansMax = ansMax;
            if (m_ansMax < m_max)
                m_ansMax = m_max;

            // Make sure there are enough _ to fit a max answer
            BlankSeparator = "_";
            var maxLen = Math.Abs(ansMax);
            while (maxLen > 0)
            {
                maxLen /= 10;
                BlankSeparator += "_";
            }
        }

        private void GenerateNextMultiplyProblem(out int a, out int b, out int answer)
        {
            do
            {
                a = m_random.Next(m_min, m_max + 1);
                b = m_random.Next(m_min, m_max + 1);
                answer = a * b;
            } while (answer < m_ansMin || answer > m_ansMax);
        }

        private void GenerateNextDivideProblem(out int a, out int b, out int answer)
        {
            do
            {
                b = m_random.Next(m_min, m_max + 1);
                answer = m_random.Next(m_min, m_max + 1);
                a = b * answer;
            } while (a < m_ansMin || a > m_ansMax);
        }

        // a x b = answer1
        // a + b = answer2
        private void GenerateNextMultiplyAddProblem(out int a, out int b, out int answer1, out int answer2)
        {
            GenerateNextMultiplyProblem(out a, out b, out answer1);
            answer2 = a + b;
        }

        // a x b = answer1
        // a - b = answer2
        private void GenerateNextMultiplySubProblem(out int a, out int b, out int answer1, out int answer2)
        {
            GenerateNextMultiplyProblem(out a, out b, out answer1);
            answer2 = a - b;
        }

        // a / b = answer1
        // a + b = answer2
        private void GenerateNextDivideAddProblem(out int a, out int b, out int answer1, out int answer2)
        {
            GenerateNextDivideProblem(out a, out b, out answer1);
            answer2 = a + b;
        }

        // a / b = answer1
        // a - b = answer2
        private void GenerateNextDivideSubProblem(out int a, out int b, out int answer1, out int answer2)
        {
            GenerateNextDivideProblem(out a, out b, out answer1);
            answer2 = a - b;
        }

        private void PopulatePortionProblem(int a, string operation, int b, int answer, List<string> questions, List<string> answers)
        {
            questions.Add(BlankSeparator);
            answers.Add(a.ToString());

            questions.Add(operation);
            answers.Add(operation);

            questions.Add(BlankSeparator);
            answers.Add(b.ToString());

            questions.Add("=");
            answers.Add("=");

            questions.Add(answer.ToString());
            answers.Add(answer.ToString());
        }

        private void PopulateEquationSeparator(List<string> questions, List<string> answers)
        {
            questions.Add("Same as");
            answers.Add("Same as");
        }

        private void GenerateNextMultiplyAddProblem(List<string> questions, List<string> answers)
        {
            GenerateNextMultiplyAddProblem(out var a, out var b, out var answer1, out var answer2);
            
            PopulatePortionProblem(a, "x", b, answer1, questions, answers);
            PopulateEquationSeparator(questions, answers);
            PopulatePortionProblem(a, "+", b, answer2, questions, answers);
        }

        private void GenerateNextMultiplySubProblem(List<string> questions, List<string> answers)
        {
            GenerateNextMultiplySubProblem(out var a, out var b, out var answer1, out var answer2);

            PopulatePortionProblem(a, "x", b, answer1, questions, answers);
            PopulateEquationSeparator(questions, answers);
            PopulatePortionProblem(a, "-", b, answer2, questions, answers);
        }

        private void GenerateNextDivideAddProblem(List<string> questions, List<string> answers)
        {
            GenerateNextDivideAddProblem(out var a, out var b, out var answer1, out var answer2);

            PopulatePortionProblem(a, "/", b, answer1, questions, answers);
            PopulateEquationSeparator(questions, answers);
            PopulatePortionProblem(a, "+", b, answer2, questions, answers);
        }

        private void GenerateNextDivideSubProblem(List<string> questions, List<string> answers)
        {
            GenerateNextDivideSubProblem(out var a, out var b, out var answer1, out var answer2);

            PopulatePortionProblem(a, "/", b, answer1, questions, answers);
            PopulateEquationSeparator(questions, answers);
            PopulatePortionProblem(a, "-", b, answer2, questions, answers);
        }

        private int m_solvePos = 0;

        public void GenerateNextProblem(out List<string> questions, out List<string> answers)
        {
            questions = new List<string>();
            answers = new List<string>();
            
            switch(m_solvePos)
            {
                case 0:
                    GenerateNextMultiplyAddProblem(questions, answers);
                    break;
                case 1:
                    GenerateNextMultiplySubProblem(questions, answers);
                    break;
                case 2:
                    GenerateNextDivideAddProblem(questions, answers);
                    break;
                case 3:
                    GenerateNextDivideSubProblem(questions, answers);
                    break;
            }

            ++m_solvePos;
            if (m_solvePos > 3)
                m_solvePos = 0;
        }
    }
}

