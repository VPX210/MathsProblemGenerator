﻿using System;
using System.Collections.Generic;

namespace MathsProblem
{
    public class NegPosAddition : IMathsProblem
    {
        private int m_min;
        private int m_max;
        private int m_ansMin;
        private int m_ansMax;
        private readonly Random m_random = new Random();

        public string BlankSeparator { get; set; }

        public string Description => "Addition and subtraction in the form: a + b = answer";

        public string FileNameSummary => "Add";

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

        public void GetNextProblem(out int a, out int b, out int answer)
        {
            do
            {
                GenerateNextProblem(out a, out b, out answer);
            } while (answer < m_ansMin || answer > m_ansMax);
        }

        private void GenerateNextProblem(out int a, out int b, out int answer)
        {
            a = m_random.Next(m_min, m_max + 1);
            b = m_random.Next(m_min, m_max + 1);
            answer = a + b;
        }

        private int m_solvePos = 0;

        public void GenerateNextProblem(out List<string> questions, out List<string> answers)
        {
            questions = new List<string>();
            answers = new List<string>();
            GetNextProblem(out var a, out var b, out var answer);
            questions.Add(m_solvePos == 0 ? BlankSeparator : a.ToString());
            answers.Add(a.ToString());
            if (b >= 0)
            {
                questions.Add("+");
                answers.Add("+");
                questions.Add(m_solvePos == 1 ? BlankSeparator : b.ToString());
                answers.Add(b.ToString());
            }
            else
            {
                questions.Add("-");
                answers.Add("-");
                b = 0 - b;
                questions.Add(m_solvePos == 1 ? BlankSeparator : b.ToString());
                answers.Add(b.ToString());
            }
            questions.Add("=");
            answers.Add("=");
            questions.Add(m_solvePos == 2 ? BlankSeparator : answer.ToString());
            answers.Add(answer.ToString());

            ++m_solvePos;
            if (m_solvePos >= 3)
                m_solvePos = 0;
        }
    }
}


