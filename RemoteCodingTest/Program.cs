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
                    Console.WriteLine("The answer for " + inputCalc + " = " + Calculate(inputCalc));
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

        public static double? Calculate(string sum)
        {
            sum = sum.Trim();
            var noSpacesSum = sum.Replace(" ","");

            double answer = 0;
            int startIndexOpenBrac = 0;
            int startIndexCloseBrac = 0;

            while(startIndexOpenBrac != -1 || startIndexCloseBrac != -1)
            {
                startIndexOpenBrac = noSpacesSum.IndexOf("(");
                startIndexCloseBrac = noSpacesSum.IndexOf(")");
                if (startIndexOpenBrac == -1 || startIndexCloseBrac == -1)
                    break;
                else
                {
                    var subCalc = noSpacesSum.Substring(startIndexOpenBrac, startIndexCloseBrac - startIndexOpenBrac + 1);

                    if (subCalc.Where(x => Parentheses().Contains(x.ToString())).Count() > 2)
                    {

                    }
                    else
                    {
                        answer = answer + runCalculate(subCalc);
                        noSpacesSum = noSpacesSum.Substring(0, startIndexOpenBrac) + answer.ToString() + noSpacesSum.Substring(startIndexCloseBrac + 1);
                    }
                }
            }

            answer = runCalculate(noSpacesSum);

            return answer;
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

        public static bool CheckOperator(string? oper, out string? operOut, out string? operType)
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

        public static double runCalculate(string subCalc)
        {
            double result = 0;
            var val = subCalc.Replace("(","").Replace(")","");

            val = BodmasSorting(val);
            
            if (val.Length == 3)
            {
                string oper = val[1].ToString();
                double a = Convert.ToDouble(val[0].ToString());
                double b = Convert.ToDouble(val[2].ToString());
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
            else if (val.Length == 5)
            {
                string oper = val[1].ToString();
                string oper2 = val[3].ToString();
                double subResult = 0;
                double a = Convert.ToDouble(val[0].ToString());
                double b = Convert.ToDouble(val[2].ToString());
                double c = Convert.ToDouble(val[4].ToString());
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
            else if (val.Length == 7)
            {
                string oper = val[1].ToString();
                string oper2 = val[3].ToString();
                string oper3 = val[5].ToString();
                double subResult = 0;
                double a = Convert.ToDouble(val[0].ToString());
                double b = Convert.ToDouble(val[2].ToString());
                double c = Convert.ToDouble(val[4].ToString());
                double d = Convert.ToDouble(val[6].ToString());
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
            return result;
        }

        public static string BodmasSorting(string val)
        {
            if (val.Contains("*") || val.Contains("/"))
            {
                int startIndexOfMultiply = 0;
                int startIndexOfDivision = 0;
                while (startIndexOfMultiply != -1 || startIndexOfDivision != -1)
                {
                    startIndexOfMultiply = val.IndexOf("*");
                    startIndexOfDivision = val.IndexOf("/");
                    if (startIndexOfMultiply == -1 && startIndexOfDivision == -1)
                        break;
                    else
                    {
                        if (startIndexOfMultiply != -1 && startIndexOfDivision != -1)
                        {
                            if (startIndexOfMultiply < startIndexOfDivision)
                            {
                                val = val.Substring(startIndexOfMultiply - 1, startIndexOfMultiply + 1) + val.Substring(0, startIndexOfMultiply - 1) + val.Substring(startIndexOfMultiply + 2);
                            }
                            else if (startIndexOfMultiply > startIndexOfDivision)
                            {
                                val = val.Substring(startIndexOfDivision - 1, startIndexOfDivision + 1) + val.Substring(0, startIndexOfDivision - 1) + val.Substring(startIndexOfDivision + 2);
                            }
                            else
                            {
                                val = val;
                            }
                        }
                        else if (startIndexOfMultiply != -1)
                        {
                            val = val.Substring(startIndexOfMultiply - 1, startIndexOfMultiply + 1) + val.Substring(0, startIndexOfMultiply - 1);
                        }
                        else if (startIndexOfDivision != -1)
                        {
                            val = val.Substring(startIndexOfDivision - 1, startIndexOfDivision + 1) + val.Substring(0, startIndexOfDivision - 1);
                        }
                        else
                        {
                            val = val;
                        }
                    }
                }
                return val;
            }
            else
                return val;
        }
        public static string[] Parentheses()
        {
            return new string[] { "(", ")" };
        }
    }
}