using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace App218
{
    class Program
    {
        static void Main(string[] args)
        {
            double a = 0;
            double b = 0;
            double c = 0;
            double d = 0;   
            Console.WriteLine("请输入number1的值：");
            a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("请输入number2的值：");
            b = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("请输入进位的值：");
            c = Convert.ToDouble(Console.ReadLine());
            d = a + b + c;
            Console.WriteLine(d);
            Console.WriteLine($"a+b+c: {a + b + c}");
        }
    }

}
