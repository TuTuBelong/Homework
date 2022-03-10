using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shape
{
    public class Square
    {
        public double side;
        public Square(double side)
        {
            this.side = side;
        }
        public Boolean isSquare()
        {
            if(side != 0)
                return true;
            else
                return false;
                }
        public double getArea()
        {
            return side*side;
        }
    }
    public class Rectangcle
    {
        public double length;
        public double width;
        public Rectangcle(double length,double width)
        {
            this.length = length;
            this.width = width;
        }
        public Boolean isRectangcle()
        {
            if (length != width&&length!=0&&width!=0)
            {
                return true;
            }
            else
                return false;
        }
        public double getArea()
        {
            return length * width;
        }
    }
    public class Delta
    {
        public double a;
        public double b;
        public double c;
        public Delta(double a,double b,double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
        public Boolean isDelta()
        {
            if (a + b > c && a + c > b && b + c > a)
            {
                return true;
            }
            else
                return false;
        }
        public double getArea()
        {
            double p = (a + b + c) / 2;
            double s= Math.Sqrt(p*(p-a)*(p-b)*(p-c));
            return s;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Square square1 = new Square(6);
            if(square1.isSquare()==true)
            Console.WriteLine(square1.getArea());
            Rectangcle rectangcle1=new Rectangcle(6,8);
            if(rectangcle1.isRectangcle()==true)
            Console.WriteLine(rectangcle1.getArea());
            Delta delta1 = new Delta(3, 5, 7);
            if(delta1.isDelta()==true)  
            Console.WriteLine(delta1.getArea());
            Console.ReadKey();  
        }
    }
}
