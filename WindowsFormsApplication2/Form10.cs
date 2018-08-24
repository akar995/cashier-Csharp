using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace WindowsFormsApplication2
{
    public partial class Form10 : Form
    {
        Form F4;
        int x = 0;
        String Namee;
        String OP;
        public static string sqlcon= "Data Source=OLIVER95\\SQLEXPRESS;Initial Catalog=C#TestDB;Integrated Security=True";
        public Form10(Form f, String name, String op)
        {
            Namee = name;
            OP = op;
            F4 = f;
            InitializeComponent();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            x =  1;
            Form5 f5 = new Form5(F4, Namee, OP);
            f5.Show();
            this.Close();

        }

        private void Form10_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (x == 0)
            {
                
                
                F4.Close();

            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // String Table_name = dateTimePicker1.Value.ToString("MMM_d_yyyy");
            DateTime start = new DateTime();
            start = dateTimePicker1.Value;
            DateTime end = new DateTime();
            end = dateTimePicker2.Value;
            int d = (int)(end - start).TotalDays;
            String Table_name = dateTimePicker1.Value.ToString("MMM_d_yyyy");
            bool test = true;
            dataGridView1.Rows.Clear();
          /* try
            {
                SqlConnection con = new SqlConnection(sqlcon);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM " + Table_name + ";", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
            }
            catch (Exception)
            {
                MessageBox.Show("There is No Table In This date "+Table_name);
                test = false;
            }*/
            if (test)
            {
                for (int i = 0; i <= d; i++)
                {
                    String Table_Name = dateTimePicker1.Value.AddDays(i).ToString("MMM_d_yyyy");
                    try
                    {
                        SqlConnection con = new SqlConnection(sqlcon);
                        SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM " + Table_Name + ";", con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        for (int j = 0; j < dt.Rows.Count ; j++)
                        {
                            double TotalDollar = double.Parse(dt.Rows[j][2].ToString()) * double.Parse(dt.Rows[j][5].ToString());
                            double TotalDinar = double.Parse(dt.Rows[j][2].ToString()) * double.Parse(dt.Rows[j][4].ToString());
                            dataGridView1.Rows.Add(dt.Rows[j][6].ToString(), dt.Rows[j][0].ToString(), dt.Rows[j][1].ToString(), dt.Rows[j][3].ToString(), dt.Rows[j][2].ToString(), dt.Rows[j][4].ToString(), dt.Rows[j][5].ToString(),TotalDinar,TotalDollar);
                        }
                    }
                    catch (Exception dunno)
                    {
                        

                    }

                }
            }
            double TDollar = 0;
            double TDinar = 0;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                TDinar=TDinar+double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                TDollar=TDollar + double.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString());

            }
            label7.Text = TDinar.ToString() + "IQ";
            label6.Text = TDollar.ToString() + "$";


        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value > DateTime.Now)
            {
                dateTimePicker2.Value = DateTime.Now;
                MessageBox.Show("you can not set to greater then today");

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string barcode = textBox2.Text;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (!dataGridView1.Rows[i].Cells[1].Value.ToString().Equals(barcode))
                {
                    dataGridView1.Rows.RemoveAt(i);
                    i = i - 1;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string uname = textBox1.Text;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (!dataGridView1.Rows[i].Cells[0].Value.ToString().Equals(uname))
                {
                    dataGridView1.Rows.RemoveAt(i);
                    i = i - 1;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string ItemName = textBox3.Text;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (!dataGridView1.Rows[i].Cells[2].Value.ToString().Equals(ItemName))
                {
                    dataGridView1.Rows.RemoveAt(i);
                    i = i - 1;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string company = textBox4.Text;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (!dataGridView1.Rows[i].Cells[3].Value.ToString().Equals(company))
                {
                    dataGridView1.Rows.RemoveAt(i);
                    i = i - 1;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!textBox9.Text.Equals("") && textBox10.Text.Equals(""))
            {
                int x = int.Parse(textBox9.Text);
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (x > int.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()))
                    {
                        dataGridView1.Rows.RemoveAt(i);
                        i = i - 1;
                    }
                }

            }
            if (textBox9.Text.Equals("") && !textBox10.Text.Equals(""))
            {
                int x = int.Parse(textBox10.Text);
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (int.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) > x)
                    {
                        dataGridView1.Rows.RemoveAt(i);
                        i = i - 1;

                    }
                }

            }
            if (!textBox9.Text.Equals("") && !textBox10.Text.Equals(""))
            {
                int x = int.Parse(textBox10.Text);
                int y = int.Parse(textBox9.Text);
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (y < int.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) && x > int.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()))
                    {
                        dataGridView1.Rows.RemoveAt(i);
                        i = i - 1;


                    }
                }

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!textBox5.Text.Equals("") && textBox6.Text.Equals(""))
            {
                double x = double.Parse(textBox5.Text);
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (x > double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()))
                    {
                        dataGridView1.Rows.RemoveAt(i);
                        i = i - 1;
                    }
                }

            }
            if (textBox5.Text.Equals("") && !textBox6.Text.Equals(""))
            {
                double x = double.Parse(textBox6.Text);
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()) > x)
                    {
                        dataGridView1.Rows.RemoveAt(i);
                        i = i - 1;

                    }
                }

            }
            if (!textBox5.Text.Equals("") && !textBox6.Text.Equals(""))
            {
                double y = double.Parse(textBox5.Text);
                double x = double.Parse(textBox6.Text);
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (y < double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()) && x > double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()))
                    {
                        dataGridView1.Rows.RemoveAt(i);
                        i = i - 1;


                    }
                }

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!textBox7.Text.Equals("") && textBox8.Text.Equals(""))
            {
                double x = double.Parse(textBox7.Text);
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (x < double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()))
                    {
                        dataGridView1.Rows.RemoveAt(i);
                        i = i - 1;
                    }
                }

            }
            if (textBox7.Text.Equals("") && !textBox8.Text.Equals(""))
            {
                double x = double.Parse(textBox8.Text);
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()) < x)
                    {
                        dataGridView1.Rows.RemoveAt(i);
                        i = i - 1;

                    }
                }

            }
            if (!textBox7.Text.Equals("") && !textBox8.Text.Equals(""))
            {
                double x = double.Parse(textBox8.Text);
                double y = double.Parse(textBox7.Text);
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (y < double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()) && x > double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()))
                    {
                        dataGridView1.Rows.RemoveAt(i);
                        i = i - 1;


                    }
                }

            }
        }

        private void Form10_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
