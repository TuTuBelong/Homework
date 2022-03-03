using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prime_number
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("求2-100内的素数");
            int a, i;
            for( a = 2; a <= 100; a++)
            {
                for( i=2;i <= 100;i++)
                {
                    if (a % i == 0)
                        break;
                }
                if (a == i)
                        Console.WriteLine(a);
            }
            Console.ReadKey();
        }
    }
}
