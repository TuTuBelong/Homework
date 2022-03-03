using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App02
{
    internal class Program
    {
        public class Arr
        {
            public int[] a;
            public int getSum()
            {
                int sum=0;
                foreach (int i in a)
                {
                    sum+=i;
                }
                return sum;
            }
            public int getAve()
            {
                int ave=getSum()/a.Length;
                return ave;
            }
            public int getMax()
            {
                int max=a[0];
                for(int i=1; i<a.Length; i++)
                {
                    if (a[i] > a[0])
                    {
                        max=a[i];
                    }
                }
                return max;
            }
            public int getMin()
            {
                int min = a[0];
                for (int i = 1; i < a.Length; i++)
                {
                    if (a[i] < a[0])
                    {
                        min = a[i];
                    }
                }
                return min;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("输入数组元素的个数");
            int n=Convert.ToInt32(Console.ReadLine());
            Arr b=new Arr();
            b.a=new int[n];
            for (int i=0; i<n; i++)
            {
                //Console.WriteLine(b.a.Length);
                Console.WriteLine("请输入数组第" + (i+1) + "数的值");
                b.a[i]=Convert.ToInt32(Console.ReadLine());
                //Console.ReadKey();
            }
            Console.WriteLine(b.getSum());
            Console.WriteLine(b.getAve());
            Console.WriteLine(b.getMax());
            Console.WriteLine(b.getMin());
            Console.ReadKey();
        }
    }
}
