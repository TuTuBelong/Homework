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
            Console.WriteLine("请输入要选择的运算方式（+，-，*，/）：");
            string c = Convert.ToString(Console.ReadLine());
            switch (c)
            {
                case "+":
                    Console.WriteLine($"a+b: {a + b}");
                    break;
                case "-":
                    Console.WriteLine($"a-b: {a - b}");
                    break;
                case "*":
                    Console.WriteLine($"a*b: {a * b}");
                    break;
                case "/":
                    Console.WriteLine($"a/b: {a / b}");
                    break;

                default: 
                    Console.WriteLine("输入有误");
                    break;
            }
            Console.ReadKey();
        }
    }

}
