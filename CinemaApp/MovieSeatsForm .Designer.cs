using System.Drawing;

using System.Windows.Forms;



namespace CinemaApp
{
    partial class MovieSeatsForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlMainContainer;
        private Panel pnlHeader;
        private Label lblTitle;
        private Panel pnlScreenArea;
        private Panel pnlScreen;
        private Label lblScreenText;
        private Panel pnlSeatingArea;
        private FlowLayoutPanel flpSeatingContainer;
        private Panel pnlFooter;
        private Button btnBook;
        private Panel pnlLegend;
        private Panel pnlAvailableExample;
        private Label lblAvailable;
        private Panel pnlSelectedExample;
        private Label lblSelected;
        private Panel pnlBookedExample;
        private Label lblBooked;
        private Panel pnlRowLabels;
        private Panel pnlColumnLabels;
        private Label lblSelectedCount;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MovieSeatsForm));
            pnlMainContainer = new Panel();
            pnlHeader = new Panel();
            lblTitle = new Label();
            pnlScreenArea = new Panel();
            pnlScreen = new Panel();
            lblScreenText = new Label();
            pnlSeatingArea = new Panel();
            pnlColumnLabels = new Panel();
            pnlRowLabels = new Panel();
            flpSeatingContainer = new FlowLayoutPanel();
            pnlFooter = new Panel();
            pnlLegend = new Panel();
            pnlBookedExample = new Panel();
            lblBooked = new Label();
            pnlSelectedExample = new Panel();
            lblSelected = new Label();
            pnlAvailableExample = new Panel();
            lblAvailable = new Label();
            lblSelectedCount = new Label();
            btnBook = new Button();
            pnlMainContainer.SuspendLayout();
            pnlHeader.SuspendLayout();
            pnlScreenArea.SuspendLayout();
            pnlScreen.SuspendLayout();
            pnlSeatingArea.SuspendLayout();
            pnlFooter.SuspendLayout();
            pnlLegend.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMainContainer
            // 
            pnlMainContainer.BackColor = Color.White;
            pnlMainContainer.Controls.Add(pnlSeatingArea);
            pnlMainContainer.Controls.Add(pnlScreenArea);
            pnlMainContainer.Controls.Add(pnlHeader);
            pnlMainContainer.Controls.Add(pnlFooter);
            pnlMainContainer.Dock = DockStyle.Fill;
            pnlMainContainer.Location = new Point(0, 0);
            pnlMainContainer.Name = "pnlMainContainer";
            pnlMainContainer.Size = new Size(1622, 838);
            pnlMainContainer.TabIndex = 0;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.BorderStyle = BorderStyle.FixedSingle;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1622, 80);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(64, 64, 64);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1620, 78);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🎬 SELECT YOUR SEATS";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlScreenArea
            // 
            pnlScreenArea.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlScreenArea.BackColor = Color.White;
            pnlScreenArea.Controls.Add(pnlScreen);
            pnlScreenArea.Location = new Point(0, 80);
            pnlScreenArea.Name = "pnlScreenArea";
            pnlScreenArea.Padding = new Padding(100, 20, 100, 10);
            pnlScreenArea.Size = new Size(1622, 90);
            pnlScreenArea.TabIndex = 1;
            // 
            // pnlScreen
            // 
            pnlScreen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlScreen.BackColor = Color.Gray;
            pnlScreen.Controls.Add(lblScreenText);
            pnlScreen.Location = new Point(100, 20);
            pnlScreen.Name = "pnlScreen";
            pnlScreen.Size = new Size(1422, 50);
            pnlScreen.TabIndex = 0;
            // 
            // lblScreenText
            // 
            lblScreenText.Dock = DockStyle.Fill;
            lblScreenText.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblScreenText.ForeColor = Color.FromArgb(255, 255, 255);
            lblScreenText.Location = new Point(0, 0);
            lblScreenText.Name = "lblScreenText";
            lblScreenText.Size = new Size(1422, 50);
            lblScreenText.TabIndex = 0;
            lblScreenText.Text = "SCREEN";
            lblScreenText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlSeatingArea
            // 
            pnlSeatingArea.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlSeatingArea.BackColor = Color.White;
            pnlSeatingArea.Controls.Add(pnlColumnLabels);
            pnlSeatingArea.Controls.Add(pnlRowLabels);
            pnlSeatingArea.Controls.Add(flpSeatingContainer);
            pnlSeatingArea.Location = new Point(0, 170);
            pnlSeatingArea.Name = "pnlSeatingArea";
            pnlSeatingArea.Padding = new Padding(120, 20, 120, 20);
            pnlSeatingArea.Size = new Size(1622, 480);
            pnlSeatingArea.TabIndex = 2;
            // 
            // pnlColumnLabels
            // 
            pnlColumnLabels.BackColor = Color.Transparent;
            pnlColumnLabels.Location = new Point(160, 20);
            pnlColumnLabels.Name = "pnlColumnLabels";
            pnlColumnLabels.Size = new Size(1342, 30);
            pnlColumnLabels.TabIndex = 2;
            // 
            // pnlRowLabels
            // 
            pnlRowLabels.BackColor = Color.Transparent;
            pnlRowLabels.Location = new Point(120, 20);
            pnlRowLabels.Name = "pnlRowLabels";
            pnlRowLabels.Size = new Size(40, 430);
            pnlRowLabels.TabIndex = 1;
            // 
            // flpSeatingContainer
            // 
            flpSeatingContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flpSeatingContainer.AutoScroll = true;
            flpSeatingContainer.BackColor = Color.White;
            flpSeatingContainer.FlowDirection = FlowDirection.TopDown;
            flpSeatingContainer.Location = new Point(160, 50);
            flpSeatingContainer.Name = "flpSeatingContainer";
            flpSeatingContainer.Size = new Size(1342, 400);
            flpSeatingContainer.TabIndex = 0;
            flpSeatingContainer.WrapContents = false;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.White;
            pnlFooter.BorderStyle = BorderStyle.FixedSingle;
            pnlFooter.Controls.Add(pnlLegend);
            pnlFooter.Controls.Add(lblSelectedCount);
            pnlFooter.Controls.Add(btnBook);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 650);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(1622, 188);
            pnlFooter.TabIndex = 3;
            // 
            // pnlLegend
            // 
            pnlLegend.BackColor = Color.Transparent;
            pnlLegend.Controls.Add(pnlBookedExample);
            pnlLegend.Controls.Add(lblBooked);
            pnlLegend.Controls.Add(pnlSelectedExample);
            pnlLegend.Controls.Add(lblSelected);
            pnlLegend.Controls.Add(pnlAvailableExample);
            pnlLegend.Controls.Add(lblAvailable);
            pnlLegend.Location = new Point(120, 30);
            pnlLegend.Name = "pnlLegend";
            pnlLegend.Size = new Size(500, 60);
            pnlLegend.TabIndex = 3;
            // 
            // pnlBookedExample
            // 
            pnlBookedExample.BackColor = Color.FromArgb(128, 128, 128);
            pnlBookedExample.Location = new Point(340, 15);
            pnlBookedExample.Name = "pnlBookedExample";
            pnlBookedExample.Size = new Size(30, 30);
            pnlBookedExample.TabIndex = 4;
            // 
            // lblBooked
            // 
            lblBooked.AutoSize = true;
            lblBooked.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBooked.ForeColor = Color.FromArgb(64, 64, 64);
            lblBooked.Location = new Point(380, 18);
            lblBooked.Name = "lblBooked";
            lblBooked.Size = new Size(100, 28);
            lblBooked.TabIndex = 5;
            lblBooked.Text = "Not Available";
            // 
            // pnlSelectedExample
            // 
            pnlSelectedExample.BackColor = Color.FromArgb(28, 103, 201); // Updated to RGB(159, 67, 101)
            pnlSelectedExample.Location = new Point(170, 15);
            pnlSelectedExample.Name = "pnlSelectedExample";
            pnlSelectedExample.Size = new Size(30, 30);
            pnlSelectedExample.TabIndex = 2;
            // 
            // lblSelected
            // 
            lblSelected.AutoSize = true;
            lblSelected.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSelected.ForeColor = Color.FromArgb(64, 64, 64);
            lblSelected.Location = new Point(210, 18);
            lblSelected.Name = "lblSelected";
            lblSelected.Size = new Size(115, 28);
            lblSelected.TabIndex = 3;
            lblSelected.Text = "Your Selection";
            // 
            // pnlAvailableExample
            // 
            pnlAvailableExample.BackColor = Color.FromArgb(112, 147, 189);
            pnlAvailableExample.Location = new Point(20, 15);
            pnlAvailableExample.Name = "pnlAvailableExample";
            pnlAvailableExample.Size = new Size(30, 30);
            pnlAvailableExample.TabIndex = 0;
            // 
            // lblAvailable
            // 
            lblAvailable.AutoSize = true;
            lblAvailable.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAvailable.ForeColor = Color.FromArgb(64, 64, 64);
            lblAvailable.Location = new Point(60, 18);
            lblAvailable.Name = "lblAvailable";
            lblAvailable.Size = new Size(89, 28);
            lblAvailable.TabIndex = 1;
            lblAvailable.Text = "Available";
            // 
            // lblSelectedCount
            // 
            lblSelectedCount.Anchor = AnchorStyles.Top;
            lblSelectedCount.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSelectedCount.ForeColor = Color.FromArgb(64, 64, 64);
            lblSelectedCount.Location = new Point(640, 30);
            lblSelectedCount.Name = "lblSelectedCount";
            lblSelectedCount.Size = new Size(340, 40);
            lblSelectedCount.TabIndex = 2;
            lblSelectedCount.Text = "Selected: 0 seats";
            lblSelectedCount.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnBook
            // 
            btnBook.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBook.BackColor = Color.FromArgb(112, 147, 189);
            btnBook.Cursor = Cursors.Hand;
            btnBook.FlatAppearance.BorderSize = 0;
            btnBook.FlatAppearance.MouseDownBackColor = Color.FromArgb(112, 147, 189);
            btnBook.FlatAppearance.MouseOverBackColor = Color.FromArgb(112, 147, 189);
            btnBook.FlatStyle = FlatStyle.Flat;
            btnBook.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBook.ForeColor = Color.White;
            btnBook.Location = new Point(1100, 30);
            btnBook.Name = "btnBook";
            btnBook.Size = new Size(250, 80);
            btnBook.TabIndex = 1;
            btnBook.Text = "CONFIRM BOOKING\r\n(0 SEATS)";
            btnBook.UseVisualStyleBackColor = false;
            btnBook.Click += btnBook_Click;
            // 
            // MovieSeatsForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1622, 838);
            Controls.Add(pnlMainContainer);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1640, 885);
            Name = "MovieSeatsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cinema Seat Selection";
            Load += new System.EventHandler(this.MovieSeatsForm_Load);
            pnlMainContainer.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlScreenArea.ResumeLayout(false);
            pnlScreen.ResumeLayout(false);
            pnlSeatingArea.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            pnlLegend.ResumeLayout(false);
            pnlLegend.PerformLayout();
            ResumeLayout(false);
        }
        #endregion
    }
}
