using System.Drawing;
using System.Windows.Forms;

namespace CinemaApp
{
    partial class TicketDetailsForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblMovieName;
        private Label lblHallLabel;
        private Label lblHallValue;
        private Label lblSeatsLabel;
        private Label lblSeatsValue;
        private Label lblTimeLabel;
        private Label lblTimeValue;
        private Label lblTicketIDLabel;
        private Label lblTicketIDValue;
        private Button btnClose;
        private Panel pnlTicketCard;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            pnlTicketCard = new Panel();
            lblTicketIDValue = new Label();
            lblTicketIDLabel = new Label();
            lblTimeValue = new Label();
            lblTimeLabel = new Label();
            lblSeatsValue = new Label();
            lblSeatsLabel = new Label();
            lblHallValue = new Label();
            lblHallLabel = new Label();
            lblMovieName = new Label();
            btnClose = new Button();
            pnlTicketCard.SuspendLayout();
            SuspendLayout();

            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(41, 128, 185);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Location = new Point(0, 30);
            lblTitle.Size = new Size(500, 50);
            // 
            // pnlTicketCard
            // 
            pnlTicketCard.BackColor = Color.FromArgb(236, 240, 241); // لون فاتح للخلفية
            pnlTicketCard.BorderStyle = BorderStyle.FixedSingle;
            pnlTicketCard.Controls.Add(lblTicketIDValue);
            pnlTicketCard.Controls.Add(lblTicketIDLabel);
            pnlTicketCard.Controls.Add(lblTimeValue);
            pnlTicketCard.Controls.Add(lblTimeLabel);
            pnlTicketCard.Controls.Add(lblSeatsValue);
            pnlTicketCard.Controls.Add(lblSeatsLabel);
            pnlTicketCard.Controls.Add(lblHallValue);
            pnlTicketCard.Controls.Add(lblHallLabel);
            pnlTicketCard.Controls.Add(lblMovieName);
            pnlTicketCard.Location = new Point(50, 90);
            pnlTicketCard.Size = new Size(400, 300);
            // 
            // lblMovieName
            // 
            lblMovieName.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblMovieName.ForeColor = Color.FromArgb(44, 62, 80);
            lblMovieName.TextAlign = ContentAlignment.MiddleCenter;
            lblMovieName.Location = new Point(0, 20);
            lblMovieName.Size = new Size(400, 40);
            // 
            // Labels for details
            // 
            Font labelFont = new Font("Segoe UI", 12F, FontStyle.Regular);
            Color labelColor = Color.FromArgb(127, 140, 141);
            Color valueColor = Color.FromArgb(52, 73, 94);

            int labelX = 30;
            int valueX = 180;
            int startY = 80;
            int spacing = 35;

            // Hall
            lblHallLabel = CreateLabel("Hall:", labelFont, labelColor, new Point(labelX, startY));
            lblHallValue = CreateLabel("N/A", labelFont, valueColor, new Point(valueX, startY));
            pnlTicketCard.Controls.Add(lblHallLabel);
            pnlTicketCard.Controls.Add(lblHallValue);

            // Seats
            lblSeatsLabel = CreateLabel("Seats:", labelFont, labelColor, new Point(labelX, startY + spacing));
            lblSeatsValue = CreateLabel("N/A", labelFont, valueColor, new Point(valueX, startY + spacing));
            pnlTicketCard.Controls.Add(lblSeatsLabel);
            pnlTicketCard.Controls.Add(lblSeatsValue);

            // Time
            lblTimeLabel = CreateLabel("Time:", labelFont, labelColor, new Point(labelX, startY + 2 * spacing));
            lblTimeValue = CreateLabel("N/A", labelFont, valueColor, new Point(valueX, startY + 2 * spacing));
            pnlTicketCard.Controls.Add(lblTimeLabel);
            pnlTicketCard.Controls.Add(lblTimeValue);

            // Ticket ID
            lblTicketIDLabel = CreateLabel("Ticket ID:", new Font("Segoe UI", 14F, FontStyle.Bold), Color.FromArgb(41, 128, 185), new Point(labelX, startY + 3 * spacing + 10));
            lblTicketIDValue = CreateLabel("N/A", new Font("Segoe UI", 14F, FontStyle.Bold), Color.FromArgb(41, 128, 185), new Point(valueX, startY + 3 * spacing + 10));
            pnlTicketCard.Controls.Add(lblTicketIDLabel);
            pnlTicketCard.Controls.Add(lblTicketIDValue);

            // 
            // btnClose
            // 
            btnClose.BackColor = Color.FromArgb(41, 128, 185);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.ForeColor = Color.White;
            btnClose.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnClose.Text = "Close / Go Home";
            btnClose.Location = new Point(50, 420);
            btnClose.Size = new Size(400, 50);

            // 
            // TicketDetailsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(500, 500);
            Controls.Add(btnClose);
            Controls.Add(pnlTicketCard);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TicketDetailsForm";
            StartPosition = FormStartPosition.CenterScreen;
            pnlTicketCard.ResumeLayout(false);
            pnlTicketCard.PerformLayout();
            ResumeLayout(false);
        }

        private Label CreateLabel(string text, Font font, Color foreColor, Point location)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = font;
            lbl.ForeColor = foreColor;
            lbl.Location = location;
            lbl.AutoSize = true;
            return lbl;
        }
    }
}
