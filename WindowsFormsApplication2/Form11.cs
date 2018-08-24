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
    public partial class Form11 : Form
    {

        Form f1;
        int x = 0;
        String Name;
        String OP;
        
        public static string sqlconn= "Data Source=OLIVER95\\SQLEXPRESS;Initial Catalog=C#TestDB;Integrated Security=True";
        public Form11(Form f,String name,String op)
        {
         
            InitializeComponent();
            this.Name = name;
         
            OP=op;
            f1 = f;
           
           
        }


        private void button2_Click(object sender, EventArgs e)
        {
           
            label3.Text = "0";
            dataGridView1.Rows.Clear();
           
            label3.Text = "0";
        }


        public void button1_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(sqlconn);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Item_Code where Item_Code='" + textBox6.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            String searchValue = textBox6.Text;
            Boolean dataSearchResult = true;
            if (textBox6.Text.Equals(""))
            {
                MessageBox.Show("Enter The Barcode");
            }
            else
            {

                if (dataGridView1.Rows.Count > 1)

                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {

                        if (dataGridView1.Rows[i].Cells[0].Value.ToString().Equals(searchValue))
                        {
                            int RemainItem = 0;
                            if (!dataGridView1.Rows[i].Cells[4].Value.ToString().Equals(""))
                            {
                                dataSearchResult = false;
                                dataGridView1.Rows[i].Cells[3].Value = (int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()) + 1).ToString();
                                double totalPrice = double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()) * double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                                ///////////////need a select qury

                                try
                                {
                                    SqlDataAdapter sdaa = new SqlDataAdapter("SELECT * FROM Item_Code where Item_Code= '" + searchValue + "';", con);
                                    DataTable dtt = new DataTable();
                                    sdaa.Fill(dtt);
                                    // MessageBox.Show(dtt.Rows[0][0] + "  " + dtt.Rows[0][1] + "  " + dtt.Rows[0][2] + " " + dtt.Rows[0][3] + "  " + dtt.Rows[0][4] + "  " + dtt.Rows[0][5] + "  " + dtt.Rows[0][6]);
                                    for (int h = 0; h < dataGridView1.Rows.Count - 1; h++)
                                    {
                                        if (dataGridView1.Rows[h].Cells[0].Value.ToString().Equals(dtt.Rows[0][0]))
                                        {
                                            RemainItem = int.Parse(dataGridView1.Rows[h].Cells[3].Value.ToString());
                                            int x = int.Parse(dtt.Rows[0][2].ToString()) - RemainItem;
                                            if (x < 0)
                                                MessageBox.Show("you have " + x + " Of " + dtt.Rows[0][1].ToString() + "  Item Barcode: " + dtt.Rows[0][0].ToString());
                                        }
                                    }
                                }
                                catch (Exception ItemNotFound)
                                {
                                    MessageBox.Show("First Try :Item Not Found Couse " + ItemNotFound.Message);

                                }
                                

                                dataGridView1.Rows[i].Cells[6].Value = totalPrice.ToString();
                                double OneDolorPrice = 100 / double.Parse(textBox1.Text);
                                dataGridView1.Rows[i].Cells[5].Value = double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) * OneDolorPrice;
                                dataGridView1.Rows[i].Cells[7].Value = double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()) * double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());


                                break;
                            }
                            else
                            {
                                dataSearchResult = false;
                                dataGridView1.Rows[i].Cells[3].Value = (int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()) + 1).ToString();
                                
                                double totalPrice = double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()) * double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                                dataGridView1.Rows[i].Cells[7].Value = totalPrice.ToString();
                                try
                                {
                                    SqlDataAdapter sdaa = new SqlDataAdapter("SELECT * FROM Item_Code where Item_Code= '" + searchValue + "';", con);
                                    DataTable dtt = new DataTable();
                                    sdaa.Fill(dtt);
                                    RemainItem = int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                                    int x = int.Parse(dtt.Rows[0][2].ToString()) - RemainItem;
                                    if (x < 0)
                                        MessageBox.Show("you have " + x + " Of " + dt.Rows[i][1].ToString() + "  Item Barcode: " + dt.Rows[i][0].ToString());
                                }
                                catch (Exception ItemNotFound)
                                {
                                    MessageBox.Show("Second TryItem Not Found Couse " + ItemNotFound.Message);

                                }
                                
                                double OneDolorPrice = double.Parse(textBox1.Text) / 100;
                                dataGridView1.Rows[i].Cells[4].Value = double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()) * OneDolorPrice;
                                dataGridView1.Rows[i].Cells[6].Value = double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()) * double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                                break;

                            }


                        }

                    }


                if (dataSearchResult)
                {
                    try
                    {
                        if (dt.Rows[0][0].ToString().Equals(searchValue))
                        {
                            String priceTest = dt.Rows[0][2].ToString();
                            if (priceTest != "")
                            {
                                try
                                {
                                    SqlDataAdapter sdaa = new SqlDataAdapter("SELECT * FROM Item_Code where Item_Code= '" + searchValue + "';", con);
                                    DataTable dtt = new DataTable();
                                    sdaa.Fill(dtt);
                                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                                    {
                                        if(dataGridView1.Rows[i].Cells[3].Value.ToString().Equals(searchValue))
                                        {
                                        int RemainItem = int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                                        int x = int.Parse(dtt.Rows[0][2].ToString()) - RemainItem;
                                        if (x < 0)
                                            MessageBox.Show("you have " + x + " Of " + dt.Rows[0][1].ToString() + "  Item Barcode: " + dt.Rows[0][0].ToString());
                                    }
                                    }
                                }
                                catch (Exception ItemNotFound)
                                {
                                    MessageBox.Show("Thrid Try Item Not Found Couse " + ItemNotFound.Message);

                                }
                                

                                double OneDolorPrice = 100 / double.Parse(textBox1.Text.ToString());
                                double ItemPrice = double.Parse(dt.Rows[0][4].ToString()) * OneDolorPrice;
                                // dataGridView1.Rows.Add(dt.Rows[0][0], dt.Rows[0][1], "1", dt.Rows[0][2], dt.Rows[0][5]);
                                dataGridView1.Rows.Add(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][3].ToString(), "1", dt.Rows[0][4].ToString(), ItemPrice, dt.Rows[0][4].ToString(), ItemPrice);
                            }
                            else
                            {
                                try
                                {
                                    SqlDataAdapter sdaa = new SqlDataAdapter("SELECT * FROM Item_Code where Item_Code= '" + searchValue + "';", con);
                                    DataTable dtt = new DataTable();
                                    sdaa.Fill(dtt);
                                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                                    {
                                        if(dataGridView1.Rows[i].Cells[3].Value.ToString().Equals(searchValue))
                                        {
                                  int  RemainItem = int.Parse(dataGridView1.Rows[0].Cells[3].Value.ToString());
                                    int x = int.Parse(dtt.Rows[0][2].ToString()) - RemainItem;
                                    if (x < 0)
                                        MessageBox.Show("you have " + x + " Of " + dt.Rows[0][1].ToString() + "  Item Barcode: " + dt.Rows[0][0].ToString());
                                        }
                                    }
                                }
                                catch (Exception ItemNotFound)
                                {
                                    MessageBox.Show("forth tryItem Not Found Couse " + ItemNotFound.Message);

                                }
                                
                                double OneDolorPrice = double.Parse(textBox1.Text.ToString()) / 100;
                                double ItemPrice = double.Parse(dt.Rows[0][5].ToString()) * OneDolorPrice;
                                // dataGridView1.Rows.Add(dt.Rows[0][0], dt.Rows[0][1], "1", dt.Rows[0][2], dt.Rows[0][5]);
                                dataGridView1.Rows.Add(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][3].ToString(), "1", ItemPrice, dt.Rows[0][5].ToString(), ItemPrice, dt.Rows[0][5].ToString());

                            }
                        }
                    }
                    catch (Exception )
                    {
                        MessageBox.Show("Wrong Barcode");
                    }
                }
            }
        }

        
       
        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.c_BarcodeTableAdapter.FillBy(this._C_TestDBDataSet._C_Barcode);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

     

        /// <summary>
        /// return number of rows in database in item table
        public static int CountRows()
        {
            String s1 = DateTime.Now.ToString("MMM_d_yyyy");
            string stmt = "SELECT COUNT(*) FROM "+s1;
            int count = 0;
            
            using (SqlConnection thisConnection = new SqlConnection(sqlconn))
            {
                using (SqlCommand cmdCount = new SqlCommand(stmt, thisConnection))
                {
                    thisConnection.Open();
                    count = (int)cmdCount.ExecuteScalar();
                }
            }
            return count;
        }
        public static int CountItemRows()
        {
            ;
            string stmt = "SELECT COUNT(*) FROM Item_Code";
            int count = 0;

            using (SqlConnection thisConnection = new SqlConnection(sqlconn))
            {
                using (SqlCommand cmdCount = new SqlCommand(stmt, thisConnection))
                {
                    thisConnection.Open();
                    count = (int)cmdCount.ExecuteScalar();
                }
            }
            return count;
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sqlconn);
            con.Open();
            String s1 = DateTime.Now.ToString("MMM_d_yyyy");
            double text4 = 0;
            double text5 = 0;
            try
            {
                text4 = double.Parse(textBox4.Text);
            }
            catch (Exception)
            { }
            try
            {

                text5 = double.Parse(textBox5.Text);
            }
            catch (Exception)
            { }

            if (text4 > 0 || text5 > 0)
            {

                try
                {
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM" + s1 + ";", con);

                    DataTable dt = new DataTable();

                    sda.Fill(dt);



                }
                catch (Exception)
                {
                }
                try
                {


                    SqlCommand cmd = new SqlCommand("CREATE TABLE " + s1 + "(Item_Code NVARCHAR(50), Item_Name NVARCHAR(50), Number NVARCHAR(50),Company NVARCHAR(50), DinarPrice NVARCHAR(50), DolorPrice NVARCHAR(50),UserName NVARCHAR(50));", con);
                    cmd.ExecuteNonQuery();


                }
                catch (Exception) { }
                try
                {
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM " + s1 + ";", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    SqlDataAdapter sdaa = new SqlDataAdapter("SELECT * FROM Item_Code;", con);
                    DataTable dtt = new DataTable();
                    sdaa.Fill(dtt);

                    int rowws = CountRows();
                    Boolean dataSearchTest = true;
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        string dataRowInfo = dataGridView1.Rows[i].Cells[0].Value.ToString();
                        string dataDBInfo = "";
                        string DBUserName = "";
                        if (CountRows() == 0)
                        {
                            rowws = rowws + 1;
                        }
                        dataSearchTest = true;
                        for (int j = 0; j < rowws; j++)
                        {

                            ///////////////////////to get user name
                            try
                            {
                                DBUserName = dt.Rows[j][6].ToString();

                            }
                            catch (Exception)
                            {
                                DBUserName = " ";
                            }

                            ////to a code
                            try
                            {
                                dataDBInfo = dt.Rows[j][0].ToString();

                            }
                            catch (Exception)
                            {
                                dataDBInfo = "akar";
                            }
                            if (dataRowInfo.Equals(dataDBInfo) && this.Name.Equals(DBUserName))
                            {
                                try
                                {
                                    int num = int.Parse(dt.Rows[j][2].ToString()) + int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());

                                    SqlCommand cmd = new SqlCommand("UPDATE " + s1 + " SET Number='" + num + "' where Item_Code = '" + dataDBInfo + "' ;", con);
                                    int upNum = 0;
                                    SqlDataAdapter GetTableNumber = new SqlDataAdapter("SELECT Number FROM Item_Code where Item_Code= '" + dataDBInfo + "';", con);
                                    DataTable TableNumber = new DataTable();
                                    GetTableNumber.Fill(TableNumber);
                                    upNum = int.Parse(TableNumber.Rows[0][0].ToString()) - int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                                    SqlCommand cmdd = new SqlCommand("UPDATE Item_Code SET Number='" + upNum + "' where Item_Code = '" + dataDBInfo + "' ;", con);
                                    dataSearchTest = false;
                                    cmd.ExecuteNonQuery();
                                    cmdd.ExecuteNonQuery();
                                }
                                catch (Exception update)
                                {
                                    MessageBox.Show("First try " + update.Message);
                                }
                            }

                        }

                        if (dataSearchTest)
                        {
                            string code = dataGridView1.Rows[i].Cells[0].Value.ToString();
                            string name = dataGridView1.Rows[i].Cells[1].Value.ToString();
                            string Company = dataGridView1.Rows[i].Cells[2].Value.ToString();
                            string number = dataGridView1.Rows[i].Cells[3].Value.ToString();
                            string DinarPrice = dataGridView1.Rows[i].Cells[4].Value.ToString();
                            string DolorPrice = dataGridView1.Rows[i].Cells[5].Value.ToString();
                            int upNum = 0;
                            for (int h = 0; h < CountItemRows() - 1; h++)
                            {
                                if (dtt.Rows[h][0].Equals(dataDBInfo))
                                {
                                    MessageBox.Show("test1"+h);
                                    upNum = int.Parse(dtt.Rows[h][2].ToString()) - int.Parse(dataGridView1.Rows[i].Cells[3].ToString());
                                    break;
                                }
                            }

                            int upNum1 = 0;
                            SqlDataAdapter GetTableNumber = new SqlDataAdapter("SELECT Number FROM Item_Code where Item_Code= '" + code + "';", con);
                            DataTable TableNumber = new DataTable();
                            GetTableNumber.Fill(TableNumber);
                            upNum1 = int.Parse(TableNumber.Rows[0][0].ToString()) - int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                            SqlCommand cmdd = new SqlCommand("UPDATE Item_Code SET Number='" + upNum1 + "' where Item_Code = '" + code + "' ;", con);


                            SqlCommand cmd = new SqlCommand("INSERT INTO " + s1 + " (Item_Code,Item_Name,Number,Company,DinarPrice,DolorPrice,UserName) VALUES ('" + code + "','" + name + "','" + number + "','" + Company + "','" + DinarPrice + "','" + DolorPrice + "','" + this.Name + "');", con);
                            cmd.ExecuteNonQuery();
                            cmdd.ExecuteNonQuery();
                        }
                    }
                    con.Close();


                }
                catch (Exception ee)
                {
                    MessageBox.Show("Second try " + ee.Message);
                }
                dataGridView1.Rows.Clear();
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
            }

            else
            {
                MessageBox.Show("Enter The Given Money");
            }

        }

            

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            double totalDinarPrice = 0;
            double totalDolorPrice = 0;
            for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
            {
                totalDinarPrice = double.Parse(dataGridView1.Rows[j].Cells[6].Value.ToString()) + totalDinarPrice;
                totalDolorPrice = double.Parse(dataGridView1.Rows[j].Cells[7].Value.ToString()) + totalDolorPrice;
            }
            label3.Text = totalDinarPrice.ToString();
            label16.Text = totalDolorPrice.ToString();
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form11 f = new Form11(f1,Name,OP);
            
        }
       
        private void button4_Click(object sender, EventArgs e)
        {

            Form2 f = new Form2();

            
            f.Owner = this;
            f.Show();
            
            
           

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode.ToString() == "R")
            {
                
                label3.Text = "0";
                dataGridView1.Rows.Clear();
               
                label3.Text = "0";
                label3.Text = "0";
            }
            if (e.Control && e.KeyCode.ToString() == "D")
            {
                Form2 f = new Form2();
                f.Owner = this;
                f.Show();
                
            }
            if (e.Control && e.KeyCode.ToString() == "T")
            {

                Form3 f = new Form3(double.Parse(textBox1.Text),this.Name);


                f.Owner = this;
                f.Show();

            }
            if (e.Control && e.KeyCode.ToString() == "B")
            {
            SqlConnection con = new SqlConnection(sqlconn);
            con.Open();
            String s1 = DateTime.Now.ToString("MMM_d_yyyy");
            double text4 = 0;
            double text5 = 0;
            try
            {
                text4 = double.Parse(textBox4.Text);
            }
            catch (Exception)
            { }
            try
            {

                text5 = double.Parse(textBox5.Text);
            }
            catch (Exception)
            { }

            if (text4 > 0 || text5 > 0)
            {

                try
                {
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM" + s1 + ";", con);

                    DataTable dt = new DataTable();

                    sda.Fill(dt);



                }
                catch (Exception)
                {
                }
                try
                {


                    SqlCommand cmd = new SqlCommand("CREATE TABLE " + s1 + "(Item_Code NVARCHAR(50), Item_Name NVARCHAR(50), Number NVARCHAR(50),Company NVARCHAR(50), DinarPrice NVARCHAR(50), DolorPrice NVARCHAR(50),UserName NVARCHAR(50));", con);
                    cmd.ExecuteNonQuery();


                }
                catch (Exception) { }
                try
                {
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM " + s1 + ";", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    SqlDataAdapter sdaa = new SqlDataAdapter("SELECT * FROM Item_Code;", con);
                    DataTable dtt = new DataTable();
                    sdaa.Fill(dtt);

                    int rowws = CountRows();
                    Boolean dataSearchTest = true;
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        string dataRowInfo = dataGridView1.Rows[i].Cells[0].Value.ToString();
                        string dataDBInfo = "";
                        string DBUserName = "";
                        if (CountRows() == 0)
                        {
                            rowws = rowws + 1;
                        }
                        dataSearchTest = true;
                        for (int j = 0; j < rowws; j++)
                        {

                            ///////////////////////to get user name
                            try
                            {
                                DBUserName = dt.Rows[j][6].ToString();

                            }
                            catch (Exception)
                            {
                                DBUserName = " ";
                            }

                            ////to a code
                            try
                            {
                                dataDBInfo = dt.Rows[j][0].ToString();

                            }
                            catch (Exception)
                            {
                                dataDBInfo = "akar";
                            }
                            if (dataRowInfo.Equals(dataDBInfo) && this.Name.Equals(DBUserName))
                            {
                                try
                                {

                                    int num = int.Parse(dt.Rows[j][2].ToString()) + int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());

                                    SqlCommand cmd = new SqlCommand("UPDATE " + s1 + " SET Number='" + num + "' where Item_Code = '" + dataDBInfo + "' ;", con);
                                    int upNum = 0;
                                    SqlDataAdapter GetTableNumber = new SqlDataAdapter("SELECT Number FROM Item_Code where Item_Code= '" + dataDBInfo + "';", con);
                                    DataTable TableNumber = new DataTable();
                                    GetTableNumber.Fill(TableNumber);
                                    upNum = int.Parse(TableNumber.Rows[0][0].ToString()) - int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());

                                    SqlCommand cmdd = new SqlCommand("UPDATE Item_Code SET Number='" + upNum + "' where Item_Code = '" + dataDBInfo + "' ;", con);
                                    dataSearchTest = false;
                                    cmd.ExecuteNonQuery();
                                    cmdd.ExecuteNonQuery();
                                }
                                catch (Exception update)
                                {
                                    MessageBox.Show("First try " + update.Message);
                                }
                            }

                        }

                        if (dataSearchTest)
                        {
                            string code = dataGridView1.Rows[i].Cells[0].Value.ToString();
                            string name = dataGridView1.Rows[i].Cells[1].Value.ToString();
                            string Company = dataGridView1.Rows[i].Cells[2].Value.ToString();
                            string number = dataGridView1.Rows[i].Cells[3].Value.ToString();
                            string DinarPrice = dataGridView1.Rows[i].Cells[4].Value.ToString();
                            string DolorPrice = dataGridView1.Rows[i].Cells[5].Value.ToString();
                            int upNum = 0;
                            for (int h = 0; h < CountItemRows() - 1; h++)
                            {
                                if (dtt.Rows[h][0].Equals(dataDBInfo))
                                {
                                    upNum = int.Parse(dtt.Rows[h][6].ToString()) - int.Parse(dataGridView1.Rows[i].Cells[3].ToString());
                                    break;
                                }
                            }

                            int upNum1 = 0;
                            SqlDataAdapter GetTableNumber = new SqlDataAdapter("SELECT Number FROM Item_Code where Item_Code= '" + code + "';", con);
                            DataTable TableNumber = new DataTable();
                            GetTableNumber.Fill(TableNumber);
                            upNum1 = int.Parse(TableNumber.Rows[0][0].ToString()) - int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                            SqlCommand cmdd = new SqlCommand("UPDATE Item_Code SET Number='" + upNum1 + "' where Item_Code = '" + code + "' ;", con);


                            SqlCommand cmd = new SqlCommand("INSERT INTO " + s1 + " (Item_Code,Item_Name,Number,Company,DinarPrice,DolorPrice,UserName) VALUES ('" + code + "','" + name + "','" + number + "','" + Company + "','" + DinarPrice + "','" + DolorPrice + "','" + this.Name + "');", con);
                            cmd.ExecuteNonQuery();
                            cmdd.ExecuteNonQuery();
                        }
                    }
                    con.Close();


                }
                catch (Exception ee)
                {
                    MessageBox.Show("Second try " + ee.Message);
                }
                dataGridView1.Rows.Clear();
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
            }

            else
            {
                MessageBox.Show("Enter The Given Money");
            }
        }
        }

        public void Delete_Grid_Data(string s)
        {

            
            int y = 0;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() != null)
                {

                    string code = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    if (code.Equals(s))
                    {
                        y = int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                        if (y == 1)
                        {

                            dataGridView1.Rows.RemoveAt(i);
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells[3].Value = y - 1;
                            
                       
                        }


                    }
               
                }
            }
        }

      

    

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            double totalDinarPrice = 0;
            double totalDolorPrice = 0;
            for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
            {
                totalDinarPrice = double.Parse(dataGridView1.Rows[j].Cells[6].Value.ToString()) + totalDinarPrice;
                totalDolorPrice = double.Parse(dataGridView1.Rows[j].Cells[7].Value.ToString()) + totalDolorPrice;
            }
            label3.Text = totalDinarPrice.ToString();
            label16.Text = totalDolorPrice.ToString();
           
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            double totalDinarPrice = 0;
            double totalDolorPrice = 0;
            for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
            {
                totalDinarPrice = double.Parse(dataGridView1.Rows[j].Cells[6].Value.ToString()) + totalDinarPrice;
                totalDolorPrice = double.Parse(dataGridView1.Rows[j].Cells[7].Value.ToString()) + totalDolorPrice;
            }
            label3.Text = totalDinarPrice.ToString();
            label16.Text = totalDolorPrice.ToString();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

            Form3 f = new Form3(double.Parse(textBox1.Text),this.Name);


            f.Owner = this;
            f.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (x == 0)
            {

                f1.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            x = 1;
            Form5 f5 = new Form5(f1, Name, OP);
            f5.Show();
            this.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!textBox4.Text.Equals(""))
            {
                try
                {
                    textBox5.Enabled = false;
                    double DinarPrice = double.Parse(label3.Text);
                    double DolorPrice = double.Parse(label16.Text);
                    double DinarGevinMoney = double.Parse(textBox4.Text);
                    label8.Text = (DinarGevinMoney - DinarPrice).ToString();

                    double oneDolor = 100 / double.Parse(textBox1.Text);
                    double dolorGivenMoney = oneDolor * DinarGevinMoney;
                    label10.Text = (dolorGivenMoney - DolorPrice).ToString();
                }
                catch(Exception)
                {
                    MessageBox.Show("Input A Number");
                }
            }
            else
            {
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                label10.Text = "0";
                label8.Text = "0";

            }




        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (!textBox5.Text.Equals(""))
            {
                try
                {
                    textBox4.Enabled = false;
                    double DinarPrice = double.Parse(label3.Text);
                    double DolorPrice = double.Parse(label16.Text);
                    double DolorGivenMoney = double.Parse(textBox5.Text);
                    label10.Text = (DolorGivenMoney - DolorPrice).ToString();

                   double oneDolor =  double.Parse(textBox1.Text)/100;
                   double DinarGivenMoney = oneDolor * DolorGivenMoney;
                   label8.Text = (DinarGivenMoney - DinarPrice).ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("Input A Number");
                }
            }
            else
            {
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                label10.Text = "0";
                label8.Text = "0";

            }
        }

        private void Form11_Load(object sender, EventArgs e)
        {

        }
    }
}
