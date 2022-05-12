using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data;

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

    }
    public class OrderDetail
    {

        public string Name { get; set; }
        public int Number { get; set; }
        public int Price { get; set; }
        public int OrderId { get; set; }
        public OrderDetail(string name, int number, int price,int orderId)
        {
            this.Name = name;
            this.Number = number;
            this.Price = price;
            this.OrderId = orderId;
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
                int index = 0;
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
        public void export()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream("order.xml", FileMode.Create))
            {
                serializer.Serialize(fs, this);
            }
        }
        public void import(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                List<Order> list = (List<Order>)xmlSerializer.Deserialize(fs);

            }

        }
    }
    internal class Program
    {
        private static void LINQInDataSet()
        {
            using (MySqlConnection conn = GetConnection())
            {
                String sql = "SELECT * FROM orders";
                using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, conn))
                {
                    using (DataSet ds = new DataSet())
                    {
                        dataAdapter.Fill(ds);
                        DataRow[] rows = ds.Tables[0].Select("price=5");
                        for (int i = 0; i < rows.Length; i++)
                        {
                            Console.WriteLine($"{rows[i][0]},{rows[i][1]},{rows[i][2]}");
                        }
                    }
                }
            }
        }
    //编辑订单项（通过id）
        private static void EditInDataSet(int m)
        {
            using (MySqlConnection conn = GetConnection())
            {
                String sql = "SELECT * FROM orders";
                using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, conn))
                {
                    MySqlCommandBuilder cmdBuilder =
                      new MySqlCommandBuilder(dataAdapter);
                    using (DataSet ds = new DataSet())
                    {
                        dataAdapter.Fill(ds);
                        DataRow[] rows = ds.Tables[0].Select("id="+m);
                        for (int i = 0; i < rows.Length; i++)
                        {
                            rows[i].BeginEdit();
                            rows[i][1] = "作业";//订单名称
                            rows[i][2] = 10000;//价格
                            rows[i][3] = "xjs";//客户名称
                            rows[i].EndEdit();
                        }
                        dataAdapter.Update(ds);
                    }
                }
            }
        }

        private static void AddRowInDataSet(Order a)
        {
            using (MySqlConnection conn = GetConnection())
            {
                String sql = "SELECT * FROM Orders";
                using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, conn))
                {
                    MySqlCommandBuilder cmdBuilder = new MySqlCommandBuilder(dataAdapter);
                    using (DataSet ds = new DataSet())
                    {
                        dataAdapter.Fill(ds);
                        DataRow newRow = ds.Tables[0].NewRow();
                        newRow[0] = a.Id;
                        newRow[1] = a.OrderName;
                        newRow[2] = a.Price;
                        newRow[3] = a.ClientName;
                        ds.Tables[0].Rows.Add(newRow);
                        dataAdapter.Update(ds);
                    }
                }
            }
        }
    private static void AddOrderDetails(OrderDetail a)
    {
      using (MySqlConnection conn = GetConnection())
      {
        String sql = "SELECT * FROM Details";
        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, conn))
        {
          MySqlCommandBuilder cmdBuilder = new MySqlCommandBuilder(dataAdapter);
          using (DataSet ds = new DataSet())
          {
            dataAdapter.Fill(ds);
            DataRow newRow = ds.Tables[0].NewRow();
            newRow[0] = a.Name;
            newRow[1] = a.Price;
            newRow[2] = a.Number;
            newRow[3]= a.OrderId;
            ds.Tables[0].Rows.Add(newRow);
            dataAdapter.Update(ds);
          }
        }
      }
    }


    private static MySqlConnection GetConnection()
        {
            MySqlConnection connection = new MySqlConnection(
                "datasource=localhost;username=root;" +
                "password=123456789;database=order;charset=utf8mb4");
            connection.Open();
            return connection;
        }

        private static void QueryAllOrders()
        {
            Console.WriteLine("QueryAllOrders");
            using (MySqlConnection conn = GetConnection())
            {
                String sql = "SELECT * FROM orders";
                using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, conn))
                {
                    using (DataSet ds = new DataSet())
                    {
                        dataAdapter.Fill(ds);
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            foreach (object field in row.ItemArray)
                            {
                                Console.Write(field + "\t");
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
            //Console.WriteLine(ds.Tables[0].Rows[0][1]);
        }
        private static void QueryOrders(int j)
        {
            Console.WriteLine("QueryOrders");
            using (MySqlConnection conn = GetConnection())
            {
                String sql = "SELECT * FROM orders";
                using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, conn))
                {
                    using (DataSet ds = new DataSet())
                    {
                        dataAdapter.Fill(ds);
                        DataRow[] rows = ds.Tables[0].Select("id="+j);
                        for (int i = 0; i < rows.Length; i++)
                        {
                            Console.WriteLine($"{rows[i][0]},{rows[i][1]},{rows[i][2]},{rows[i][3]}");
                           
                        }
                    }
                }
            }
            //Console.WriteLine(ds.Tables[0].Rows[0][1]);
        }
    private static void QueryOrderDetails(int j)
    {
      Console.WriteLine("QueryOrders");
      using (MySqlConnection conn = GetConnection())
      {
        String sql = "SELECT * FROM orders";
        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, conn))
        {
          using (DataSet ds = new DataSet())
          {
            dataAdapter.Fill(ds);
            DataRow[] rows = ds.Tables[0].Select("id=" + j);
            for (int i = 0; i < rows.Length; i++)
            {
              Console.WriteLine($"{rows[i][0]},{rows[i][1]},{rows[i][2]},{rows[i][3]}");
            }
          }
        }
      }
      //Console.WriteLine(ds.Tables[0].Rows[0][1]);
    }
    static void Main(string[] args)
        {
            Program program = new Program();
            //QueryOrders();
            //LINQInDataSet();
            
            //QueryOrders();
            //EditInDataSet();
            //QueryOrders();
            orderService service = new orderService();
            bool i = true;
            while (i)
            {

                Order order1 = new Order(5, "水果", 1000, "小康");
                Order order2 = new Order(6, "电子产品", 5000, "劲松");
                 OrderDetail orderDetail1 = new OrderDetail("apple", 100, 2, 5);
                Console.WriteLine("1增加订单和订单明细，2删除订单，3按订单号查询订单，4显示所有订单，5.按订单金额查询，6.按订单名称查询，7.编辑订单项");
                string choose1 = Console.ReadLine();
                switch (choose1)
                {
                    case "1":
                        AddRowInDataSet(order1);//向数据库添加order1
                        AddRowInDataSet(order2);
                        AddOrderDetails(orderDetail1);//向数据库内添加订单明细
                        QueryAllOrders();//查询数据库内orders
                       QueryOrderDetails(5);
                        //Console.WriteLine(service.orderList.Count);
                        //service.orderList.ForEach(o => Console.WriteLine(o.ClientName));

                        break;
                    case "2": service.removeOrder(1); break;
                    case "3":
                        Console.WriteLine("请输入查询的订单编号");
                        int j1 = Convert.ToInt32(Console.ReadLine());
                       
                        QueryOrders(j1);
                        break;
                    case "4": QueryAllOrders();
                        break;
        
                    case "7":
                        Console.WriteLine("请输入要修改的订单号");
                        int m = Convert.ToInt32(Console.ReadLine());
                        QueryOrders(m);
                        EditInDataSet(m);
                        QueryOrders(m); 
                        break;
                    case "8": i = false; break;
                    default: Console.WriteLine("输入错误"); break;
                }
            }
            Console.ReadKey();
        }
    }
}

