using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Services; 
using Models; 
using Microsoft.FSharp.Core; 

namespace CinemaApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            btnLogin.Click += BtnLogin_Click;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please Enter Email and Password", "Login Falid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = UserService.login(email, password);

            if (result.IsOk)
            {
                dynamic loggedUser = result.ResultValue;
                int userId = loggedUser.UserId;
                string userName = loggedUser.Name;
                bool role = loggedUser.Role; // false = admin, true = user

                if (!role) //admin
                {
                    var openAdminForm = Application.OpenForms.OfType<AdminHomeForm>().FirstOrDefault();
                    if (openAdminForm == null)
                    {
                        AdminHomeForm adminHome = new AdminHomeForm();
                        adminHome.Show();
                    }
                    else
                    {
                        openAdminForm.BringToFront();
                    }
                }
                else //  user
                {
                    var openUserForm = Application.OpenForms.OfType<UserHomePage>().FirstOrDefault();
                    if (openUserForm == null)
                    {
                        UserHomePage home = new UserHomePage(userId, userName);
                        home.Show();
                    }
                    else
                    {
                        openUserForm.BringToFront();
                    }
                }

                this.Hide(); 
            }
            else
            {
                string errorMessage = (string)result.ErrorValue;
                MessageBox.Show(errorMessage, "Login Falid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}