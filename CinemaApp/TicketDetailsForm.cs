using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace CinemaApp
{
    // Class مؤقتة لتمثيل بيانات التذكرة المعروضة
    public class TicketDisplayData
    {
        public string TicketID { get; set; }
        public string MovieName { get; set; }
        public string HallName { get; set; }
        public string Seats { get; set; }
        public DateTime ShowTime { get; set; }
    }

    public partial class TicketDetailsForm : Form
    {
        private TicketDisplayData ticketData;

        public TicketDetailsForm(TicketDisplayData data)
        {
            this.ticketData = data;
            InitializeComponent();

            this.Text = "Ticket Confirmation";

            // تحديث العناصر بناءً على بيانات التذكرة
            lblTitle.Text = "✅ Booking Confirmed!";
            lblMovieName.Text = ticketData.MovieName;
            lblHallValue.Text = ticketData.HallName;
            lblSeatsValue.Text = ticketData.Seats;
            lblTimeValue.Text = ticketData.ShowTime.ToString("yyyy-MM-dd HH:mm");
            lblTicketIDValue.Text = ticketData.TicketID;

            // تطبيق الحواف الدائرية على زر "Close"
            ApplyRadiusToControl(btnClose, 12);

            // إغلاق الواجهة عند الضغط على زر الإغلاق
            btnClose.Click += (sender, e) => this.Close();
        }

        // دالة مساعدة لتطبيق الحواف الدائرية
        private void ApplyRadiusToControl(Control control, int radius)
        {
            control.Paint += (sender, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                Rectangle bounds = new Rectangle(0, 0, control.Width, control.Height);
                GraphicsPath path = GetRoundedRect(bounds, radius);
                control.Region = new Region(path);

                if (control is Button button)
                {
                    using (SolidBrush brush = new SolidBrush(button.BackColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                    TextRenderer.DrawText(
                        e.Graphics,
                        button.Text,
                        button.Font,
                        button.ClientRectangle,
                        button.ForeColor,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                    );
                }
            };
            control.Invalidate();
        }

        private GraphicsPath GetRoundedRect(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            Rectangle arc = new Rectangle(bounds.X, bounds.Y, diameter, diameter);

            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}
