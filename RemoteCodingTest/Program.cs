using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace RemoteCodingTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            Exec();
        }

        public static void Exec()
        {
            string? options;
            string? inputCalc;

            Console.WriteLine("Welcome to Calculating System");
            for (int i = 0; i < 10; i--)
            {
                toSleep();
                Console.WriteLine("Please enter a calculation (eg 1 + 1): ");
                inputCalc = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(inputCalc))
                    continue;
                else
                {
                    Console.WriteLine("The answer for " + inputCalc + ": " + Calculate(inputCalc));
                    toSleep(1500);

                TryAgain:
                    Console.WriteLine("Do you want to try again? y/n");
                    options = Console.ReadLine();

                    if (options == "y")
                        continue;
                    else if (options == "n")
                    {
                        toSleep();
                        Console.WriteLine("Thank you for using Calculating System");
                        break;
                    }
                    else
                    {
                        toSleep();
                        Console.WriteLine("Invalid Input");
                        toSleep(1500);
                        goto TryAgain;
                    }
                }
            }
        }
        public static double Calculate(string sum)
        {
            //var calculations = new List<Calculation>();
            var splitSum = sum.Split(' ');
            List<object> calcs = new List<object>();
            for (int i = 0; i < splitSum.Length; i++)
            {
                //if (Identity(splitSum[i], out string? output, out string? dataType))
                //{
                //    calculations.Add(new Calculation { Index = i, Value = output, DataType = dataType });
                //}
                if (double.TryParse(splitSum[i], out double output))
                {
                    calcs.Add(output);
                }
                else 
                {
                    calcs.Add(splitSum[i]);
                }
            }
            var answer = runCalculate(calcs);

            return 0;
        }

        public static void toSleep(int? millisec = null)
        {
            int? interval = millisec == null ? 1000 : millisec;
            Console.WriteLine("Please Wait...");
            Thread.Sleep((int)interval);
        }

        public static bool Identity(string line, out string? output, out string? dataType)
        {
            if (double.TryParse(line, out var result))
            {
                output = line;
                dataType = "Number";
                return true;
            }
            else if (checkParentheses(line, out string? parentheses, out string? parenthesesType))
            {
                output = parentheses;
                dataType = parenthesesType;
                return true;
            }
            else if (CheckOperator(line, out string? operOut, out string? operType))
            {
                output = operOut;
                dataType = operType;
                return true;
            }
            else
            {
                output = null;
                dataType = null;
                return false;
            }
        }

        public static bool checkParentheses(string line, out string? parentheses, out string? parenthesesType)
        {
            switch (line)
            {
                case "(":
                    parentheses = "(";
                    parenthesesType = "OpenParentheses";
                    return true;
                case ")":
                    parentheses = ")";
                    parenthesesType = "CloseParentheses";
                    return true;
                default:
                    parentheses = null;
                    parenthesesType = null;
                    return false;
            }
        }

        public static bool CheckOperator(string oper, out string? operOut, out string? operType)
        {
            switch (oper)
            {
                case "+":
                    operOut = "+";
                    operType = "Add";
                    return true;
                case "-":
                    operOut = "-";
                    operType = "Minus";
                    return true;
                case "*":
                    operOut = "*";
                    operType = "Multiply";
                    return true;
                case "/":
                    operOut = "/";
                    operType = "Divide";
                    return true;
                default:
                    operOut = null;
                    operType = null;
                    return false;
            }
        }

        public static double? runCalculate(List<object> inputCalc)
        {
            if (inputCalc.Contains("(") && inputCalc.Contains(")"))
            {

            }
            else
            {
                double result;
                foreach (object inputCalcItem in inputCalc)
                {
                    result = inputCalcItem.GetType().Name == "Double" ? inputCalcItem
                }
            }
            //switch (oper)
            //{
            //    case "+":
            //        return x + y;
            //    case "-":
            //        return x - y;
            //    case "*":
            //        return x * y;
            //    case "/":
            //        return x / y;
            //}
            return null;

        }
        public string[] Bodmas()
        {
            return new string[] { "*", "/" };
        }
    }
}