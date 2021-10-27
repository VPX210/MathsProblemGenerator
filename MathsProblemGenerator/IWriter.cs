using MathsProblem;

namespace MathsProblemGenerator
{
    public interface IWriter
    {
        string Description { get; }

        int MaxNumX { get; }
        int MaxNumY { get; }

        int NumX { get;  set; }
        int NumY { get; set; }

        void Run(IMathsProblem problemGenerator);
    }
}
