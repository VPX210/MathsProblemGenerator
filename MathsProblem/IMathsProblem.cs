﻿
using System.Collections.Generic;

namespace MathsProblem
{
    public interface IMathsProblem
    {
        string Description { get; }

        string FileNameSummary { get; }

        void Initialise(int min, int max, int ansMin, int ansMax);

        string BlankSeparator { get; set; }

        void GenerateNextProblem(out List<string> questions, out List<string> answers);
    }
}
