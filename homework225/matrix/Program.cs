using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrix
{
    public class arr
    {
        public int[] []a;
        public Boolean isMatrix()
        {
            for(int i = 0; i < a.Length - 1; i++)
            {
                for(int j = 0; j < a[i].Length-1; j++)
                {
                    if(a[i][j] != a[i + 1][j + 1])
                    {
                        return false;
                    } 
                }
            }
            return true;
        }
            
    }
    
    internal class Program
    {
        static void Main(string[] args)
        {
           
            arr b = new arr();
            b.a = new int[][] { new int[]{ 1, 2, 3, 4 }, new int []{ 4, 1, 2, 3 }, new int []{ 9, 4, 1, 2 } };
            //Console.WriteLine("请输入矩阵的行数：");
            //int m = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("请输入矩阵的列数;");
            //int n = Convert.ToInt32(Console.ReadLine());
            //b.a = new int[m][];
            //for (int i = 0; i < m - 1; i++)
            //{
            //    for (int k = 0; k < n - 1; k++)
            //    {
            //        Console.WriteLine("请输入第" + (i + 1) + "行,第" + (k + 1) + "列的值");
            //        b.a[i][k] =Convert.ToInt32(Console.ReadLine());
            //        Console.ReadKey();
            //    }
            //}
           if(b.isMatrix() == true)
            
                Console.WriteLine("该矩阵为托普利茨矩阵");
           else
                Console.WriteLine("该矩阵不是托普利茨矩阵")
            ;
            
            Console.ReadKey();  
        }
    }
}
