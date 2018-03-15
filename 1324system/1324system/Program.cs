using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1324system
{
    class Program
    {
        static int[] _system = new int[] { 1, 3, 2, 4 };
        //static int[] _system = new int[] { 1, 3, 2, 6 };

        static void Main(string[] args)
        {
            var amountOfTries = 100;
            var winningColor = 0;
            var stepInSystem = 0;
            var nextStake = 1;
            var balance = 10;

            Random rnd = new Random();
            int blackOrRed = rnd.Next(0, 2);
            Console.WriteLine("Starting gambling");
            Console.WriteLine("Balance " + balance.ToString());
            for (int i = 0; i < amountOfTries; i++)
            {
                //Log("Spinning result {0}", blackOrRed);
                var won = blackOrRed == winningColor;

                /*balance += (nextStake * (won ? 1 : -1));
                nextStake = (won ? nextStake * 2 : 1);
                Console.WriteLine(string.Format("{0}, new balace {1}", (won ? "Won" : "Lost"), balance));

                if (balance > 20)
                {
                    Console.ReadKey();
                    return;
                }*/
                balance += (_system[stepInSystem] * (won ? 1 : -1));
                Log("Stake is {0}", _system[stepInSystem]);
                Log("{0}, new balace {1}", (won ? "Won" : "Lost"), balance);

                if (won)
                {
                    if (stepInSystem + 1 == 4)
                        stepInSystem = 0;
                    else
                        stepInSystem++;
                } else
                    stepInSystem = 0;

                blackOrRed = rnd.Next(0, 2);
            }

            Console.WriteLine("End gambling");
            Console.WriteLine("Balance " + balance.ToString());
            Console.ReadKey();
        }

        private static void Log(string v, params object[] parameters)
        {
            Console.WriteLine(string.Format(v, parameters));
        }
    }
}
