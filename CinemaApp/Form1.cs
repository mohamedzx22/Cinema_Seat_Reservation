using CinemaApp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CinemaApp
{
  
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegisterForm R = new RegisterForm();
            R.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
          
        }

        private void label2_Click(object sender, EventArgs e)
        {
           
        }


        private void label2_Click_1(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }
    }
}