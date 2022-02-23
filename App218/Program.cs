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
            Console.WriteLine("请输入number1的值：");
            double a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("请输入number2的值：");
            double b = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("请输入进位的值：");
            double c = Convert.ToDouble(Console.ReadLine());
            double d = a + b;
            Console.WriteLine(d);
            Console.WriteLine($"a+b+c: {a + b + c}");
        }
    }

}
