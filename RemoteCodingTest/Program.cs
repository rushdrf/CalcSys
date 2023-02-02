using System.Linq;
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
                ToSleep();
                Console.WriteLine("Please enter a calculation (eg 1 + 1): ");
                inputCalc = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(inputCalc))
                    continue;
                else if (GetOperators().Where(x => inputCalc.Contains(x)).Count() == 0)
                {
                    Console.WriteLine("Invalid Input");
                    continue;
                }
                else if (GetUsedDelimiter().Where(x => inputCalc.Contains(x)).Count() > 0)
                {
                    Console.WriteLine("Please remove \"@\" and \"#\" symbols from the calculation");
                    continue;
                }
                else
                {
                    Console.WriteLine("The answer for " + inputCalc + " = " + Calculate(inputCalc));
                    ToSleep();

                TryAgain:
                    Console.WriteLine("Do you want to try again? y/n");
                    options = Console.ReadLine();

                    if (options == "y")
                        continue;
                    else if (options == "n")
                    {
                        ToSleep();
                        Console.WriteLine("Thank you for using Calculating System");
                        break;
                    }
                    else
                    {
                        ToSleep();
                        Console.WriteLine("Invalid Input");
                        ToSleep(1500);
                        goto TryAgain;
                    }
                }
            }
        }

        public static double? Calculate(string sum)
        {
            string? newSum = null;
            sum = sum.Trim().Replace(" ", "").Replace("(", "#(").Replace(")", ")#");
            string[]? splitSums = sum.Split("#").Where(x => !string.IsNullOrEmpty(x)).ToArray();
            for (int a = 0; a >= 0; a++)
            {
                if (!string.IsNullOrEmpty(newSum))
                {
                    splitSums = newSum.Split("#").Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    splitSums = SolveCalculation(splitSums);
                }
                else
                {
                    splitSums = SolveCalculation(splitSums);
                }

                newSum = string.Join("", splitSums);

                if (GetParentheses().Where(x => newSum.Contains(x)).Count() > 0)
                {
                    if (newSum.Count(x => x == '*') == 1 && (newSum.Contains("+") || newSum.Contains("-")))
                    {
                        newSum = BodmasHandler(newSum, "*");
                        continue;
                    }
                    else if (newSum.Count(x => x == '/') == 1 && (newSum.Contains("+") || newSum.Contains("-")))
                    {
                        newSum = BodmasHandler(newSum, "/");
                        continue;
                    }
                    else
                    {
                        newSum = newSum.Trim().Replace(" ", "").Replace("(", "#(").Replace(")", ")#");
                        continue;
                    }
                }
                else if (newSum.Count(x => x == '*') > 0 && newSum.Count(x => x == '/') == 0 && (newSum.Contains("+") || newSum.Contains("-")))
                {
                    newSum = BodmasHandler(newSum, "*");
                    continue;
                }
                else if (newSum.Count(x => x == '/') > 0 && newSum.Count(x => x == '*') == 0 && (newSum.Contains("+") || newSum.Contains("-")))
                {
                    newSum = BodmasHandler(newSum, "/");
                    continue;
                }
                else if (newSum.Count(x => GetOperators(true).Contains(x)) > 0 && (newSum.Contains("+") || newSum.Contains("-")))
                {
                    newSum = BodmasHandler(newSum, GetOperators(true));
                    continue;
                }
                else
                    break;   
            }
            var result = RunCalculate(newSum);
            return result.doubleVal;
        }

        public static void ToSleep(int? millisec = null)
        {
            int? interval = millisec == null ? 1000 : millisec;
            Console.WriteLine("Please Wait...");
            Thread.Sleep((int)interval);
        }

        public static Calculation RunCalculate(string subCalc)
        {
            double result = 0;
            var arrayCalc = SplitByOperatorAndTrimBracket(subCalc);
            var calcLen = arrayCalc.Length;
            for (int i = 1; i < calcLen; i = i + 2)
            {
                string oper = arrayCalc[i];
                double b = Convert.ToDouble(arrayCalc[i + 1]);
                if (i == 1)
                {
                    double a = Convert.ToDouble(arrayCalc[i - 1]);
                    if (oper.Equals("+"))
                    {
                        result = a + b;
                    }
                    else if (oper.Equals("-"))
                    {
                        result = a - b;
                    }
                    else if (oper.Equals("*"))
                    {
                        result = a * b;
                    }
                    else if (oper.Equals("/"))
                    {
                        result = a / b;
                    }
                    else
                        throw new Exception("Operator not found");
                }
                else
                {
                    if (oper.Equals("+"))
                    {
                        result += b;
                    }
                    else if (oper.Equals("-"))
                    {
                        result -= b;
                    }
                    else if (oper.Equals("*"))
                    {
                        result *= b;
                    }
                    else if (oper.Equals("/"))
                    {
                        result /= b;
                    }
                    else
                        throw new Exception("Operator not found");
                }

            }
            return new Calculation { stringVal = result.ToString(), doubleVal = result };
        }

        public static string[] GetParentheses()
        {
            return new string[] { "(", ")" };
        }

        public static string[] SolveCalculation(string[] splitSums)
        {
            for (int i = 0; i < splitSums.Length; i++)
            {
                if (GetParentheses().Where(x => splitSums[i].Contains(x)).Count() == 2)
                {
                    splitSums[i] = RunCalculate(splitSums[i]).stringVal;
                }
            }
            return splitSums;
        }

        public static string BodmasHandler(string newSum, string op)
        {
            var splitNewSum = SplitByOperator(newSum);

            if (newSum.Count(x => x.ToString() == op) > 1)
            {
                var ListIndexOp = new List<int>();
                for (int b = 0; b < splitNewSum.Length; b++)
                {
                    if (splitNewSum[b].Equals(op))
                    {
                        ListIndexOp.Add(b);
                    }
                }
                var (FirstIndexOp, LastIndexOp) = OperatorsFirstAndLastIndex(ListIndexOp);

                splitNewSum[FirstIndexOp - 1] = "(" + splitNewSum[FirstIndexOp - 1];
                splitNewSum[LastIndexOp + 1] = splitNewSum[LastIndexOp + 1] + ")";
            }
            else
            {
                for (int b = 0; b < splitNewSum.Length; b++)
                {
                    if (splitNewSum[b].Equals(op))
                    {
                        splitNewSum[b - 1] = "(" + splitNewSum[b - 1];
                        splitNewSum[b + 1] = splitNewSum[b + 1] + ")";
                    }
                }
            }
            newSum = string.Join("", splitNewSum);
            newSum = newSum.Trim().Replace(" ", "").Replace("(", "#(").Replace(")", ")#");
            return newSum;
        }

        public static string BodmasHandler(string newSum, char[] op)
        {
            var splitNewSum = SplitByOperator(newSum);

            if (newSum.Count(x => op.Contains(x)) > 1)
            {
                var convertOp = op.Select(x => x.ToString()).ToArray();
                var ListIndexOp = new List<int>();
                for (int b = 0; b < splitNewSum.Length; b++)
                {
                    if (convertOp.Contains(splitNewSum[b]))
                    {
                        ListIndexOp.Add(b);
                    }
                }
                var (FirstIndexOp, LastIndexOp) = OperatorsFirstAndLastIndex(ListIndexOp);

                splitNewSum[FirstIndexOp - 1] = "(" + splitNewSum[FirstIndexOp - 1];
                splitNewSum[LastIndexOp + 1] = splitNewSum[LastIndexOp + 1] + ")";
            }
            else
            {
                throw new Exception("Invalid Data");
            }
            newSum = string.Join("", splitNewSum);
            newSum = newSum.Trim().Replace(" ", "").Replace("(", "#(").Replace(")", ")#");
            return newSum;
        }

        public static char[] GetOperators(bool priority = false)
        {
            if (priority)
                return new char[] { '*', '/' };
            else
                return new char[] { '+', '-', '*', '/' };
        }

        public static char[] GetUsedDelimiter()
        {
            return new char[] { '#', '@' };
        }

        public static (int, int) OperatorsFirstAndLastIndex(List<int> Input)
        {
            var FirstIndexOp = Input.Min();
            var LastIndexOp = Input.Max();
            return (FirstIndexOp, LastIndexOp);
        }

        public static string[] SplitByOperatorAndTrimBracket(string newSum)
        {
            newSum = newSum.Replace("(", "").Replace(")", "").Replace("+", "@+@").Replace("-", "@-@").Replace("*", "@*@").Replace("/", "@/@");
            return newSum.Split("@");
        }
        public static string[] SplitByOperator(string newSum)
        {
            newSum = newSum.Replace("+", "@+@").Replace("-", "@-@").Replace("*", "@*@").Replace("/", "@/@");
            return newSum.Split("@");
        }
    }

    public class Calculation
    {
        public string stringVal { get; set; }
        public double doubleVal { get; set; }
    }
}