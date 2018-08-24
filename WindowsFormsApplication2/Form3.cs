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
    public partial class Form3 : Form
    {
        Double dolor;
        string userName;
        public static string sqlcon= "Data Source=OLIVER95\\SQLEXPRESS;Initial Catalog=C#TestDB;Integrated Security=True";
        public Form3(double Dolor, string UserName)
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
            userName = UserName;
            dolor = Dolor;
            MessageBox.Show(UserName);
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("MMM_d_yyyy");



        }

        private void button1_Click(object sender, EventArgs e)
        {
            String Table_name = dateTimePicker1.Value.ToString("MMM_d_yyyy");

            String searchValue = textBox1.Text.ToString();
            try
            {
                SqlConnection con = new SqlConnection(sqlcon);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM " + Table_name + " where Item_Code='" + searchValue + "' and UserName='" + this.userName + "';", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                String ItemCode;
                String ItemName;
                String Company;
                String Number;
                String DinarPrice;
                String DolorPrice;


                ItemCode = dt.Rows[0][0].ToString();
                ItemName = dt.Rows[0][1].ToString();
                Number = dt.Rows[0][2].ToString();
                Company = dt.Rows[0][3].ToString();
                DinarPrice = dt.Rows[0][4].ToString();
                DolorPrice = dt.Rows[0][5].ToString();

                if (dataGridView1.Rows.Count > 1)
                {
                    try
                    {
                        SqlDataAdapter table = new SqlDataAdapter("SELECT Number FROM Item_Code where Item_Code='" + searchValue + "';", con);
                        DataTable ItemNumber = new DataTable();
                        table.Fill(ItemNumber);

                        Boolean testresult = true;


                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {

                            string dname = "";
                            try
                            {
                                dname = dataGridView1.Rows[i].Cells[0].Value.ToString();
                            }
                            catch (Exception )
                            {
                                dname = "";
                            }
                            if (dname.Equals(searchValue))
                            {
                                int numup = int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()) + 1;
                                int num = int.Parse(dt.Rows[0][2].ToString()) - numup;
                                if (num < 0)
                                {
                                    MessageBox.Show("you shall not return this becouse we have " + num + " of " + searchValue);
                                }
                                else
                                {
                                    dataGridView1.Rows[i].Cells[3].Value = numup.ToString();
                                    double TotalDinar = double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) * double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                                    double TotalDolor = double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()) * double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                                    dataGridView1.Rows[i].Cells[6].Value = TotalDinar.ToString();
                                    dataGridView1.Rows[i].Cells[7].Value = TotalDolor.ToString();


                                }
                                testresult = false;

                            }
                        }
                        if (testresult)
                        {

                            int num = int.Parse(dt.Rows[0][2].ToString()) - 1;
                            if (num < 0)
                            {
                                MessageBox.Show("you shall not return this becouse we have " + num + " of " + searchValue);
                            }
                            else
                            {
                                string totaldinar = DinarPrice;
                                string totldolar = DolorPrice;

                                dataGridView1.Rows.Add(ItemCode, ItemName, Company, "1", DinarPrice, DolorPrice, totaldinar, totldolar);


                            }
                        }






                    }
                    catch (Exception FirstTry)
                    {
                        MessageBox.Show("First Try " + FirstTry.Message);
                    }
                }
                else
                {
                    int num = int.Parse(dt.Rows[0][2].ToString());
                    if (num < 0)
                    {
                        MessageBox.Show("you shall not return this becouse we have " + num + " of " + searchValue);
                    }
                    else
                    {
                        dataGridView1.Rows.Add(ItemCode, ItemName, Company, "1", DinarPrice, DolorPrice);
                    }
                }

            }
            catch (Exception ItemNotFound)
            {
                MessageBox.Show("Item could Not be Found in " + Table_name + " Table Couse of " + ItemNotFound.Message);
            }

        }



        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            /////////////////////////////////////////////////////////
            /////////////////cheking month//////////////////////
            String str = dateTimePicker1.Value.ToString("MM");
            String str1 = DateTime.Now.ToString("MM");
            int x = int.Parse(str1);
            int y = int.Parse(str);

            if (x != y)
            {
                dateTimePicker1.Value = DateTime.Now;
                MessageBox.Show("rajan dast la mangy mada babi babm");
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////checking years/////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            String str0 = dateTimePicker1.Value.ToString("yy");
            String str11 = DateTime.Now.ToString("yy");
            int x0 = int.Parse(str11);
            int y0 = int.Parse(str0);

            if (x0 != y0)
            {
                dateTimePicker1.Value = DateTime.Now;
                MessageBox.Show("rajan dast la sal mada babi babm");
            }


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////// checking days////////////////////////////////////////////////////////////////////////////////////






            int xx = int.Parse(dateTimePicker1.Value.ToString("dd"));
            int x1 = int.Parse(DateTime.Now.ToString("dd"));
            int y1 = x1 - 7;
            bool test = false;
            for (int i = y1; i <= x1; i++)
            {
                if (xx == i)
                {
                    test = true;
                }
            }
            if (test == false)
            {
                dateTimePicker1.Value = DateTime.Now;
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(sqlcon);
                con.Open();
                string s1 = dateTimePicker1.Value.ToString("MMM_d_yyyy");
                
                string code;
               

                // SqlConnection con = new SqlConnection("Data Source=OLIVER95;Initial Catalog=C#TestDB;Integrated Security=True");
                // SqlDataAdapter sda = new SqlDataAdapter("SELECT Number FROM " + s1 + " where Item_Code='" + code + "'and UserName='" + this.Name + "';", con);
                // DataTable dt = new DataTable();
                //
                // con.Open();
                // SqlCommand cmd = new SqlCommand("UPDATE " + s1 + " SET Number='" + nk1 + "' where Item_Code = '" + "'and UserName='" + this.Name + "';", con);
                // cmd.ExecuteNonQuery();
                // con.Close();
                if (dataGridView1.Rows.Count > 1)
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        code = dataGridView1.Rows[i].Cells[0].Value.ToString();

                        try
                        {


                            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM " + s1 + " where Item_Code='" + code + "'and UserName='" + this.userName + "';", con);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            SqlDataAdapter sdaa = new SqlDataAdapter("SELECT Number FROM Item_Code where Item_Code='" + code + "';", con);
                            DataTable dtt = new DataTable();
                            sdaa.Fill(dtt);
                            int numbertest = int.Parse(dtt.Rows[0][0].ToString()) - int.Parse(dt.Rows[0][2].ToString());
                            int nummm = int.Parse(dtt.Rows[0][0].ToString()) + int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                            if (numbertest < 1)
                            {
                                SqlCommand cmd = new SqlCommand("UPDATE Item_Code SET Number='" + (nummm).ToString() + "' where Item_Code = '" + code + "';", con);
                                cmd.ExecuteNonQuery();
                                SqlCommand cmdd = new SqlCommand("DELETE FROM " + s1 + " WHERE Item_Code = '" + code + "' and UserName='" + this.userName + "';", con);
                                cmdd.ExecuteNonQuery();
                            }
                            else
                            {
                                int numup = int.Parse(dtt.Rows[0][0].ToString()) + int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());

                                int itemup = int.Parse(dt.Rows[0][2].ToString()) - int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                                SqlCommand cmd = new SqlCommand("UPDATE Item_Code SET Number='" + numup.ToString() + "' where Item_Code = '" + code + "';", con);
                                cmd.ExecuteNonQuery();
                                SqlCommand cmdd = new SqlCommand("UPDATE " + s1 + " SET Number='" + itemup.ToString() + "' where Item_Code = '" + code + "'and UserName='" + this.userName + "';", con);
                                cmdd.ExecuteNonQuery();
                            }


                        }
                        catch (Exception donnu)
                        {
                            MessageBox.Show(donnu.Message);
                        }
                        finally
                        {
                            dataGridView1.Rows.Clear();
                            textBox1.Text = "";
                            dateTimePicker1.Value = DateTime.Now;
                            
                        }



                    }
                con.Close();
            }
            catch (Exception conex)
            { MessageBox.Show(conex.Message); }
        }
    }
}


