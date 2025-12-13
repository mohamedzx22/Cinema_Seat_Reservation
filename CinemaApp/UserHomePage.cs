using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Services;

namespace CinemaApp
{
    public partial class UserHomePage : Form
    {
        private int userId;
        private string userName;
        private FlowLayoutPanel cardsPanel;
        private Label statusLabel;
        private Panel noBookingsPanel;
        private Panel selectedCard = null;

        public UserHomePage(int userId, string userName)
        {
            this.userId = userId;
            this.userName = userName;

            InitializeComponent();
            SetupCardBasedUI();
            LoadUserBookings();
        }

        private void SetupCardBasedUI()
        {
            this.Text = $"Cinema Dashboard - {userName}";
            this.Size = new Size(1646, 902);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(248, 248, 252);

            lvBookings.Visible = false;

            lblWelcome.Text = $"🎬 Welcome, {userName}!";
            lblWelcome.Font = new Font("Segoe UI Semibold", 28, FontStyle.Bold);
            lblWelcome.ForeColor = Color.FromArgb(26, 35, 126);
            lblWelcome.Location = new Point(50, 30);
            lblWelcome.AutoSize = true;

            cardsPanel = new FlowLayoutPanel
            {
                Location = new Point(50, 100),
                Size = new Size(1546, 600),
                AutoScroll = true,
                BackColor = Color.Transparent,
                Padding = new Padding(10),
                WrapContents = true,
                AutoSize = false
            };
            this.Controls.Add(cardsPanel);

            noBookingsPanel = new Panel
            {
                Size = new Size(600, 300),
                Location = new Point((this.ClientSize.Width - 600) / 2, 250),
                BackColor = Color.Transparent,
                Visible = false
            };

            PictureBox noBookingsIcon = new PictureBox
            {
                Image = SystemIcons.Information.ToBitmap(),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(80, 80),
                Location = new Point(260, 20)
            };

            Label noBookingsLabel = new Label
            {
                Text = "No Bookings Yet",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(97, 97, 97),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false,
                Size = new Size(600, 50),
                Location = new Point(0, 120)
            };

            Label noBookingsSubLabel = new Label
            {
                Text = "Book your first movie to get started!",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.FromArgb(97, 97, 97),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false,
                Size = new Size(600, 30),
                Location = new Point(0, 180)
            };

            noBookingsPanel.Controls.Add(noBookingsIcon);
            noBookingsPanel.Controls.Add(noBookingsLabel);
            noBookingsPanel.Controls.Add(noBookingsSubLabel);
            this.Controls.Add(noBookingsPanel);

            statusLabel = new Label
            {
                Text = "Loading bookings...",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(97, 97, 97),
                AutoSize = true,
                Location = new Point(50, 720)
            };
            this.Controls.Add(statusLabel);

            int buttonY = 780;
            int totalButtonWidth = 480;
            int buttonStartX = (this.ClientSize.Width - totalButtonWidth) / 2;

            btnBookMovie.Location = new Point(buttonStartX, buttonY);
            btnCancelBooking.Location = new Point(buttonStartX + 250, buttonY);

            StyleButton(btnBookMovie, Color.FromArgb(41, 128, 185), Color.FromArgb(52, 152, 219));
            StyleButton(btnCancelBooking, Color.FromArgb(231, 76, 60), Color.FromArgb(192, 57, 43));

            btnBookMovie.Text = " BOOK NEW MOVIE";
            btnBookMovie.Size = new Size(230, 60);
            btnCancelBooking.Text = "CANCEL BOOKING";
            btnCancelBooking.Size = new Size(230, 60);

            btnCancelBooking.Enabled = false;
            btnCancelBooking.BackColor = Color.FromArgb(180, 180, 180);
        }

        private void StyleButton(Button button, Color normalColor, Color hoverColor)
        {
            button.Font = new Font("Segoe UI Semibold", 12, FontStyle.Bold);
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;

            ApplyRoundedCorners(button, 10);

            button.MouseEnter += (sender, e) =>
            {
                if (button.Enabled) button.BackColor = hoverColor;
            };

            button.MouseLeave += (sender, e) =>
            {
                if (button.Enabled) button.BackColor = normalColor;
            };

            button.BackColor = normalColor;
        }

        private void ApplyRoundedCorners(Control control, int radius)
        {
            control.Paint += (sender, e) =>
            {
                GraphicsPath path = new GraphicsPath();
                Rectangle bounds = new Rectangle(0, 0, control.Width, control.Height);
                int diameter = radius * 2;

                path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
                path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
                path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
                path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
                path.CloseFigure();

                if (control is Button btn)
                {
                    control.Region = new Region(path);
                    using (SolidBrush brush = new SolidBrush(btn.BackColor))
                        e.Graphics.FillPath(brush, path);

                    TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font,
                        btn.ClientRectangle, btn.ForeColor,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
            };
        }

        private void LoadUserBookings()
        {
            cardsPanel.Controls.Clear();
            statusLabel.Text = "Loading bookings...";
            noBookingsPanel.Visible = false;
            selectedCard = null;

            try
            {
                var fsharpTicketsList = TicketService.getUserTickets(userId);
                var userTickets = fsharpTicketsList.Select(item => (dynamic)item).ToList();

                if (userTickets.Any())
                {
                    foreach (var ticket in userTickets.OrderByDescending(t => ((DateTime)t.StartTime)))
                    {
                        Panel card = CreateBookingCard(ticket);
                        cardsPanel.Controls.Add(card);
                    }

                    statusLabel.Text = $"📊 Found {userTickets.Count} booking{(userTickets.Count != 1 ? "s" : "")}";
                    statusLabel.ForeColor = Color.FromArgb(26, 35, 126);
                }
                else
                {
                    noBookingsPanel.Visible = true;
                    statusLabel.Text = "📭 No bookings found";
                    statusLabel.ForeColor = Color.FromArgb(108, 117, 125);
                }
            }
            catch (Exception ex)
            {
                statusLabel.Text = "❌ Error loading bookings";
                statusLabel.ForeColor = Color.FromArgb(231, 76, 60);
                MessageBox.Show($"Error loading bookings:\n{ex.Message}",
                    "Data Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateBookingCard(dynamic ticket)
        {
            Panel card = new Panel
            {
                Width = 480,
                Height = 280,
                Margin = new Padding(15),
                BackColor = Color.White,
                Tag = ticket.TicketID,
                Cursor = Cursors.Hand
            };

            card.Paint += (sender, e) =>
            {
                // Shadow
                using (var shadowPath = GetRoundedRect(new Rectangle(3, 3, card.Width - 6, card.Height - 6), 12))
                using (var shadowBrush = new SolidBrush(Color.FromArgb(10, 0, 0, 0)))
                {
                    e.Graphics.FillPath(shadowBrush, shadowPath);
                }

                // Card background
                using (var cardPath = GetRoundedRect(new Rectangle(0, 0, card.Width - 6, card.Height - 6), 12))
                using (var cardBrush = new SolidBrush(card.BackColor))
                {
                    e.Graphics.FillPath(cardBrush, cardPath);
                    e.Graphics.DrawPath(new Pen(Color.FromArgb(230, 230, 235), 1), cardPath);
                }

                // Blue border if selected
                if (card == selectedCard)
                {
                    using (var borderPath = GetRoundedRect(new Rectangle(0, 0, card.Width - 6, card.Height - 6), 12))
                    using (var borderPen = new Pen(Color.FromArgb(41, 128, 185), 2))
                    {
                        e.Graphics.DrawPath(borderPen, borderPath);
                    }
                }
            };

            Panel headerPanel = new Panel
            {
                Height = 50,
                Width = card.Width,
                Location = new Point(0, 0)
            };

            headerPanel.Paint += (sender, e) =>
            {
                using (var path = GetRoundedRect(new Rectangle(0, 0, headerPanel.Width, headerPanel.Height), 12, true, false))
                using (var brush = new SolidBrush(Color.FromArgb(41, 128, 185)))
                {
                    e.Graphics.FillPath(brush, path);
                }

                TextRenderer.DrawText(e.Graphics, $"🎫 TICKET: {ticket.TicketID}",
                    new Font("Segoe UI Semibold", 13, FontStyle.Bold),
                    new Rectangle(0, 0, headerPanel.Width, headerPanel.Height),
                    Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };
            card.Controls.Add(headerPanel);

            int yPos = headerPanel.Bottom + 15;

            Label lblMovie = new Label
            {
                Text = $"{ticket.MovieTitle}",
                Font = new Font("Segoe UI Semibold", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 33, 33),
                Location = new Point(0, yPos),
                Size = new Size(card.Width, 55),
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(lblMovie);

            yPos += lblMovie.Height + 20;

            Panel detailsPanel = new Panel
            {
                Location = new Point(30, yPos),
                Size = new Size(card.Width - 60, 100),
                BackColor = Color.Transparent
            };

            Label lblHall = new Label
            {
                Text = $"🏢 Hall: {ticket.HallName}",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(97, 97, 97),
                Location = new Point(0, 0),
                AutoSize = true
            };
            detailsPanel.Controls.Add(lblHall);

            Label lblDate = new Label
            {
                Text = $"📅 {((DateTime)ticket.StartTime).ToString("MMM dd, yyyy")}",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(97, 97, 97),
                Location = new Point(200, 0),
                AutoSize = true
            };
            detailsPanel.Controls.Add(lblDate);

            Label lblSeat = new Label
            {
                Text = $"💺 Seat: Row {ticket.Row}, Col {ticket.Column}",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(97, 97, 97),
                Location = new Point(0, 30),
                AutoSize = true
            };
            detailsPanel.Controls.Add(lblSeat);

            Label lblTime = new Label
            {
                Text = $"⏰ {((DateTime)ticket.StartTime).ToString("HH:mm")}",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(97, 97, 97),
                Location = new Point(200, 30),
                AutoSize = true
            };
            detailsPanel.Controls.Add(lblTime);

            try
            {
                DateTime endTime = ((DateTime)ticket.EndTime);
                TimeSpan duration = endTime - ((DateTime)ticket.StartTime);
                string durationText = $"🕐 {duration.Hours}h {duration.Minutes}m";

                Label lblDuration = new Label
                {
                    Text = durationText,
                    Font = new Font("Segoe UI", 11),
                    ForeColor = Color.FromArgb(97, 97, 97),
                    Location = new Point(0, 60),
                    AutoSize = true
                };
                detailsPanel.Controls.Add(lblDuration);
            }
            catch { /* Duration not available */ }

            card.Controls.Add(detailsPanel);

            yPos = detailsPanel.Bottom + 15;





            card.Click += (sender, e) =>
            {
                if (selectedCard != null && selectedCard != card)
                {
                    selectedCard.BackColor = Color.White;
                    selectedCard.Invalidate();
                }

                selectedCard = card;
                card.BackColor = Color.FromArgb(245, 250, 255);
                card.Invalidate();

                btnCancelBooking.Enabled = true;
                btnCancelBooking.BackColor = Color.FromArgb(231, 76, 60);
                btnCancelBooking.Tag = ticket.TicketID;
            };

            card.MouseEnter += (sender, e) =>
            {
                if (card != selectedCard)
                {
                    card.BackColor = Color.FromArgb(250, 250, 252);
                    card.Invalidate();
                }
            };

            card.MouseLeave += (sender, e) =>
            {
                if (card != selectedCard)
                {
                    card.BackColor = Color.White;
                    card.Invalidate();
                }
            };

            return card;
        }

        private GraphicsPath GetRoundedRect(Rectangle bounds, int radius, bool topOnly = false, bool bottomOnly = false)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            Rectangle arc = new Rectangle(bounds.Location, new Size(diameter, diameter));

            if (topOnly || !bottomOnly)
                path.AddArc(arc, 180, 90);
            else
                path.AddLine(bounds.Left, bounds.Top + radius, bounds.Left, bounds.Top);

            arc.X = bounds.Right - diameter;
            if (topOnly || !bottomOnly)
                path.AddArc(arc, 270, 90);
            else
                path.AddLine(bounds.Right - radius, bounds.Top, bounds.Right, bounds.Top);

            arc.Y = bounds.Bottom - diameter;
            if (bottomOnly || !topOnly)
                path.AddArc(arc, 0, 90);
            else
                path.AddLine(bounds.Right, bounds.Bottom - radius, bounds.Right, bounds.Bottom);

            arc.X = bounds.Left;
            if (bottomOnly || !topOnly)
                path.AddArc(arc, 90, 90);
            else
                path.AddLine(bounds.Left + radius, bounds.Bottom, bounds.Left, bounds.Bottom);

            path.CloseFigure();
            return path;
        }

        private void BtnBookMovie_Click(object sender, EventArgs e)
        {
            MovieForm movieForm = new MovieForm(userId, userName);
            movieForm.Show();
            this.Hide();
        }

        private void BtnCancelBooking_Click(object sender, EventArgs e)
        {
            string ticketId = btnCancelBooking.Tag as string;
            if (string.IsNullOrEmpty(ticketId))
            {
                MessageBox.Show("Please select a booking to cancel.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedCard == null) return;

            string movie = "";
            string hall = "";
            string seat = "";
            string date = "";
            string time = "";

            foreach (Control control in selectedCard.Controls)
            {
                if (control is Panel detailsPanel && control.Tag == null)
                {
                    foreach (Control label in detailsPanel.Controls)
                    {
                        if (label is Label lbl)
                        {
                            if (lbl.Text.Contains("Hall:")) hall = lbl.Text.Replace("🏢 Hall: ", "");
                            else if (lbl.Text.Contains("📅")) date = lbl.Text.Replace("📅 ", "");
                            else if (lbl.Text.Contains("Seat:")) seat = lbl.Text.Replace("💺 Seat: ", "");
                            else if (lbl.Text.Contains("⏰")) time = lbl.Text.Replace("⏰ ", "");
                        }
                    }
                }
                else if (control is Label lbl && control.Tag == null && lbl.Text.Length > 0 && !lbl.Text.Contains("🎫"))
                {
                    if (!lbl.Text.Contains("🏢") && !lbl.Text.Contains("📅") &&
                        !lbl.Text.Contains("💺") && !lbl.Text.Contains("⏰") &&
                        !lbl.Text.Contains("🕐"))
                    {
                        movie = lbl.Text;
                    }
                }
            }

            var result = MessageBox.Show(
                $"Are you sure you want to cancel this booking?\n\n" +
                $"🎫 Ticket: {ticketId}\n" +
                $"🎬 Movie: {movie}\n\n" +
                $"🏢 Hall: {hall}\n" +
                $"💺 {seat}\n" +
                $"📅 {date} at ⏰ {time}",
                "Confirm Cancellation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool isCancelled = TicketService.cancelTicket(ticketId, userId);

                    if (isCancelled)
                    {
                        cardsPanel.Controls.Remove(selectedCard);
                        selectedCard = null;
                        btnCancelBooking.Enabled = false;
                        btnCancelBooking.BackColor = Color.FromArgb(180, 180, 180);
                        btnCancelBooking.Tag = null;

                        MessageBox.Show($"✅ Ticket {ticketId} cancelled successfully.",
                            "Cancellation Successful",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Update status
                        if (cardsPanel.Controls.Count == 0)
                        {
                            noBookingsPanel.Visible = true;
                            statusLabel.Text = "📭 No bookings found";
                        }
                        else
                        {
                            statusLabel.Text = $"📊 Found {cardsPanel.Controls.Count} booking{(cardsPanel.Controls.Count != 1 ? "s" : "")}";
                        }
                    }
                    else
                    {
                        MessageBox.Show("❌ Failed to cancel booking. It may have already been cancelled.",
                            "Cancellation Failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"❌ Error during cancellation: {ex.Message}",
                        "Cancellation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
