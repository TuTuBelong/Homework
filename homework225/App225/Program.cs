using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace App225
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入一个数字a");
            int a= Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("输出"+a+"所有素数因子");
            for(int i = 2; i <=a; i++)
            {
                while (a % i == 0)
                {
                    a = a / i;
                    Console.WriteLine(i);
                }
            }
            Console.ReadKey();
        }
    }
}
