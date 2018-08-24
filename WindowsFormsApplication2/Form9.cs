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
    public partial class Form9 : Form
    {
        int x = 0;
        Form F1;
        String Namee;
        String OP;
        public static string sqlcon= "Data Source=OLIVER95\\SQLEXPRESS;Initial Catalog=C#TestDB;Integrated Security=True";
        public Form9(Form f1,String name,String op)
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
            F1 = f1;
            Namee = name;
            OP = op;
            InitializeComponent();
            refresh(dataGridView1);
        }
        public static void refresh(DataGridView dataGridView1)
        {
            dataGridView1.Rows.Clear();
            SqlConnection con = new SqlConnection(sqlcon);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Item_Code", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            int x = dt.Rows.Count;
            String ID;
            String ICode;
            String IName;
            String Price;
            String DPrice;
            String Company;
            String Number;
           

            for (int i = 0; i < x; i++)
            {
                ICode = dt.Rows[i][0].ToString();
                IName = dt.Rows[i][1].ToString();
                Number = dt.Rows[i][2].ToString();
                Company = dt.Rows[i][3].ToString();
                Price = dt.Rows[i][4].ToString();
                ID = dt.Rows[i][6].ToString();
                DPrice = dt.Rows[i][5].ToString();
                dataGridView1.Rows.Add(ID,ICode,IName,Company,Number,Price,DPrice);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sqlcon);
            string ID = textBox1.Text;
            string ICode = textBox2.Text;
            string IName = textBox3.Text;
            string Company = textBox4.Text;
            string Number = textBox5.Text;
            string DinarPrice = textBox6.Text;
            string DollarPrice = textBox7.Text;

            try
            {
                if (!textBox1.Text.Equals(""))
                {
                    if (!textBox2.Text.Equals(""))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE Item_Code SET Item_Code='" + ICode + "' where ID = '" + ID + "' ;", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        refresh(dataGridView1);
                    }
                    if (!textBox3.Text.Equals(""))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE Item_Code SET Item_Name= '" + IName + "' where ID = '" + ID + "' ;", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        refresh(dataGridView1);
                    }
                    if (!textBox4.Text.Equals(""))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE Item_Code SET Company= '" + Company + "' where ID = '" + ID + "' ;", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        refresh(dataGridView1);
                    }
                    if (!textBox5.Text.Equals(""))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE Item_Code SET Number= '" + Number + "' where ID = '" + ID + "' ;", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        refresh(dataGridView1);
                    }
                    if (!textBox6.Text.Equals(""))
                    {
                        if (!textBox6.Text.Equals(""))
                        {
                            textBox7.Enabled = false;
                            double oneDinarPrice = 100 / double.Parse(textBox8.Text);
                            double total = double.Parse(textBox6.Text) * oneDinarPrice;
               

                            con.Open();
                            SqlCommand cmd = new SqlCommand("UPDATE Item_Code SET Price= '" + DinarPrice + "' where ID = '" + ID + "' ;", con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            refresh(dataGridView1);
                            con.Open();
                            SqlCommand cmdd = new SqlCommand("UPDATE Item_Code SET DPrice= '" + total + "' where ID = '" + ID + "' ;", con);
                            cmdd.ExecuteNonQuery();
                            con.Close();
                            refresh(dataGridView1);
                        }
                    }
                    else
                    {
                        if (!textBox7.Text.Equals(""))
                        {
                            textBox6.Enabled = false;
                            double oneDinarPrice = double.Parse(textBox8.Text) / 100;
                            double total= (double.Parse(textBox7.Text) * oneDinarPrice);
                            con.Open();
                            SqlCommand cmd = new SqlCommand("UPDATE Item_Code SET Price= '" + total.ToString() + "' where ID = '" + ID + "' ;", con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            refresh(dataGridView1);
                            con.Open();
                            SqlCommand cmdd = new SqlCommand("UPDATE Item_Code SET DPrice= '" + DollarPrice + "' where ID = '" + ID + "' ;", con);
                            cmdd.ExecuteNonQuery();
                            con.Close();
                            refresh(dataGridView1);
                        }
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

        private void button3_Click(object sender, EventArgs e)
        {
            x = 1;
            Form5 f = new Form5(F1, Namee, OP);
            f.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String ID = textBox1.Text.ToString();
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            SqlDataAdapter sda1 = new SqlDataAdapter("DELETE  FROM Item_Code where ID='" + ID + "'", con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            con.Close();
            refresh(dataGridView1);
        }

        private void Form9_FormClosed(object sender, FormClosedEventArgs e)
        {

            if (x == 0)
            {
                F1.Close();
            }
        }

      

        

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (!textBox6.Text.Equals(""))
            {
                textBox7.Enabled = false;
            }
            else
            {
                textBox7.Enabled = true;
                textBox7.Text = "";
                textBox6.Enabled = true;
            }
        }

      

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (!textBox7.Text.Equals(""))
            {
                textBox6.Enabled = false;  
            }
            else
            {
                textBox7.Enabled = true;
                textBox6.Text = "";
                textBox6.Enabled = true;
            }
        }
    }
}
