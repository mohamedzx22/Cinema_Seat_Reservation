using System;
using System.Drawing;
using System.Windows.Forms;

namespace CinemaApp
{
    partial class UserHomePage
    {
        private Label lblWelcome;
        private ListView lvBookings;
        private Button btnBookMovie;
        private Button btnCancelBooking;

        private void InitializeComponent()
        {
            this.Text = "User Home Page";
            this.Size = new Size(1646, 902);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // ===== Welcome Label =====
            lblWelcome = new Label
            {
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                AutoSize = true,
                Location = new Point(30, 20)
            };
            this.Controls.Add(lblWelcome);

            // ===== Bookings ListView =====
            lvBookings = new ListView
            {
                Size = new Size(740, 300),
                Location = new Point(30, 70),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true
            };

            lvBookings.Columns.Add("Ticket ID", 120);
            lvBookings.Columns.Add("Movie", 200);
            lvBookings.Columns.Add("Hall", 150);
            lvBookings.Columns.Add("Seat", 100);
            lvBookings.Columns.Add("Date", 150);

            this.Controls.Add(lvBookings);

            // ===== Book Movie Button =====
            btnBookMovie = new Button
            {
                Text = "Book Movie",
                Size = new Size(200, 50),
                Location = new Point(200, 400),
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnBookMovie.FlatAppearance.BorderSize = 0;
            btnBookMovie.Click += BtnBookMovie_Click;
            this.Controls.Add(btnBookMovie);

            // ===== Cancel Booking Button =====
            btnCancelBooking = new Button
            {
                Text = "Cancel Booking",
                Size = new Size(200, 50),
                Location = new Point(400, 400),
                BackColor = Color.FromArgb(192, 57, 43),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancelBooking.FlatAppearance.BorderSize = 0;
            btnCancelBooking.Click += BtnCancelBooking_Click;
            this.Controls.Add(btnCancelBooking);
        }
    }
}
