
using System.Collections.Generic;

namespace MathsProblem
{
    public interface IMathsProblem
    {
        string Description { get; }

        void Initialise(int min, int max, int ansMin, int ansMax);

        string BlankSeparator { get; set; }

        void GetNextProblem(out int a, out int b, out int answer);
        void GenerateNextProblem(out List<string> questions, out List<string> answers);
    }
}
