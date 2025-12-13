namespace CinemaApp
{
    partial class AdminHomeForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing != null && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnAddMovieWithTime = new System.Windows.Forms.Button();
            this.btnAddShowTime = new System.Windows.Forms.Button();
            this.btnViewMovies = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblTitle.Location = new System.Drawing.Point(0, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(540, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🎬 Admin Dashboard";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // btnAddMovieWithTime
            // 
            this.btnAddMovieWithTime.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnAddMovieWithTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddMovieWithTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddMovieWithTime.ForeColor = System.Drawing.Color.White;
            this.btnAddMovieWithTime.Location = new System.Drawing.Point(60, 100);
            this.btnAddMovieWithTime.Name = "btnAddMovieWithTime";
            this.btnAddMovieWithTime.Size = new System.Drawing.Size(420, 60);
            this.btnAddMovieWithTime.TabIndex = 1;
            this.btnAddMovieWithTime.Text = "➕ Add Movie + ShowTimes";
            this.btnAddMovieWithTime.UseVisualStyleBackColor = false;
            this.btnAddMovieWithTime.Click += new System.EventHandler(this.btnAddMovieWithTime_Click);

            // 
            // btnAddShowTime
            // 
            this.btnAddShowTime.BackColor = System.Drawing.Color.MediumPurple;
            this.btnAddShowTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddShowTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddShowTime.ForeColor = System.Drawing.Color.White;
            this.btnAddShowTime.Location = new System.Drawing.Point(60, 180);
            this.btnAddShowTime.Name = "btnAddShowTime";
            this.btnAddShowTime.Size = new System.Drawing.Size(420, 60);
            this.btnAddShowTime.TabIndex = 2;
            this.btnAddShowTime.Text = "⏰ Add ShowTime";
            this.btnAddShowTime.UseVisualStyleBackColor = false;
            this.btnAddShowTime.Click += new System.EventHandler(this.btnAddShowTime_Click);

            // 
            // btnViewMovies
            // 
            this.btnViewMovies.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnViewMovies.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewMovies.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnViewMovies.ForeColor = System.Drawing.Color.White;
            this.btnViewMovies.Location = new System.Drawing.Point(60, 260);
            this.btnViewMovies.Name = "btnViewMovies";
            this.btnViewMovies.Size = new System.Drawing.Size(420, 60);
            this.btnViewMovies.TabIndex = 3;
            this.btnViewMovies.Text = "🎞️ View Movies";
            this.btnViewMovies.UseVisualStyleBackColor = false;
            this.btnViewMovies.Click += new System.EventHandler(this.btnViewMovies_Click);

            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.OrangeRed;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(60, 340);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(420, 60);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "🚪 Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            // 
            // AdminHomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 420);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnAddMovieWithTime);
            this.Controls.Add(this.btnAddShowTime);
            this.Controls.Add(this.btnViewMovies);
            this.Controls.Add(this.btnLogout);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AdminHomeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin Dashboard";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnAddMovieWithTime;
        private System.Windows.Forms.Button btnAddShowTime;
        private System.Windows.Forms.Button btnViewMovies;
        private System.Windows.Forms.Button btnLogout;
    }
}
