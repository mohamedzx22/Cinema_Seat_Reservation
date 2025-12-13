
namespace CinemaApp
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private Button btnLogin;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Arial", 24F, FontStyle.Bold);
            lblTitle.Location = new Point(548, 48);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(400, 68);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Login";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Click += LblTitle_Click; // مربوط method موجودة
            // 
            // txtEmail
            // 
            txtEmail.BackColor = SystemColors.InactiveBorder;
            txtEmail.Location = new Point(594, 195);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "Enter Email";
            txtEmail.Size = new Size(300, 35);
            txtEmail.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = SystemColors.InactiveBorder;
            txtPassword.Location = new Point(594, 268);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Enter Password";
            txtPassword.Size = new Size(300, 35);
            txtPassword.TabIndex = 2;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.LightSteelBlue;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Location = new Point(594, 346);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(300, 40);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += BtnLogin_Click;
            // 
            // LoginForm
            // 
            BackColor = SystemColors.Window;
            ClientSize = new Size(1622, 838);
            Controls.Add(lblTitle);
            Controls.Add(txtEmail);
            Controls.Add(txtPassword);
            Controls.Add(btnLogin);
            Name = "LoginForm";
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        private void LblTitle_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
