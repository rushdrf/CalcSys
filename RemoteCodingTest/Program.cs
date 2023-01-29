using System.Runtime.CompilerServices;

namespace RemoteCodingTest
{
    internal class Program
    {
        static void Main(string[] args)
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

            return 0;
        }

        public static void toSleep(int? millisec = null)
        {
            int? interval = millisec == null ? 1000 : millisec;
            Console.WriteLine("Please Wait...");
            Thread.Sleep((int)interval);
        }

    }
}