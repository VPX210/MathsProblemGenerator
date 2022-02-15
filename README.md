# MathsProblemGenerator
A simple Windows command line maths problem generator

Is run in modes:
1. Simple command line question then answer display
2. Writes output to CSV files:
 * Two CSV files: MathsProblemGenerator.csv and MathsProblemGeneratorAnswer.csv on the desktop folder
 * Commandline allows specifying the number of question columns and rows for the files (recommend 3 x 30)
 * Can be imported into a spreadsheet for formatting and printing
3. Writes output to XLSX files:
 * Single .xlsx fils: MathsProblemGenerator.csv and MathsProblemGeneratorAnswer.xlsx on the desktop folder
 * Commandline allows specifying the number of questions columns and rows for the file (recommend 3 x 30)
 * One sheet for questions, one sheet for answers
 * Defaults to A4 with gridlines ready for printing

## Addition and Subtraction
Creates multiple problems in the format: a + b = ans
Where 
* a and b min max values can be set (can be negative)
* ans min max values can be set  (can be negative)
* Each problem generated rotate a, b then ans to be blank (calculated)

## Multiplication 
Creates multiple problems in the format: a x b = ans
Where 
* a and b min max values can be set (can be negative)
* ans min max values can be set (can be negative)
* Each problem generated rotate a, b then ans to be blank (calculated)

## Multiplication 
Creates multiple problems in the format: a x b = ans
Where 
* a and b min max values can be set
* ans min max values can be set
* Each problem generated rotate a, b then ans to be blank (calculated)

## Division 
Creates multiple problems in the format: a / b = ans
Where 
* b min max values can be set
* ans min max values can be set
* ans is a whole number, a is always larger than b
* Each problem generated rotate a, b then ans to be blank (calculated)