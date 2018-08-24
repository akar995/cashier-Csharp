using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            

            InitializeComponent();
        
           
        


            
        }



    

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            SplashScreen sp = new SplashScreen();
            Thread.Sleep(1000);
            if (panel1.Visible == true)
            {
                
                panel1.Visible = false;
                panel2.Visible = true;
                panel3.Visible = false;
                Thread.Sleep(1000);
            }
            if (panel1.Visible == true)
            {
                
                panel1.Visible = false;
                panel2.Visible = false;
                panel3.Visible = true;
            }

        }

       

            
        
       
        
      
    }
}
