namespace CinemaApp
{
    
        partial class AddShowTimeForm
        {
            private System.ComponentModel.IContainer components = null;

            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                    components.Dispose();
                base.Dispose(disposing);
            }

            #region Windows Form Designer generated code

            private void InitializeComponent()
            {
                this.lblTitle = new System.Windows.Forms.Label();
                this.cmbMovies = new System.Windows.Forms.ComboBox();
                this.cmbHalls = new System.Windows.Forms.ComboBox();
                this.lblMovie = new System.Windows.Forms.Label();
                this.lblHall = new System.Windows.Forms.Label();
                this.lblShowTime = new System.Windows.Forms.Label();
                this.dtpShowTime = new System.Windows.Forms.DateTimePicker();
                this.btnSaveShowTime = new System.Windows.Forms.Button();
                this.SuspendLayout();
                // 
                // lblTitle
                // 
                this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
                this.lblTitle.ForeColor = System.Drawing.Color.MediumBlue;
                this.lblTitle.Location = new System.Drawing.Point(0, 10);
                this.lblTitle.Name = "lblTitle";
                this.lblTitle.Size = new System.Drawing.Size(400, 40);
                this.lblTitle.TabIndex = 0;
                this.lblTitle.Text = "➕ Add ShowTime";
                this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                // 
                // lblMovie
                // 
                this.lblMovie.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
                this.lblMovie.Location = new System.Drawing.Point(30, 60);
                this.lblMovie.Name = "lblMovie";
                this.lblMovie.Size = new System.Drawing.Size(100, 25);
                this.lblMovie.TabIndex = 1;
                this.lblMovie.Text = "Select Movie:";
                // 
                // cmbMovies
                // 
                this.cmbMovies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.cmbMovies.Font = new System.Drawing.Font("Segoe UI", 11F);
                this.cmbMovies.FormattingEnabled = true;
                this.cmbMovies.Location = new System.Drawing.Point(30, 90);
                this.cmbMovies.Name = "cmbMovies";
                this.cmbMovies.Size = new System.Drawing.Size(340, 28);
                this.cmbMovies.TabIndex = 2;
                // 
                // lblHall
                // 
                this.lblHall.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
                this.lblHall.Location = new System.Drawing.Point(30, 130);
                this.lblHall.Name = "lblHall";
                this.lblHall.Size = new System.Drawing.Size(100, 25);
                this.lblHall.TabIndex = 3;
                this.lblHall.Text = "Select Hall:";
                // 
                // cmbHalls
                // 
                this.cmbHalls.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.cmbHalls.Font = new System.Drawing.Font("Segoe UI", 11F);
                this.cmbHalls.FormattingEnabled = true;
                this.cmbHalls.Location = new System.Drawing.Point(30, 160);
                this.cmbHalls.Name = "cmbHalls";
                this.cmbHalls.Size = new System.Drawing.Size(340, 28);
                this.cmbHalls.TabIndex = 4;
                // 
                // lblShowTime
                // 
                this.lblShowTime.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
                this.lblShowTime.Location = new System.Drawing.Point(30, 200);
                this.lblShowTime.Name = "lblShowTime";
                this.lblShowTime.Size = new System.Drawing.Size(100, 25);
                this.lblShowTime.TabIndex = 5;
                this.lblShowTime.Text = "ShowTime:";
                // 
                // dtpShowTime
                // 
                this.dtpShowTime.CustomFormat = "yyyy-MM-dd HH:mm";
                this.dtpShowTime.Font = new System.Drawing.Font("Segoe UI", 11F);
                this.dtpShowTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                this.dtpShowTime.Location = new System.Drawing.Point(30, 230);
                this.dtpShowTime.Name = "dtpShowTime";
                this.dtpShowTime.Size = new System.Drawing.Size(340, 27);
                this.dtpShowTime.TabIndex = 6;
                // 
                // btnSaveShowTime
                // 
                this.btnSaveShowTime.BackColor = System.Drawing.Color.MediumBlue;
                this.btnSaveShowTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.btnSaveShowTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
                this.btnSaveShowTime.ForeColor = System.Drawing.Color.White;
                this.btnSaveShowTime.Location = new System.Drawing.Point(100, 280);
                this.btnSaveShowTime.Name = "btnSaveShowTime";
                this.btnSaveShowTime.Size = new System.Drawing.Size(200, 45);
                this.btnSaveShowTime.TabIndex = 7;
                this.btnSaveShowTime.Text = "Save ShowTime";
                this.btnSaveShowTime.UseVisualStyleBackColor = false;
                this.btnSaveShowTime.Click += new System.EventHandler(this.btnSaveShowTime_Click);
                // 
                // AddShowTimeForm
                // 
                this.ClientSize = new System.Drawing.Size(400, 350);
                this.Controls.Add(this.btnSaveShowTime);
                this.Controls.Add(this.dtpShowTime);
                this.Controls.Add(this.lblShowTime);
                this.Controls.Add(this.cmbHalls);
                this.Controls.Add(this.lblHall);
                this.Controls.Add(this.cmbMovies);
                this.Controls.Add(this.lblMovie);
                this.Controls.Add(this.lblTitle);
                this.Font = new System.Drawing.Font("Segoe UI", 10F);
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                this.Name = "AddShowTimeForm";
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                this.Text = "Add ShowTime";
                this.ResumeLayout(false);
            }

            #endregion

            private System.Windows.Forms.Label lblTitle;
            private System.Windows.Forms.Label lblMovie;
            private System.Windows.Forms.ComboBox cmbMovies;
            private System.Windows.Forms.Label lblHall;
            private System.Windows.Forms.ComboBox cmbHalls;
            private System.Windows.Forms.Label lblShowTime;
            private System.Windows.Forms.DateTimePicker dtpShowTime;
            private System.Windows.Forms.Button btnSaveShowTime;
        }
    }

