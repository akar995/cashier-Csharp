using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form5 : Form
    {
        Form F4;
        int x = 0;
        String Namee="";
        String OP;
        public Form5(Form f,String name,String op)
        
        {
            
            InitializeComponent();
            Namee = name;
            OP = op;

            F4 =f ;
            String s = ("Wellcome "+op+" "+Namee).ToString(); ;
            label1.Text = s;
            if (op.Equals("Data Enter"))
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
            }
            if (op.Equals("Cashear"))
            {
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
            }
           

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            x =1;
           //this.Close();
            Form11 f1=new Form11(F4,Namee,OP);
            f1.Show();
            this.Close();

            

        }

       

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (x == 0)
            {
                // this.Close();
                F4.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            x = 1;
            
            //this.Close();
            Form6 f1 = new Form6(F4,Namee,OP);
            f1.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            x = 1;

            //this.Close();
            Form7 f1 = new Form7(F4, Namee, OP);
            f1.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            x = 1;
            Form8 f1 = new Form8(F4, Namee, OP);
            f1.Show();
            this.Close();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            x = 1;
            Form9 f1 = new Form9(F4, Namee, OP);
            f1.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            x = 1;
           F4.Show();
            this.Close();
        }

       

        private void button7_Click(object sender, EventArgs e)
        {
            x = 1;

            //this.Close();
            Form10 f1 = new Form10(F4, Namee, OP);
            f1.Show();
            this.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}