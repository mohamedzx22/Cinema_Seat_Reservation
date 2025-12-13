namespace CinemaApp
{
    partial class AddMovieWithShowTimeForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblHallName;
        private System.Windows.Forms.TextBox txtHallName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.ListBox lstShowTimes;
        private System.Windows.Forms.Button btnAddShowTime;
        private System.Windows.Forms.Button btnSaveMovie;

        // ✅ DateTimePicker لإدخال التاريخ والوقت
        private System.Windows.Forms.DateTimePicker dtpShowTime;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblHallName = new System.Windows.Forms.Label();
            this.txtHallName = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.lstShowTimes = new System.Windows.Forms.ListBox();
            this.btnAddShowTime = new System.Windows.Forms.Button();
            this.btnSaveMovie = new System.Windows.Forms.Button();
            this.dtpShowTime = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(0, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Add Movie + ShowTimes";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblHallName
            this.lblHallName.AutoSize = true;
            this.lblHallName.Location = new System.Drawing.Point(30, 70);
            this.lblHallName.Name = "lblHallName";
            this.lblHallName.Size = new System.Drawing.Size(80, 25);
            this.lblHallName.TabIndex = 1;
            this.lblHallName.Text = "Hall Name";

            // txtHallName
            this.txtHallName.Location = new System.Drawing.Point(120, 67);
            this.txtHallName.Name = "txtHallName";
            this.txtHallName.Size = new System.Drawing.Size(250, 30);
            this.txtHallName.TabIndex = 2;

            // txtName
            this.txtName.Location = new System.Drawing.Point(30, 110);
            this.txtName.Name = "txtName";
            this.txtName.PlaceholderText = "Movie Name";
            this.txtName.Size = new System.Drawing.Size(340, 30);
            this.txtName.TabIndex = 3;

            // txtDuration
            this.txtDuration.Location = new System.Drawing.Point(30, 150);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.PlaceholderText = "Duration (minutes)";
            this.txtDuration.Size = new System.Drawing.Size(340, 30);
            this.txtDuration.TabIndex = 4;

            // dtpShowTime
            this.dtpShowTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpShowTime.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpShowTime.ShowUpDown = true;
            this.dtpShowTime.Location = new System.Drawing.Point(30, 190);
            this.dtpShowTime.Name = "dtpShowTime";
            this.dtpShowTime.Size = new System.Drawing.Size(240, 30);
            this.dtpShowTime.TabIndex = 5;

            // btnAddShowTime
            this.btnAddShowTime.BackColor = System.Drawing.Color.MediumPurple;
            this.btnAddShowTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddShowTime.ForeColor = System.Drawing.Color.White;
            this.btnAddShowTime.Location = new System.Drawing.Point(280, 190);
            this.btnAddShowTime.Name = "btnAddShowTime";
            this.btnAddShowTime.Size = new System.Drawing.Size(90, 30);
            this.btnAddShowTime.TabIndex = 6;
            this.btnAddShowTime.Text = "Add";
            this.btnAddShowTime.UseVisualStyleBackColor = false;
            this.btnAddShowTime.Click += new System.EventHandler(this.btnAddShowTime_Click);

            // lstShowTimes
            this.lstShowTimes.FormattingEnabled = true;
            this.lstShowTimes.ItemHeight = 24;
            this.lstShowTimes.Location = new System.Drawing.Point(30, 230);
            this.lstShowTimes.Name = "lstShowTimes";
            this.lstShowTimes.Size = new System.Drawing.Size(340, 100);
            this.lstShowTimes.TabIndex = 7;

            // btnSaveMovie
            this.btnSaveMovie.BackColor = System.Drawing.Color.MediumBlue;
            this.btnSaveMovie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveMovie.ForeColor = System.Drawing.Color.White;
            this.btnSaveMovie.Location = new System.Drawing.Point(100, 350);
            this.btnSaveMovie.Name = "btnSaveMovie";
            this.btnSaveMovie.Size = new System.Drawing.Size(200, 45);
            this.btnSaveMovie.TabIndex = 8;
            this.btnSaveMovie.Text = "Save Movie";
            this.btnSaveMovie.UseVisualStyleBackColor = false;
            this.btnSaveMovie.Click += new System.EventHandler(this.btnSaveMovie_Click);

            // Add controls
            this.Controls.Add(this.btnSaveMovie);
            this.Controls.Add(this.lstShowTimes);
            this.Controls.Add(this.btnAddShowTime);
            this.Controls.Add(this.dtpShowTime);
            this.Controls.Add(this.txtDuration);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtHallName);
            this.Controls.Add(this.lblHallName);
            this.Controls.Add(this.lblTitle);

            this.ClientSize = new System.Drawing.Size(400, 420);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddMovieWithShowTimeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Movie";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
