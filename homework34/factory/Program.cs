using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace factory
{
    public abstract class Shape
    {
        public double a;
        public double b;
        public double c;
        public abstract double getArea();
       
    }
    class Square : Shape
    {
        public override double getArea()
        {
            Console.Write("正方形的面积是：");
            return a * a;
        }
    }
    class TanRectangcle : Shape
    {
        public override double getArea()
        {
            Console.Write("长方形的面积是：");
            return a * b;
        }
    }
    class Delta : Shape
    {
        public override double getArea()
        {
            double p = (a + b + c) / 2;
            Console.Write("三角形的面积是：");
            return Math.Sqrt(p*(p-a)*(p-b)*(p-c));
        }
    }
    public class shapeFactory
    {
        public static Shape createShape(string s)
        {
            Shape shape = null;
            if (s == "长方形")
                shape = new TanRectangcle();
            else if (s == "正方形")
                shape = new Square();
            else if (s == "三角形")
                shape = new Delta();
            else
                throw new ArgumentException("输入有误");
            return shape;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入形状名称：");
            string s =Console.ReadLine();
            Shape shape =shapeFactory.createShape(s);// 创建对象
            shape.a = 10;
            shape.b = 5;
            shape.c = 10;   
            Console.WriteLine(shape.getArea());
            Console.ReadKey();
        }
    }
}
