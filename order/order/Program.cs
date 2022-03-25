using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace order
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderName { get; set; }
        public int Price { get; set; }
        public string ClientName { get; set; }
        public List<OrderDetail> orderDetail = new List<OrderDetail>();
        public Order()
        {
            this.Id = 0;
            this.OrderName = string.Empty;
            this.Price = 0;
            this.ClientName = string.Empty;
        }
        public int CompareTo(object obj)
        {
            Order a = obj as Order;
            return this.Id.CompareTo(a.Id);
        }
        public override bool Equals(object obj)
        {
            Order o = obj as Order;
            return this.Id == o.Id;
        }
        public void getAllPrice()  //计算总价
        {
            int i = 0;
            foreach (OrderDetail a in this.orderDetail)
            {
                i = i + a.getPrice();
            }
            this.Price = i;

        }
          public void addOrderDetail(OrderDetail a)   //添加订单项
          {
            orderDetail.Add(a);
          }
        public void showOrderDetail()  //展示订单项
        {
            Console.WriteLine("序号 名称 数量 单价");
            foreach (OrderDetail a in this.orderDetail)
            {
                Console.WriteLine("{0} {1} {2} {3}", this.orderDetail.IndexOf(a)+1, a.Name, a.Number, a.Price);
            }
        }
    }
        public class OrderDetail               //订单明细项
        {
            private string name;
            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    name = value;
                }
            }
            private int number;
            public int Number
            {
                get
                {
                    return number;
                }
                set
                {
                    if (value >= 0) number = value;
                    else Console.WriteLine("数量不应该小于0");
                }
            }
            private int price;
            public int Price
            {
                get
                {
                    return price;
                }
                set
                {
                    price = value;
                }
            }

            public OrderDetail()//无参构造函数
            {
                this.Name = string.Empty;
                this.Number = 0;
                this.Price = 0;
            }

            public OrderDetail(string name, int number, int price)
            {
                this.name = name;
                this.number = number;
                this.price = price;
            }
            public int getPrice()
            {
                return this.number * this.price;
            }
        //public int CompareTo(object obj)
        //{
        //    OrderDetail a = obj as OrderDetail;
        //    return this.name.CompareTo(a.name);
        //}
            public override bool Equals(object obj)
            {
                OrderDetail o = obj as OrderDetail;
                return this.name == o.name;
            }
    }
        class orderService
        {
                public List<Order> orderList = new List<Order>();
                 public void SearchOrder()
                {

                    //按1.2.3.4分别查询订单编号，金额范围，客户名称和订单名称
                    Console.WriteLine("请输入查询任务的编码：1.订单编号，2.订单金额，3.客户名称，4.订单名称");
                    int i = Convert.ToInt32(Console.ReadLine());
                    switch (i)
                    {
                        case 1:
                            Console.WriteLine("请输入查询的订单编号");
                            int j = Convert.ToInt32(Console.ReadLine());
                            var query1 = from s1 in orderList
                                         where j == s1.Id
                                         orderby s1.Price
                                         select s1;
                            List<Order> list1 = query1.ToList();
                            foreach (Order a in list1)
                            {
                                Console.Write("订单名称" + a.OrderName + " ");
                                Console.Write("订单编号" + a.Id + " ");
                                Console.Write("订单总价" + a.Price + " ");
                                Console.Write("客户名称" + a.ClientName + "\n");
                                a.showOrderDetail();
                            }
                            Console.ReadKey();
                            break;
                        case 2:
                            Console.WriteLine("请输入查询金额的最大值");
                            int max = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("请输入查询金额的最小值");
                            int min = Convert.ToInt32(Console.ReadLine());
                            var query2 = from s2 in orderList
                                         where s2.Price <= max && s2.Price >= min
                                         orderby s2.Price
                                         select s2;
                            List<Order> list2 = query2.ToList();
                            foreach (Order a in list2)
                            {
                                Console.Write("订单名称" + a.OrderName + " ");
                                Console.Write("订单编号" + a.Id + " ");
                                Console.Write("订单总价" + a.Price + " ");
                                Console.Write("客户名称" + a.ClientName + "\n");
                                a.showOrderDetail();
                            }
                            Console.ReadKey();
                            break;
                        case 3:
                            Console.WriteLine("请输入要查询的订单名称");
                            string k = Console.ReadLine();
                            var query3 = from s3 in orderList
                                         where s3.OrderName == k
                                         orderby s3.Price
                                         select s3;
                            List<Order> list3 = query3.ToList();
                            foreach (Order a in list3)
                            {
                                Console.Write("订单名称" + a.OrderName + " ");
                                Console.Write("订单编号" + a.Id + " ");
                                Console.Write("订单总价" + a.Price + " ");
                                Console.Write("客户名称" + a.ClientName + "\n");
                                a.showOrderDetail(); 
                            }
                            Console.ReadKey();
                            break;
                        case 4:
                            Console.WriteLine("请输入客户名称");
                            string n = Console.ReadLine();
                            var query4 = from s4 in orderList
                                         where s4.ClientName == n
                                         orderby s4.Price
                                         select s4;
                            List<Order> list4 = query4.ToList();
                            foreach (Order a in list4)
                            {
                                Console.Write("订单名称" + a.OrderName + " ");
                                Console.Write("订单编号" + a.Id + " ");
                                Console.Write("订单总价" + a.Price + " ");
                                Console.Write("客户名称" + a.ClientName + "\n");
                                a.showOrderDetail();
                            }
                            Console.ReadKey();
                            break;
                        default:
                            Console.WriteLine("输入错误");
                            Console.ReadKey();
                            break;
                    }
                }
            public void AddOrder()
            {
                Console.WriteLine("请输入订单号");
                int i = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("请输入订单价格");
                int j = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("请输入订单名称");
                string m = Console.ReadLine();
                Console.WriteLine("请输入客户名称");
                string n = Console.ReadLine();
                Order a = new Order();
                a.Id = i;
                a.OrderName = m;
                a.Price = j;
                a.ClientName = n;
                bool judge = true;
                bool same = false;
                foreach (Order k in this.orderList)
                {
                    if (k.Equals(a)) same = true;
                }
                if (same) Console.WriteLine("订单号重复");
                else
                {
                    while (judge && !same)
                    {
                        Console.WriteLine("请输入物品名称：");
                        string name = Console.ReadLine();
                        Console.WriteLine("请输入购买数量：");
                        int number = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("请输入单价：");
                        int price = Convert.ToInt32(Console.ReadLine());
                        OrderDetail b = new OrderDetail(name,number,price);
                         //a.orderDetail.Add(b);
                        bool flag=false;
                        foreach (OrderDetail k in a.orderDetail)
                        {
                            if (k.Equals(b))
                                flag = true;
                        }
                        if(flag)
                            Console.WriteLine("订单内容重复");
                        else
                            a.orderDetail.Add(b);
                        Console.WriteLine("是否继续添加订单项：");
                        string x = Console.ReadLine();
                        if (x == "否") judge = false;
                        else if (x == "是") continue;
                        else if (x != "否" && x != "是")
                        {
                            Exception e = new Exception();
                            throw e;
                        }
                    }
                    orderList.Add(a);
                    Console.WriteLine("建立成功");
                }
            }
            public void removeOrder()           //删除订单
            {
                try
                {
                    Console.WriteLine("输入订单号删除订单：");
                    int id = Convert.ToInt32(Console.ReadLine());
                    int index = 0;
                    foreach (Order a in this.orderList)
                    {
                        if (a.Id == id) index = this.orderList.IndexOf(a);
                    }
                    this.orderList.RemoveAt(index); Console.WriteLine("删除成功"); Console.WriteLine("-----------------"); 
                    Console.WriteLine("输入错误"); 
                    
                }
                catch
                {
                    Console.WriteLine("输入错误");
                }

            }
            public void ShowOrder()
            {
                foreach (Order a in orderList)
                {
                    Console.Write("订单名称"+a.OrderName + " ");
                    Console.Write("订单编号"+a.Id + " ");
                    Console.Write("订单总价"+a.Price + " ");
                    Console.Write("客户名称"+a.ClientName + "\n");
                    a.showOrderDetail();
                }
            }
        }
        internal class Program
        {
            static void Main(string[] args)
            {
                Order order = new Order();
                orderService service = new orderService();
                bool a = true;
            while(a)
            {
                Console.WriteLine("输入1增加订单，输入2删除订单，输入3查询订单，输入4显示所有订单");
                string choose1 = Console.ReadLine();
                switch (choose1)
                {
                    case "1": service.AddOrder(); break;
                    case "2": service.removeOrder(); break;
                    case "3": service.SearchOrder(); break;
                    case "4": service.ShowOrder(); break;
                    case "5": a = false;break;
                    default: Console.WriteLine("输入错误"); break;
                }
            }
            Console.ReadKey();
            }
        }
    }

