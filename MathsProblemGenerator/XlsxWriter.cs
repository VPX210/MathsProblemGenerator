using MathsProblem;
using OfficeOpenXml;
using System;
using System.IO;

namespace MathsProblemGenerator
{
    public class XlsxWriter : IWriter
    {
        public string Description => "Write to an XLSX file";

        public int MaxNumX => 1000;

        public int MaxNumY => 1000;

        public int NumX { get; set; }
        public int NumY { get; set; }

        private void SetupWorkSheet( ExcelWorksheet ws )
        {
            ws.DefaultRowHeight = 22;
            ws.DefaultColWidth = 5;
            ws.Cells.Style.Font.Size = 16;

            ws.PrinterSettings.FitToWidth = 1;
            ws.PrinterSettings.PaperSize = ePaperSize.A4;
            ws.PrinterSettings.Orientation = eOrientation.Portrait;
            ws.PrinterSettings.ShowGridLines = true;
        }

        public void Run(IMathsProblem problemGenerator)
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "MathsProblemGenerator.xlsx");
            Console.WriteLine($"Writing question XLSX file to:{filePath}");

            if (File.Exists(filePath))
                File.Delete(filePath);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var ep = new ExcelPackage(filePath))
            {
                var wsq = ep.Workbook.Worksheets.Add("Questions");
                var wsa = ep.Workbook.Worksheets.Add("Answers");
                SetupWorkSheet(wsq);
                SetupWorkSheet(wsa);

                for (var yLoop = 1; yLoop <= NumY; ++yLoop)
                {
                    var xPos = 1;
                    for (var xLoop = 0; xLoop < NumX; ++xLoop)
                    {
                        problemGenerator.GenerateNextProblem(out var quesion, out var answer);

                        for (var eLoop = 0; eLoop < quesion.Count; ++eLoop)
                        {
                            wsa.Column(xPos).AutoFit(5);
                            wsq.Column(xPos).AutoFit(5);

                            wsq.Cells[yLoop, xPos].Value = quesion[eLoop];
                            wsa.Cells[yLoop, xPos].Value = answer[eLoop];
                            ++xPos;
                        }

                        if (xLoop + 1 < NumX)
                        {
                            ++xPos;
                        }
                    }
                }
                ep.Save();
            }
        }
    }
}
