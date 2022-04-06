using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cayleyTree
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(graphics==null) graphics=this.CreateGraphics();
            drawCayLeyTree(deep,200,310,leng,-Math.PI/2);
            
        }
        private Graphics graphics;
        int deep;
        int leng ;
        double th1;
        double th2 ;
        double per1 ;
        double per2 ;
        private Pen penColor = Pens.Blue;
        void drawCayLeyTree(int n,double x0,double y0,double leng,double th)
        {
            if (n == 0) return;
            double x1=x0+leng*Math.Cos(th);
            double y1=y0+leng*Math.Sin(th);
            drawLine(x0, y0, x1, y1);
            drawCayLeyTree(n - 1, x1, y1, per1 * leng, th + th1);
            drawCayLeyTree(n - 1, x1, y1, per2 * leng, th - th2);

        }
        void drawLine(double x0,double y0,double x1,double y1)
        {
            graphics.DrawLine(penColor,(int)x0, (int)y0, (int)x1, (int)y1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem)
            {
                case "red":
                    penColor = Pens.Red;
                    break;
                case "black":
                    penColor= Pens.Black;
                    break;
                case "green":
                    penColor = Pens.Green;
                    break;
                case "blue":
                    penColor = Pens.Blue;
                    break;
                case "orange":
                    penColor = Pens.Orange;
                    break;
                default:
                    break;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 0 && numericUpDown1.Value < 21)
            {
                deep = (int)numericUpDown1.Value;
            }
            else
                throw new ArgumentOutOfRangeException($"invaild deep value!");
        }
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            leng = (int)numericUpDown2.Value;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string a = textBox2.Text;
            double i;
            if (a != "")
            {
                i = double.Parse(a);

                if (i >= 0.39&& i < 0.71)
                {
                    per2 = i;
                }
                else
                    Console.WriteLine("inviad value!");
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
            string a = textBox3.Text;
            double i;
            if (a != "")
            {
                i = double.Parse(a);

                if (i > 0.39 && i < 0.71)
                {
                    per1 = i;
                }
                else
                    Console.WriteLine("inviad value!");
            }

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string a = textBox4.Text;
            double i;
            if (a != "")
            {
                i = double.Parse(a);

                if (i > 29 && i < 61)
                {
                    th2 = i*Math.PI/180;
                }
                else
                    Console.WriteLine("inviad value!");
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string a = textBox5.Text;
            double i;
            if (a != "")
            {
                i = double.Parse(a);

                if (i > 29 && i < 61)
                {
                    th1 = i * Math.PI / 180;
                }
                else
                    Console.WriteLine("inviad value!");
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
     
      comboBox1.DropDownStyle=ComboBoxStyle.DropDownList;
      
    }
  }
}
