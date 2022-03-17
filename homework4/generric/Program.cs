using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generric
{
    public class Node<T>
    {
        public  Node<T> Next{ get; set; }
        public T Data { get; set; }
        public Node(T t)
        {
            Next = null;
            Data = t;
        }
    }
    public class GenericList<T>
    {
        private Node<T> head;
        private Node<T> tail;   
        public GenericList()
        {
            head = tail = null;
        }
        public Node<T> Head
        {
            get => head;
        }
        public void add(T t)
        {
            Node<T> n = new Node<T>(t);
            if(tail == null)
            {
                head=tail = n;
            }
            else
            {
                tail.Next = n;
                tail = n;
            }
            
        }
        public void printNode(Action<T>action)
        {
            while(head != null)
            {
                action(head.Data);
                head = head.Next;
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
           GenericList<int> list = new GenericList<int>();
            for(int i = 0; i < 10; i++)
            {
                list.add(i);   
            }
            int sum=0;
            int min=0;
            int max=0;
            list.printNode((m) =>
            {
                Console.WriteLine(m);
                sum += m;
                min = Math.Min(min, m);
                max = Math.Max(max, m);
            });
            Console.WriteLine("最大值" + max);
            Console.WriteLine("最小值" + min);
            Console.WriteLine("总和" + sum);
            Console.ReadKey();
        }
    }
}
