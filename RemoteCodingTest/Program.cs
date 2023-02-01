using System.Linq;
using System.Net.NetworkInformation;

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
                    toSleep();

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
                else if (newSum.Count(x => x == '*') == 1 && (newSum.Contains("+") || newSum.Contains("-")))
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
                    break;   
            }
            var result = runCalculate(newSum);
            return result.doubleVal;
        }

        public static void toSleep(int? millisec = null)
        {
            int? interval = millisec == null ? 1000 : millisec;
            Console.WriteLine("Please Wait...");
            Thread.Sleep((int)interval);
        }

        public static Calculation runCalculate(string subCalc)
        {
            double result = 0;
            subCalc = subCalc.Replace("(", "").Replace(")", "").Replace("+", "@+@").Replace("-", "@-@").Replace("*", "@*@").Replace("/", "@/@");
            var arrayCalc = subCalc.Split("@");
            var calcLen = arrayCalc.Length;

            if (calcLen == 3)
            {
                string oper = arrayCalc[1];
                double a = Convert.ToDouble(arrayCalc[0]);
                double b = Convert.ToDouble(arrayCalc[2]);
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
            else if (calcLen == 5)
            {
                string oper = arrayCalc[1];
                string oper2 = arrayCalc[3].ToString();
                double subResult = 0;
                double a = Convert.ToDouble(arrayCalc[0]);
                double b = Convert.ToDouble(arrayCalc[2]);
                double c = Convert.ToDouble(arrayCalc[4]);
                if (oper.Equals("+"))
                {
                    subResult = a + b;
                }
                else if (oper.Equals("-"))
                {
                    subResult = a - b;
                }
                else if (oper.Equals("*"))
                {
                    subResult = a * b;
                }
                else if (oper.Equals("/"))
                {
                    subResult = a / b;
                }
                else
                    throw new Exception("Operator not found");

                if (oper2.Equals("+"))
                {
                    result = subResult + c;
                }
                else if (oper2.Equals("-"))
                {
                    result = subResult - c;
                }
                else if (oper2.Equals("*"))
                {
                    result = subResult * c;
                }
                else if (oper2.Equals("/"))
                {
                    result = subResult / c;
                }
                else
                    throw new Exception("Operator not found");
            }
            else if (calcLen == 7)
            {
                string oper = arrayCalc[1];
                string oper2 = arrayCalc[3];
                string oper3 = arrayCalc[5];
                double subResult = 0;
                double a = Convert.ToDouble(arrayCalc[0]);
                double b = Convert.ToDouble(arrayCalc[2]);
                double c = Convert.ToDouble(arrayCalc[4]);
                double d = Convert.ToDouble(arrayCalc[6]);
                if (oper.Equals("+"))
                {
                    subResult = a + b;
                }
                else if (oper.Equals("-"))
                {
                    subResult = a - b;
                }
                else if (oper.Equals("*"))
                {
                    subResult = a * b;
                }
                else if (oper.Equals("/"))
                {
                    subResult = a / b;
                }
                else
                    throw new Exception("Operator not found");

                if (oper2.Equals("+"))
                {
                    subResult += c;
                }
                else if (oper2.Equals("-"))
                {
                    subResult -= c;
                }
                else if (oper2.Equals("*"))
                {
                    subResult *= c;
                }
                else if (oper2.Equals("/"))
                {
                    subResult /= c;
                }
                else
                    throw new Exception("Operator not found");

                if (oper3.Equals("+"))
                {
                    result = subResult + d;
                }
                else if (oper3.Equals("-"))
                {
                    result = subResult - d;
                }
                else if (oper3.Equals("*"))
                {
                    result = subResult * d;
                }
                else if (oper3.Equals("/"))
                {
                    result = subResult / d;
                }
                else
                    throw new Exception("Operator not found");
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
                    splitSums[i] = runCalculate(splitSums[i]).stringVal;
                }
            }
            return splitSums;
        }

        public static string BodmasHandler(string newSum, string op)
        {
            var splitNewSum = newSum.ToCharArray().Select(x => x.ToString()).ToArray();

            for (int b = 0; b < splitNewSum.Length; b++)
            {
                if (splitNewSum[b].Equals(op))
                {
                    splitNewSum[b - 1] = "(" + splitNewSum[b - 1];
                    splitNewSum[b + 1] = splitNewSum[b + 1] + ")";
                }
            }
            newSum = string.Join("", splitNewSum);
            newSum = newSum.Trim().Replace(" ", "").Replace("(", "#(").Replace(")", ")#");
            return newSum;
        }
        
        public static char[] GetOperators()
        {
            return new char[] { '+', '-', '*', '/' };
        }

        public static char[] GetUsedDelimiter()
        {
            return new char[] { '#', '@' };
        }
    }

    public class Calculation
    {
        public string stringVal { get; set; }
        public double doubleVal { get; set; }
    }
}