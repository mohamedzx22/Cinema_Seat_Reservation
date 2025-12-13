using System;
using System.Windows.Forms;
using Services; 
using Microsoft.FSharp.Core; 

namespace CinemaApp
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
         

            if (string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password))
            {
                MessageBox.Show("All fields are required!", "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!email.Contains("@"))
            {
                MessageBox.Show("Please enter a valid email address!", "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password.Length < 4)
            {
                MessageBox.Show("Password must be at least 4 characters.", "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                FSharpResult<int, string> result = UserService.register(username, email, password);

                if (result.IsOk)
                {
                    int newUserId = result.ResultValue; 
                    MessageBox.Show("Registration successful! You can now login.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoginForm login = new LoginForm();
                    login.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(result.ErrorValue, "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
