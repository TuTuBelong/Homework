using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using order;
using MySql.Data.MySqlClient;
namespace orderForm
{
    public partial class Form1 : Form
    {
        orderService service = new orderService();
    public Form1()
        {
            InitializeComponent();   
      Order order1 = new Order(1, "食品", 1000, "小康");
      Order order2 = new Order(2, "电子产品", 5000, "谢劲松");
      OrderDetail milk = new OrderDetail("牛奶", 5, 5);
      OrderDetail orange = new OrderDetail("橘子", 10, 2);
      OrderDetail iphone = new OrderDetail("手机", 1, 4000);
      OrderDetail computer = new OrderDetail("电脑", 1, 6000);
      order1.orderDetail.Add(milk);
      order1.orderDetail.Add(orange);
      order2.orderDetail.Add(computer);
      order2.orderDetail.Add(iphone);
      service.orderList.Add(order1);
      service.orderList.Add(order2);
      orderBindingSource.DataSource = service.orderList;
      queryAll();
      

    }
    private void connect()
    {
      string connStr = "Database=register;Data Source=127.0.0.1;port=3306;User Id=root;";
      MySqlConnection conn = new MySqlConnection(connStr);//创建Connection对象
      conn.Open();
    }
    private void queryAll()
    {
      Order thisOrder = (Order)orderBindingSource.Current;
      dataGridView2.DataSource = thisOrder.orderDetail;
    }
    private void panel2_Paint(object sender, PaintEventArgs e)
    {
      
    }

    private void panel1_Paint(object sender, PaintEventArgs e)
    {

    }

    private void btnQuery_Click(object sender, EventArgs e)
    {
      switch (comboBox1.SelectedIndex)
      {
        case 0:
          orderBindingSource.DataSource = service.orderList;
          break;
          case 1:
          int i;
          bool x = int.TryParse(textBox1.Text,out i);
          orderBindingSource.DataSource =
               service.orderList.Where(s => s.Id == i).ToList<Order>();
          break;
        case 2:
          orderBindingSource.DataSource =
               service.orderList.Where(s => s.OrderName == textBox1.Text).ToList<Order>();
          break;
        case 3:
          int j;
          bool y= int.TryParse(textBox1.Text, out j);
          orderBindingSource.DataSource =
               service.orderList.Where(s => s.Price == j).ToList<Order>();
          break;

      }
     
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
  
    }

    private void btnCreat_Click(object sender, EventArgs e)
    {
     Form2 form2 = new Form2();
      form2.ShowDialog();
      if(form2.orders.Count != 0)
      {
        foreach(Order a in form2.orders)
        {
          service.orderList.Add(a);
        }
      }
      dataGridView1.DataSource = null;
      dataGridView1.DataSource = service.orderList;
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      Order thisOrder=(Order)orderBindingSource.Current;
      service.removeOrder(thisOrder.Id);
      dataGridView1.DataSource = null;
      dataGridView1.DataSource = service.orderList;
    }

    private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {

    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void btnImport_Click(object sender, EventArgs e)
    {
      OpenFileDialog FileDialog = new OpenFileDialog();
      FileDialog.ShowDialog();
      service.import(FileDialog.FileName);
      orderBindingSource.DataSource = service.orderList;
      dataGridView1.DataSource = null;
      dataGridView1.DataSource = orderBindingSource;
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
      SaveFileDialog FileDialog = new SaveFileDialog();
      FileDialog.ShowDialog();
      service.export();
    }

    private void orderId_TextChanged(object sender, EventArgs e)
    {

    }
  }
  
}
