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
    public partial class Form7 : Form
    {
        int x = 0;
        Form F1;
        String Name;
        String OP;
        public static string sqlcon = "Data Source=OLIVER95\\SQLEXPRESS;Initial Catalog=C#TestDB;Integrated Security=True";
        public Form7(Form f, String name, String op)
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
            label5.Visible = false;
            F1 = f;
            Name = name;
           
            OP = op;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!(textBox1.Text.Equals("") && textBox2.Text.Equals("") && textBox3.Text.Equals("") && textBox4.Text.Equals("")))
            {
                bool test = true;
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString().Equals(textBox1.Text))
                    {
                        label5.Text = "Already exsits in The Table";
                        label5.Visible = true;
                        test = false;
                    }
                }
                if (test)
                {
                    if (!(textBox5.Text.Equals("")) || !(textBox6.Text.Equals("")))
                    {
                        if (!textBox5.Text.Equals(""))
                        {
                            double OneDinarPrice = 100 / double.Parse(textBox7.Text);
                            double ItemDollarPrice = double.Parse(textBox5.Text) * OneDinarPrice;
                            dataGridView1.Rows.Add(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, ItemDollarPrice);
                        }
                        else
                        {
                            double OneDollarPrice = double.Parse(textBox7.Text) / 100;
                            double ItemDinarPrice = double.Parse(textBox6.Text) * OneDollarPrice;
                            dataGridView1.Rows.Add(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, ItemDinarPrice, textBox6.Text);


                        }

                    }
                    textBox1.Select();
                    textBox1.SelectAll();
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";

                    label5.Visible = false;

                }

            }
            else
            {
                label5.Text = "Please Fill All Data";
                label5.Visible = true;
            }

        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() != null)
                {

                    String code = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    if (textBox1.Text.Equals(""))
                    {
                        label5.Text = "Enter The Barcode";
                        label5.Visible = true;
                    }
                    else
                    {
                        if (code.Equals(textBox1.Text.ToString()))
                        {


                            dataGridView1.Rows.RemoveAt(i);
                            textBox1.Select();
                            textBox1.SelectAll();
                            textBox2.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";
                            textBox5.Text = "";
                            textBox6.Text = "";

                            label5.Visible = false;
                        }
                        else
                        {
                            label5.Text = "No Row Found";
                            label5.Visible = true;

                        }
                    }
                }

            }


        }
        public static int CountRows(string tbname)
        {

            string stmt = "SELECT COUNT(*) FROM Item_Code where Item_Code='" + tbname + "';";
            int count = 0;

            using (SqlConnection thisConnection = new SqlConnection(sqlcon))
            {
                using (SqlCommand cmdCount = new SqlCommand(stmt, thisConnection))
                {
                    thisConnection.Open();
                    count = (int)cmdCount.ExecuteScalar();
                }
            }
            return count;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string code = "";
            string name = "";
            string company = "";
            string number = "";
            string price = "";
            string dPrice = "";

            int x = dataGridView1.Rows.Count - 1;
            for (int i = 0; i < x; i++)
            {


                code = dataGridView1.Rows[i].Cells[0].Value.ToString();
                name = dataGridView1.Rows[i].Cells[1].Value.ToString();
                company = dataGridView1.Rows[i].Cells[2].Value.ToString();
                number = dataGridView1.Rows[i].Cells[3].Value.ToString();
                price = dataGridView1.Rows[i].Cells[4].Value.ToString();
                dPrice = dataGridView1.Rows[i].Cells[5].Value.ToString();
                string datacode = "";
                string dataname = "";
                string com = "";
                int exn = 0;
                int rownum = CountRows(code);
                if (rownum > 0)
                    try
                    {
                        SqlConnection con = new SqlConnection(sqlcon);
                        SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Item_Code where Item_Code='" + code + "'", con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        datacode = dt.Rows[0][0].ToString();
                        dataname = dt.Rows[0][1].ToString();
                        com = dt.Rows[0][3].ToString();
                        exn=int.Parse(dt.Rows[0][2].ToString());
                    }
                    catch (Exception eee)
                    {
                        label5.Text = eee.Message;
                        label5.Visible = true;
                    }
                if (datacode.Equals(code))
                {

                    if (dataname.Equals(name))
                    {
                        MessageBox.Show("Item exist with Diffrent Name");
                    }
                    else
                    {
                        if (company.Equals(com))
                        {
                            MessageBox.Show("Item exist with Diffrent Company");
                        }
                        else
                        {
                            try
                            {
                                string num = dataGridView1.Rows[i].Cells[3].Value.ToString();
                                SqlConnection con = new SqlConnection(sqlcon);
                                SqlCommand cmdd = new SqlCommand("UPDATE Item_Code SET Number='" + (num+exn) + "' where Item_Code = '" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "' ;", con);
                                con.Open();
                                cmdd.ExecuteNonQuery();
                                con.Close();
                                dataGridView1.Rows.RemoveAt(i);
                                i = i - 1;
                                x = x - 1;
                            }
                            catch (Exception err)
                            {
                                MessageBox.Show(err.Message);
                            }
                        }
                    }
                }
                else
                {

                    try
                    {
                        SqlConnection conn = new SqlConnection(sqlcon);
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO Item_Code (Item_Code,Item_Name,Price,Company,DPrice,Number) VALUES ('" + code + "','" + name + "','" + price + "','" + company + "','" + dPrice + "','" + number + "');", conn);
                        cmd.ExecuteNonQuery();

                        conn.Close();
                        dataGridView1.Rows.RemoveAt(i);
                        i = i - 1;
                        x = x - 1;
                        label5.Text = "Data Add Successfully";
                        label5.Visible = true;
                        textBox1.Select();
                        textBox1.SelectAll();
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";

                    }
                    catch (Exception ee)
                    {

                        label5.Text = ee.Message;
                        label5.Visible = true;
                    }

                }
            }
        }

        private void Form7_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (x == 0)
            {
                F1.Close(); 
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            x = 1;
            Form5 f5 = new Form5(F1, Name, OP);
            f5.Show();
            this.Close();
        }

        private void Form7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode.ToString() == "S")
            {
                string searchValue = textBox3.Text;
              
               
                if (!searchValue.Equals("") )
                {
                    try
                    {
                        SqlConnection con = new SqlConnection(sqlcon);
                        SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Item_Code where Company= '" + searchValue + "';", con);
                        DataTable dtt = new DataTable();
                        sda.Fill(dtt);
                        bool x = true;
                        for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
                        {
                            if (dataGridView1.Rows[j].Cells[2].Value.ToString().Equals(textBox3.Text))
                            {
                                dataGridView1.Rows[j].Cells[2].Selected = true;
                                MessageBox.Show("Item Exist In Table ");
                                x = false;
                            }

                        }
                        if (x)
                        {
                            dataGridView1.Rows.Add(dtt.Rows[0][0], dtt.Rows[0][1], dtt.Rows[0][3], dtt.Rows[0][2], dtt.Rows[0][4], dtt.Rows[0][5]);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("There is No Item With This Bardcode");
                    }

                }
            }
            if (e.Control && e.KeyCode.ToString() == "Q")
            {

                if (!(textBox1.Text.Equals("") && textBox2.Text.Equals("") && textBox3.Text.Equals("") && textBox4.Text.Equals("")))
                {
                    bool test = true;
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value.ToString().Equals(textBox1.Text))
                        {
                            label5.Text = "Already exsits in The Table";
                            label5.Visible = true;
                            test = false;
                        }
                    }
                    if (test)
                    {
                        if (!((textBox5.Text.Equals("")) && (textBox6.Text.Equals(""))))
                        {
                            if (!textBox5.Text.Equals(""))
                            {
                                double OneDinarPrice = 100 / double.Parse(textBox7.Text);
                                double ItemDollarPrice = double.Parse(textBox5.Text) * OneDinarPrice;
                                dataGridView1.Rows.Add(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, ItemDollarPrice);
                            }
                            else
                            {
                                double OneDollarPrice = double.Parse(textBox7.Text) / 100;
                                double ItemDinarPrice = double.Parse(textBox6.Text) * OneDollarPrice;
                                dataGridView1.Rows.Add(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, ItemDinarPrice, textBox6.Text);


                            }
                        }
                        label5.Visible = false;

                    }

                }
                else
                {
                    label5.Text = "Please Fill All Data";
                    label5.Visible = true;
                }
                textBox1.Select();
                textBox1.SelectAll();
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                
            }
            if (e.Control && e.KeyCode.ToString() == "D")
            {

                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString() != null)
                    {

                        String code = dataGridView1.Rows[i].Cells[0].Value.ToString();
                        if (textBox1.Text.Equals(""))
                        {
                            label5.Text = "Enter The Barcode";
                            label5.Visible = true;
                        }
                        else
                        {
                            if (code.Equals(textBox1.Text.ToString()))
                            {


                                dataGridView1.Rows.RemoveAt(i);
                                label5.Visible = false;
                            }
                            else
                            {
                                label5.Text = "No Row Found";
                                label5.Visible = true;

                            }
                        }
                    }

                }
                textBox1.Select();
                textBox1.SelectAll();
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                
            }
            if (e.Control && e.KeyCode.ToString() == "E")
            {


                string code = "";
                string name = "";
                string company = "";
                string number = "";
                string price = "";
                string dPrice = "";


                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {


                    code = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    name = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    company = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    number = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    price = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    dPrice = dataGridView1.Rows[i].Cells[5].Value.ToString();
                    string datacode = "";
                    string dataname = "";
                    int rownum = CountRows(code);
                    if (rownum > 0)
                        try
                        {
                            SqlConnection con = new SqlConnection(sqlcon);
                            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Item_Code where Item_Code='" + code + "'", con);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            datacode = dt.Rows[0][0].ToString();
                            dataname = dt.Rows[0][1].ToString();
                        }
                        catch (Exception eee)
                        {
                            label5.Text = eee.Message;
                            label5.Visible = true;
                        }
                    if (datacode.Equals(code))
                    {
                        string str = "Data Exist In Database With The Same Name";
                        if (dataname.Equals(name))
                        {
                            str = "Data Exist In Database With A different Name";
                        }
                        MessageBox.Show(str);
                    }
                    else
                    {

                        try
                        {
                            SqlConnection conn = new SqlConnection(sqlcon);
                            conn.Open();
                            SqlCommand cmd = new SqlCommand("INSERT INTO Item_Code (Item_Code,Item_Name,Price,Company,DPrice,Number) VALUES ('" + code + "','" + name + "','" + price + "','" + company + "','" + dPrice + "','" + number + "');", conn);
                            cmd.ExecuteNonQuery();

                            conn.Close();
                            dataGridView1.Rows.RemoveAt(i);
                            label5.Text = "Data Add Successfully";
                            label5.Visible = true;
                        }
                        catch (Exception ee)
                        {

                            label5.Text = ee.Message;
                            label5.Visible = true;
                        }

                    }
                    textBox1.Select();
                    textBox1.SelectAll();
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                
                }
                

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string searchValue = textBox3.Text;
            int num = 0;
          
            if (!searchValue.Equals("") )
            {
                try
                {
                    SqlConnection con = new SqlConnection(sqlcon);
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Item_Code where Company= '" + searchValue + "';", con);
                    DataTable dtt = new DataTable();
                    sda.Fill(dtt);
                    bool x = true;
                    for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
                    {
                        if (dataGridView1.Rows[j].Cells[2].Value.ToString().Equals(textBox3.Text))
                        {
                            dataGridView1.Rows[j].Cells[2].Selected = true;
                            MessageBox.Show("Item Exist In Table ");
                            x = false;
                        }

                    }
                    if (x)
                    {
                        dataGridView1.Rows.Add(dtt.Rows[0][0], dtt.Rows[0][1], dtt.Rows[0][3], dtt.Rows[0][2], dtt.Rows[0][4], dtt.Rows[0][5]);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("There is No Item With This Bardcode");
                }

            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int i =  dataGridView1.Rows.Count - 2;
            if (!dataGridView1.Rows[i].Cells[4].Value.ToString().Equals(""))
            {
                dataGridView1.Rows[i].Cells[6].Value = (double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()) * double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())).ToString();
                double oneDollar = 100 / double.Parse(textBox7.Text);
                double total = oneDollar * double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                dataGridView1.Rows[i].Cells[7].Value = (double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()) * total).ToString();


            }
            else
            {
                dataGridView1.Rows[i].Cells[7].Value = (double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()) * double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString())).ToString();
                double oneDollar =  double.Parse(textBox7.Text)/100;
                double total = oneDollar * double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                dataGridView1.Rows[i].Cells[6].Value = (double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()) * total).ToString();

            }
            
                //dataGridView1.Rows[i].Cells[6].Value = (double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()) * double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())).ToString();
                //dataGridView1.Rows[i].Cells[7].Value = (double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()) * double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString())).ToString();

            
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
