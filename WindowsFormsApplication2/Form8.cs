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
    public partial class Form8 : Form
    {
        int x = 0;
        Form F1;
        String Name;
        
        String OP;
        public static string sqlcon= "Data Source=OLIVER95\\SQLEXPRESS;Initial Catalog=C#TestDB;Integrated Security=True";
        public Form8(Form f1,String name,String op)
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
            F1 = f1;
            Name = name;
            OP = op;
            refresh(dataGridView1);
       
        }
        public static void refresh(DataGridView dataGridView1)
        {
            dataGridView1.Rows.Clear();
            SqlConnection con = new SqlConnection(sqlcon);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM User_Table", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            int x = dt.Rows.Count;
            String ID;
            String FName;
            String UName;
            String Email;
            String Pass;
            String Permission;

            for (int i = 0; i < x; i++)
            {
                ID = dt.Rows[i][0].ToString();
                FName = dt.Rows[i][1].ToString();
                UName = dt.Rows[i][2].ToString();
                Email = dt.Rows[i][3].ToString();
                Pass = dt.Rows[i][4].ToString();
                Permission = dt.Rows[i][5].ToString();
                dataGridView1.Rows.Add(ID, FName, UName, Email, Pass, Permission);

            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(sqlcon);
            String ID=textBox1.Text.ToString();
            String FName = textBox2.Text.ToString();
            String UName = textBox3.Text.ToString();
            String Email = textBox4.Text.ToString();
            String Pass = textBox5.Text.ToString();
            String Per = comboBox1.Text.ToString();
            try
            {
                if (!textBox1.Text.Equals(""))
                {
                    if (!textBox2.Text.Equals(""))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE User_Table SET Full_Name='" + FName + "' where ID = '" + ID + "' ;", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        refresh(dataGridView1);
                    }
                    if (!textBox3.Text.Equals(""))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE User_Table SET UserName= '" + UName + "' where ID = '" + ID + "' ;", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        refresh(dataGridView1);
                    }
                    if (!textBox4.Text.Equals(""))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE User_Table SET Email= '" + Email + "' where ID = '" + ID + "' ;", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        refresh(dataGridView1);
                    }
                    if (!textBox5.Text.Equals(""))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE User_Table SET Password= '" + Pass + "' where ID = '" + ID + "' ;", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        refresh(dataGridView1);
                    }
                    if (!Per.Equals(""))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE User_Table SET Permission= '" + Per + "' where ID = '" + ID + "' ;", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        refresh(dataGridView1);
                    }
                }
                else
                {
                    MessageBox.Show("Enter The ID");
                }
            }
            catch (Exception ifc)
            {
                MessageBox.Show(ifc.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String ID = textBox1.Text.ToString();
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            SqlDataAdapter sda1 = new SqlDataAdapter("DELETE  FROM User_Table where ID='" + ID + "'", con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            con.Close();
            refresh(dataGridView1);
        }

        private void Form8_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            if (x == 0)
            {
                F1.Close();   
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            x = 1;
            Form5 f = new Form5(F1, Name, OP);
            f.Show();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


       

    }
}
