using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace WindowsFormsApplication2
{
    public partial class Form6 : Form
    {
        int x = 0;
        Form f1;
        String Name;
        String OP;
        public static string sqlcon= "Data Source=OLIVER95\\SQLEXPRESS;Initial Catalog=C#TestDB;Integrated Security=True";
        public Form6(Form f,String name,String op)
        {
           // try
           // {
           //     StreamReader st = new StreamReader("C:\\Program Files (x86)\\oliver\\My Product Name\\SQLCON\\SQLCONN.txt");
           //     String line = "";
           //     while ((line = st.ReadLine()) != null)
           //     {
           //
           //         sqlcon = line;
           //     }
           // }
           // catch (Exception fileE)
           // {
           //     MessageBox.Show(fileE.Message);
           // }
            InitializeComponent();
            Name=name;
            OP=op;
            f1=f;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String Fname, Uname, Pass, Rpass, Email;
            String Per="";
            Fname = textBox1.Text;
            Uname = textBox2.Text;
            Email = textBox3.Text;
            Pass  = textBox4.Text;
            Rpass = textBox5.Text;
            if (Pass.Equals(Rpass) && (!Pass.Equals("") || !Pass.Equals("")))
            {
                if (checkBox1.Checked == true )
                {
                    Per = checkBox1.Text;
                }
                if (checkBox2.Checked == true)
                {
                    Per = checkBox2.Text;
                }
                if (checkBox3.Checked == true)
                {
                    Per = checkBox3.Text;
                }
                else
                {
                    label7.Visible = true;
                    label7.Text = "Please Select User Permition";

                  
                }
                if(!Per.ToString().Equals(""))
                {
                    try
                    {
                        SqlConnection conn = new SqlConnection(sqlcon);
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO User_Table (Full_Name,UserName,Email,Password,Permission) VALUES ('" + Fname + "','" + Uname + "','" + Email + "','" + Pass + "','"+Per+"');", conn);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Inserting Data Successfully");
                        conn.Close();
                        label7.Visible = true;
                        label7.Text = "User Added Successfully";
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Exception Occre while creating table:"); ;
                    }

                }
               

            }
            else
            {
                if(Pass.Equals("") &&Pass.Equals(""))
                {
                     label7.Visible = true;
                label7.Text = "Password Please";
                }
                else{

                label7.Visible = true;
                label7.Text = "Password Do not Match";
                }
               
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {

                checkBox1.Checked = false;
                checkBox3.Checked = false;
                // checkBox2.Checked = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                 checkBox2.Checked = false;
                checkBox3.Checked = false;
            } 
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            x = 1;
            Form5 f5 = new Form5(f1,Name,OP);
            f5.Show();
            this.Close();
        }

        private void Form6_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (x == 0)
            {
                // this.Close();
                f1.Close();
            }
        }
    }
}
