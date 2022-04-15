using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using orderForm;
using order;

namespace orderForm
{
  
  public partial class Form2 : Form
  {
    public List<Order> orders = new List<Order>();
    public Form2()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      int i;
      bool x = int.TryParse(textBox2.Text, out i);
      string j = textBox1.Text;
      int k;
      bool y = int.TryParse(textBox3.Text, out k);
      string l=textBox4.Text;
     Order order = new Order(i,j,k,l);
      orders.Add(order);
      this.Close();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {

    }

    private void textBox2_TextChanged(object sender, EventArgs e)
    {

    }
  }
}
