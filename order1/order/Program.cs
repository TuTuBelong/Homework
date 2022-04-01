using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
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
        public Order(int id, string i, int p, string j)
        {
            this.Id = id;
            this.OrderName = i;
            this.Price = p;
            this.ClientName = j;
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
        public override int GetHashCode()
        {
            return Id;
        }
        public int getAllPrice()
        {
            int i = 0;
            foreach (OrderDetail a in this.orderDetail)
            {
                i = a.getPrice();
            }
            return i;

        }
        public void addOrderDetail(OrderDetail a)
        {
            if (this.orderDetail.Contains(a))
            {
                throw new ApplicationException($"The goods ({a.Name}) exist in order {Id}");
            }
            orderDetail.Add(a);
        }
        public void showOrderDetail()
        {
            Console.WriteLine("序号 货物名称 数量 单价");
            foreach (OrderDetail a in this.orderDetail)
            {
                Console.WriteLine("{0} {1} {2} {3}", this.orderDetail.IndexOf(a) + 1, a.Name, a.Number, a.Price);
            }
        }
        public void export()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Order));
            using (FileStream fs = new FileStream("order.xml", FileMode.Create))
            {
                serializer.Serialize(fs, this);
            }
        }
        public void import(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order));
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                List<Order> list = (List<Order>)xmlSerializer.Deserialize(fs);
                
            }

        }
    }
    public class OrderDetail
    {

        public string Name { get; set; }
        public int Number { get; set; }
        public int Price { get; set; }
        public OrderDetail(string name, int number, int price)
        {
            this.Name = name;
            this.Number = number;
            this.Price = price;
        }
        public int getPrice()
        {
            return this.Number * this.Price;
        }
        public int CompareTo(object obj)
        {
            OrderDetail a = obj as OrderDetail;
            return this.Name.CompareTo(a.Name);
        }
        public override bool Equals(object obj)
        {
            OrderDetail o = obj as OrderDetail;
            return this.Name == o.Name;
        }
    }
    public class orderService
    {
        public List<Order> orderList = new List<Order>();
        public List<Order> SearchId(int j)
        {
            var query1 = from s1 in orderList
                         where j == s1.Id
                         orderby s1.Price
                         select s1;
            List<Order> list1 = query1.ToList();
            return list1;
        }
        public List<Order> SearchPrice(int max, int min)
        {
            var query2 = from s2 in orderList
                         where s2.Price <= max && s2.Price >= min
                         orderby s2.Price
                         select s2;
            List<Order> list2 = query2.ToList();
            return list2;
        }
        public List<Order> SearchOrderName(string name)
        {
            var query3 = from s3 in orderList
                         where s3.OrderName == name
                         orderby s3.Price
                         select s3;
            List<Order> list3 = query3.ToList();
            return list3;
        }
        public void AddOrder(Order a)
        {

            if (a == null)
            {
                throw new ApplicationException($"Invalid order!");
            }
            if (orderList.Contains(a))
            {
              throw new ApplicationException($"the order {a.Id} already exists!");
            }
            else
            {
              orderList.Add(a);
            }
            
        }
    
    public void removeOrder(int i)
    {
        try
        {
            
            int id = i;
            int index =0;
            foreach (Order a in this.orderList)
            {
          if (a.Id == id)
          {
            index = this.orderList.IndexOf(a);
            this.orderList.RemoveAt(index);
          }
        }
            

        }
        catch
        {
            Console.WriteLine("");
        }

    }
    public void ShowOrder()
    {
           
            foreach (Order a in orderList)
            {
                Console.Write("订单名称" + a.OrderName + " ");
                Console.Write("订单编号" + a.Id + " ");
                Console.Write("订单总价" + a.Price + " ");
                Console.Write("客户名称" + a.ClientName + "\n");
                a.showOrderDetail();
            }

        }
}
internal class Program
{
    static void Main(string[] args)
    {

            orderService service = new orderService();
            bool i = true;
        while (i)
        {
            
            Order order1 = new Order(1, "食品", 1000, "小康");
            Order order2 = new Order(2, "电子产品", 5000, "谢劲松");
            Console.WriteLine("1增加订单，2删除订单，3按订单号查询订单，4显示所有订单，5.按订单金额查询，6.按订单名称查询");
            string choose1 = Console.ReadLine();
            switch (choose1)
            {
                case "1":
                    
                    OrderDetail milk = new OrderDetail("牛奶", 5, 5);
                    OrderDetail orange = new OrderDetail("橘子", 10, 2);
                    OrderDetail iphone = new OrderDetail("手机", 1, 4000);
                    OrderDetail computer = new OrderDetail("电脑", 1, 6000);
                    order1.orderDetail.Add(milk);
                    order1.orderDetail.Add(orange);
                    order2.orderDetail.Add(computer);
                    order2.orderDetail.Add(iphone);
                    service.AddOrder(order1);
                    service.AddOrder(order2);
                        Console.WriteLine(order1.orderDetail.Count);
                        //Console.WriteLine(service.orderList.Count);
                        //service.orderList.ForEach(o => Console.WriteLine(o.ClientName));

                        break;
                case "2": service.removeOrder(1); break;
                case "3":
                    Console.WriteLine("请输入查询的订单编号");
                    int j1 = Convert.ToInt32(Console.ReadLine());
                    List<Order> list1 = service.SearchId(j1);
                    foreach (Order a in list1)
                    {
                        Console.Write("订单名称" + a.OrderName + " ");
                        Console.Write("订单编号" + a.Id + " ");
                        Console.Write("订单总价" + a.Price + " ");
                        Console.Write("客户名称" + a.ClientName + "\n");
                        a.showOrderDetail();
                    };
                    break;
                case "4": service.ShowOrder(); break;
                case "5":
                    Console.WriteLine("请输入查询价格的最大值");
                    int max = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("请输入查询价格的最小值");
                    int min = Convert.ToInt32(Console.ReadLine());
                    List<Order> list2 = service.SearchPrice(max, min);
                    foreach (Order a in list2)
                    {
                        Console.Write("订单名称" + a.OrderName + " ");
                        Console.Write("订单编号" + a.Id + " ");
                        Console.Write("订单总价" + a.Price + " ");
                        Console.Write("客户名称" + a.ClientName + "\n");
                        a.showOrderDetail();
                    }
                    break;
                case "6":
                    Console.WriteLine("请输入要查询的订单名称");
                    string k1 = Console.ReadLine();

                    List<Order> list3 = service.SearchOrderName(k1);
                    foreach (Order a in list3)
                    {
                        Console.Write("订单名称" + a.OrderName + " ");
                        Console.Write("订单编号" + a.Id + " ");
                        Console.Write("订单总价" + a.Price + " ");
                        Console.Write("客户名称" + a.ClientName + "\n");
                        a.showOrderDetail();
                    }
                    break;
                case "7": i = false; break;
                default: Console.WriteLine("输入错误"); break;
            }
        }
        Console.ReadKey();
    }
}
}

