using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Models;
using Services;
using System.ComponentModel;

namespace CinemaApp
{
    public partial class MovieSeatsForm : Form
    {
        private const int SEAT_WIDTH = 50;
        private const int SEAT_HEIGHT = 50;
        private const int SEAT_MARGIN = 10;
        private const int SEAT_RADIUS = 6;

        private Dictionary<string, RoundedButton> seatButtons = new Dictionary<string, RoundedButton>();
        private List<string> selectedSeats = new List<string>();

        private int currentUserId;
        private string currentUserName; 
        private int currentShowId;
        private dynamic selectedHall;
        private bool[,] bookedSeats;

        public MovieSeatsForm(int userId, string userName, int showId, dynamic hall) : this()
        {
            this.currentUserId = userId;
            this.currentUserName = userName; 
            this.currentShowId = showId;
            this.selectedHall = hall;
        }

        public MovieSeatsForm()
        {
            InitializeComponent();
            this.Load += MovieSeatsForm_Load;
            btnBook.Click += btnBook_Click;
        }

        private void MovieSeatsForm_Load(object sender, EventArgs e)
        {
            GenerateSeats();
            UpdateFooterInfo();
        }

        private void GenerateSeats()
        {
            flpSeatingContainer.Controls.Clear();
            pnlRowLabels.Controls.Clear();
            pnlColumnLabels.Controls.Clear();
            seatButtons.Clear();

            int rows = selectedHall?.Rows ?? 8;
            int cols = selectedHall?.Columns ?? 10;
            int hallId = selectedHall.HallId;

            bookedSeats = SeatService.loadSeatsWithValidationCSharp(hallId, currentShowId, rows, cols);

            for (int r = 0; r < rows; r++)
            {
                char rowChar = (char)('A' + r);
                Label rowLabel = new Label
                {
                    Text = rowChar.ToString(),
                    Size = new Size(40, SEAT_HEIGHT),
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                rowLabel.Location = new Point(0, r * (SEAT_HEIGHT + SEAT_MARGIN));
                pnlRowLabels.Controls.Add(rowLabel);
            }

            for (int c = 0; c < cols; c++)
            {
                Label colLabel = new Label
                {
                    Text = (c + 1).ToString(),
                    Size = new Size(SEAT_WIDTH, 30),
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                colLabel.Location = new Point(c * (SEAT_WIDTH + SEAT_MARGIN), 0);
                pnlColumnLabels.Controls.Add(colLabel);
            }

            for (int r = 0; r < rows; r++)
            {
                Panel rowPanel = new Panel
                {
                    Size = new Size(cols * (SEAT_WIDTH + SEAT_MARGIN), SEAT_HEIGHT),
                    BackColor = Color.Transparent
                };

                char rowChar = (char)('A' + r);

                for (int c = 0; c < cols; c++)
                {
                    string seatId = $"{rowChar}{c + 1}";
                    bool isBooked = bookedSeats[r, c];

                    RoundedButton seatBtn = new RoundedButton
                    {
                        Text = seatId,
                        Size = new Size(SEAT_WIDTH, SEAT_HEIGHT),
                        Location = new Point(c * (SEAT_WIDTH + SEAT_MARGIN), 0),
                        Font = new Font("Segoe UI", 9, FontStyle.Regular),
                        Cursor = Cursors.Hand,
                        Tag = seatId,
                        CornerRadius = SEAT_RADIUS
                    };

                    if (isBooked)
                    {
                        // Not available seat - Gray
                        seatBtn.BackColor = Color.FromArgb(128, 128, 128);
                        seatBtn.ForeColor = Color.White;
                        seatBtn.BorderColor = Color.FromArgb(100, 100, 100);
                        seatBtn.Enabled = false;
                        seatBtn.Text = "✗";
                    }
                    else
                    {
                        // Available seat - Your specified blue
                        seatBtn.BackColor = Color.FromArgb(112, 147, 189);
                        seatBtn.ForeColor = Color.White;
                        seatBtn.BorderColor = Color.FromArgb(90, 125, 167);
                        seatBtn.HoverColor = Color.FromArgb(132, 167, 209);
                        seatBtn.PressColor = Color.FromArgb(92, 127, 169);
                        seatBtn.Click += SeatButton_Click;
                    }

                    seatButtons.Add(seatId, seatBtn);
                    rowPanel.Controls.Add(seatBtn);
                }

                flpSeatingContainer.Controls.Add(rowPanel);
            }

            CenterSeatsInPanel(rows, cols);
        }

        private void CenterSeatsInPanel(int rows, int cols)
        {
            int totalWidth = cols * (SEAT_WIDTH + SEAT_MARGIN) - SEAT_MARGIN;
            int centerX = (flpSeatingContainer.ClientSize.Width - totalWidth) / 2;

            foreach (Control rowPanel in flpSeatingContainer.Controls)
            {
                rowPanel.Left = centerX;
            }
        }

        private void SeatButton_Click(object sender, EventArgs e)
        {
            RoundedButton clickedSeat = (RoundedButton)sender;
            string seatId = clickedSeat.Tag.ToString();

            if (selectedSeats.Contains(seatId))
            {
                selectedSeats.Remove(seatId);
                clickedSeat.BackColor = Color.FromArgb(112, 147, 189); 
                clickedSeat.BorderColor = Color.FromArgb(90, 125, 167); 
            }
            else
            {
                selectedSeats.Add(seatId);
                clickedSeat.BackColor = Color.FromArgb(28, 103, 201); 
                clickedSeat.BorderColor = Color.FromArgb(18, 83, 181); 
            }

            UpdateFooterInfo();
        }

        private void UpdateFooterInfo()
        {
            if (lblSelectedCount != null)
            {
                lblSelectedCount.Text = $"Selected: {selectedSeats.Count} seat" + (selectedSeats.Count != 1 ? "s" : "");
            }

            if (btnBook != null)
            {
                if (selectedSeats.Count > 0)
                {
                    btnBook.Text = $"CONFIRM BOOKING\r\n({selectedSeats.Count} SEATS)";
                    btnBook.Enabled = true;
                }
                else
                {
                    btnBook.Text = "SELECT SEATS TO BOOK";
                    btnBook.Enabled = false;
                }
            }
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            if (selectedSeats.Count == 0) return;

            DialogResult result = MessageBox.Show(
                $"Book {selectedSeats.Count} seat(s)?\n\nSeats: {string.Join(", ", selectedSeats)}",
                "Confirm Booking",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes) return;

            List<string> successfulBookings = new List<string>();
            List<string> failedBookings = new List<string>();
            List<string> ticketIds = new List<string>();

            try
            {
                // Process each selected seat
                foreach (var seatIdText in selectedSeats)
                {
                    try
                    {
                        char rowChar = seatIdText[0];
                        int row = rowChar - 'A';
                        int col = int.Parse(seatIdText.Substring(1)) - 1;

                        var seatInfo = SeatService.getSeatForCSharp(selectedHall.HallId, currentShowId, row, col);
                        int seatId = seatInfo.SeatId;

                        bool booked = SeatService.bookSeatForCSharp(seatId, currentShowId, currentUserId);
                        if (!booked)
                        {
                            failedBookings.Add($"{seatIdText} (Already booked)");
                            continue;
                        }

                        string ticketId = SeatService.createTicketForCSharp(currentUserId, currentShowId, seatId);
                        ticketIds.Add(ticketId);
                        successfulBookings.Add(seatIdText);

                        // Update seat visual
                        if (seatButtons.ContainsKey(seatIdText))
                        {
                            seatButtons[seatIdText].BackColor = Color.FromArgb(128, 128, 128);
                            seatButtons[seatIdText].Enabled = false;
                            seatButtons[seatIdText].Text = "✗";
                        }
                    }
                    catch (Exception ex)
                    {
                        failedBookings.Add($"{seatIdText} (Error: {ex.Message})");
                    }
                }

                string message = $"🎫 BOOKING SUMMARY\n\n";

                if (successfulBookings.Count > 0)
                {
                    message += $"✅ SUCCESSFUL ({successfulBookings.Count}):\n";
                    message += string.Join(", ", successfulBookings) + "\n\n";

                    if (ticketIds.Count > 0)
                    {
                        message += "📋 TICKET INFORMATION:\n";
                        for (int i = 0; i < Math.Min(successfulBookings.Count, ticketIds.Count); i++)
                        {
                            message += $"   • {successfulBookings[i]} → Ticket: {ticketIds[i]}\n";
                        }
                        message += "\n";
                    }
                }

                if (failedBookings.Count > 0)
                {
                    message += $"❌ FAILED ({failedBookings.Count}):\n";
                    message += string.Join("\n", failedBookings);
                }

                if (successfulBookings.Count > 0)
                {
                    message += "\n────────────────────\n";
                    message += "Your booking has been confirmed!\n";
                    message += "Please save your ticket information.";
                }

                MessageBox.Show(message, "Booking Completed",
                    MessageBoxButtons.OK,
                    successfulBookings.Count > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (successfulBookings.Count > 0)
                {
                    selectedSeats.Clear();
                    UpdateFooterInfo();

                    UserHomePage userHomePage = new UserHomePage(currentUserId, currentUserName);
                    userHomePage.Show();
                    this.Close(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Booking error: {ex.Message}\n\nPlease try again.",
                    "Booking Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Custom Rounded Button Class
        public class RoundedButton : Button
        {
            private int cornerRadius = 8;
            private Color borderColor = Color.Gray;
            private Color hoverColor = Color.Empty;
            private Color pressColor = Color.Empty;
            private bool isHovered = false;
            private bool isPressed = false;

            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public int CornerRadius
            {
                get { return cornerRadius; }
                set { cornerRadius = value; Invalidate(); }
            }

            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public Color BorderColor
            {
                get { return borderColor; }
                set { borderColor = value; Invalidate(); }
            }

            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public Color HoverColor
            {
                get { return hoverColor; }
                set { hoverColor = value; }
            }

            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public Color PressColor
            {
                get { return pressColor; }
                set { pressColor = value; }
            }

            public RoundedButton()
            {
                FlatStyle = FlatStyle.Flat;
                FlatAppearance.BorderSize = 0;
                BackColor = Color.FromArgb(112, 147, 189);
                ForeColor = Color.White;
                Font = new Font("Segoe UI", 9, FontStyle.Regular);

                // Remove focus rectangle
                SetStyle(ControlStyles.Selectable, false);

                // Mouse events for hover effect
                MouseEnter += (s, e) => { isHovered = true; Invalidate(); };
                MouseLeave += (s, e) => { isHovered = false; Invalidate(); };
                MouseDown += (s, e) => { isPressed = true; Invalidate(); };
                MouseUp += (s, e) => { isPressed = false; Invalidate(); };
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

                Color buttonColor = BackColor;
                if (isPressed && pressColor != Color.Empty)
                    buttonColor = pressColor;
                else if (isHovered && hoverColor != Color.Empty)
                    buttonColor = hoverColor;

                using (Brush brush = new SolidBrush(buttonColor))
                {
                    g.FillRoundedRectangle(brush, rect, cornerRadius);
                }

                using (Pen pen = new Pen(borderColor, 1))
                {
                    g.DrawRoundedRectangle(pen, rect, cornerRadius);
                }

                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    using (Brush textBrush = new SolidBrush(ForeColor))
                    {
                        g.DrawString(Text, Font, textBrush, rect, sf);
                    }
                }
            }
        }
    }

    public static class GraphicsExtensions
    {
        public static void FillRoundedRectangle(this Graphics g, Brush brush, Rectangle rect, int radius)
        {
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, radius))
            {
                g.FillPath(brush, path);
            }
        }

        public static void DrawRoundedRectangle(this Graphics g, Pen pen, Rectangle rect, int radius)
        {
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, radius))
            {
                g.DrawPath(pen, path);
            }
        }

        private static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            if (radius <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            int d = radius * 2;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}
