using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace WindowsFormsApplication2
{
    public partial class Form4 : Form
    {
        public string sqlcon = "Data Source=OLIVER95\\SQLEXPRESS;Initial Catalog=C#TestDB;Integrated Security=True";
        public Form4()
        {
            InitializeComponent();
        }

        private void SplashScreen()
        {
            Application.Run(new SplashScreen());
        }
        
     

        private void button1_Click(object sender, EventArgs e)
        {
            String ua = textBox1.Text;
            String ps = textBox2.Text;


            SqlConnection con = new SqlConnection(sqlcon);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM User_Table where UserName='" + textBox1.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (textBox1.Text.Equals("") || textBox2.Text.Equals(""))
            {
                MessageBox.Show("Enter the Password and Username");
            }

            else if (ua.Equals("admin") && ps.Equals("admin"))
            {
                Form5 f5 = new Form5(this,"Oliver","Lord");
                this.Hide();
                f5.Show();


            }
            else
            {
                try
                {
                    if (dt.Rows[0][2].Equals(ua) && dt.Rows[0][4].Equals(ps))
                    {
                        this.Hide();
                        Form5 f5 = new Form5(this, ua, dt.Rows[0][5].ToString());
                        f5.Show();
                    }
                    else
                    {
                        MessageBox.Show("Username or Password is Wrong");
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Password Or Username Is Wrong");
                }
            }
                  
           


        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

      

        

     

       
    }
}
