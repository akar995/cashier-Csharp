﻿using System;
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
    public partial class Form2 : Form
    {
        public static string Passing_Value;

        public Form2()
        {
            
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {
            (this.Owner as Form11).Delete_Grid_Data(numericUpDown1.Value.ToString());
            numericUpDown1.Value = 0;
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
              
        }
      
    }
}
