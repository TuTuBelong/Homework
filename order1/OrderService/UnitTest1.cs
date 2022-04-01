using Microsoft.VisualStudio.TestTools.UnitTesting;
using order;
using System;
using System.Collections.Generic;

namespace OrderService
{
    [TestClass]                                                        
    public class UnitTest1
    {
       
    //    Order order1 = new Order(1, "食品", 1000, "小康");
    //    Order order2 = new Order(2, "电子产品", 5000, "谢劲松");
    //    OrderDetail milk = new OrderDetail("牛奶", 5, 5);
    //    OrderDetail orange = new OrderDetail("橘子", 10, 2);
    //    OrderDetail iphone = new OrderDetail("手机", 1, 4000);
    //OrderDetail computer = new OrderDetail("电脑", 1, 6000);
    [TestMethod]
        public void TestAddOrder1()
        {
          orderService service1 = new orderService();
          Order order1 = new Order(1, "食品", 1000, "小康");
          service1.AddOrder(order1);
          Assert.IsTrue(service1.orderList.Count==1);
        }
    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddOrdeqr2()
    {
      orderService service2 = new orderService();
      service2.AddOrder(null);
    }
    [TestMethod]
    public void TestRemoveOrder1()
    {
      orderService service3 = new orderService();
      Order order1 = new Order(1, "食品", 1000, "小康");
      service3.AddOrder(order1);
      Order order2 = new Order(2, "电子产品", 5000, "谢劲松");
      service3.AddOrder(order2);
      service3.removeOrder(2);//exist
      Assert.IsTrue(service3.orderList.Count ==1);
    }
    [TestMethod]
    public void TestRemoveOrder2()
    {
      orderService service4=new orderService();
      Order order1 = new Order(1, "食品", 1000, "小康");
      service4.AddOrder(order1);  
      service4.removeOrder(100);//not exist
      Assert.IsTrue(service4.orderList.Count ==1);
    }

    [TestMethod]
    public void ShowOrder1()
    {
      orderService service1=new orderService();
      Order order1=new Order(1, "食品", 1000, "小康");
      service1.AddOrder(order1);
      service1.ShowOrder();//
    }
    [TestMethod]
    public void ShowOrder2()
    {
      orderService service2 = new orderService();
      service2.ShowOrder();//no date
    }
    [TestMethod]
    public void TestSearch()
    {
      orderService service3 = new orderService();
      Order order1= new Order(1, "食品", 1000, "小康");
      Order order2 = new Order(2, "电子产品", 5000, "谢劲松");
      service3.AddOrder(order1);
      service3.AddOrder(order2);
      List<Order> list = service3.SearchId(1);
      Assert.AreEqual(order1,list[0]);
    }
  }
}
