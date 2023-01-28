using System.Runtime.CompilerServices;

namespace RemoteCodingTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? options;
            string? inputCalc;

            for (int i = 0; i == -1; i++)
            {
                Console.WriteLine("Please enter a calculation:");
                inputCalc = Console.ReadLine();
                if (string.IsNullOrEmpty(inputCalc))
                {
                    Console.WriteLine("Please enter some calculation:");
                    inputCalc = Console.ReadLine();

                    if (string.IsNullOrEmpty(inputCalc)) continue;
                    else
                    {
                        Console.WriteLine(Calculate(inputCalc));
                        Thread.Sleep(1000);
                        Console.WriteLine("Do you want to try again? y/n");
                        options = Console.ReadLine() == null ? null : Console.ReadLine().ToLower();
                        if (options == "y")  
                    }
                }
                else break;
            }
        }
        public static double Calculate(string sum)
        {

            return 0;
        }

    }
}