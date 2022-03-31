using Microsoft.VisualStudio.TestTools.UnitTesting;
using order;
using System;

namespace OrderService
{
    [TestClass]                                                        
    public class UnitTest1
    {
       
        Order order1 = new Order(1, "食品", 1000, "小康");
        Order order2 = new Order(2, "电子产品", 5000, "谢劲松");
        OrderDetail milk = new OrderDetail("牛奶", 5, 5);
        OrderDetail orange = new OrderDetail("橘子", 10, 2);
        OrderDetail iphone = new OrderDetail("手机", 1, 4000);
        OrderDetail computer = new OrderDetail("电脑", 1, 6000);
        orderService service1,service2;
      
        public void init()
    {
      order1.orderDetail.Add(milk);
      order1.orderDetail.Add(orange);
      order2.orderDetail.Add(computer);
      order2.orderDetail.Add(iphone);

    }
    [TestMethod]
        public void TestAddOrder1()
        {
          orderService service1 = new orderService();
          service1.AddOrder(order1);
        }
    [TestMethod]
    public void TestAddOrder2()
    {
      orderService service1 = new orderService();
      service1.AddOrder(null);
    }
    [TestMethod]
    public void TestRemoveOrder1()
    {
      orderService service1 = new orderService();
      service1.AddOrder(order1);
      service1.removeOrder(1);//exist
    }
    [TestMethod]
    public void TestRemoveOrder2()
    {
      orderService service1 = new orderService();
      service1.removeOrder(100);//not exist
    }

    [TestMethod]
    public void ShowOrder1()
    {
      orderService service1 = new orderService();
      service1.AddOrder(order1);
      service1.ShowOrder();//
    }
    [TestMethod]
    public void ShowOrder2()
    {
      orderService service2 = new orderService();
      service2.ShowOrder();//no date
    }
  }
}
