using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Services;

namespace CinemaApp
{
    public class ShowTimeView
    {
        public int ShowId { get; set; }
        public int HallId { get; set; }
        public string HallName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
    }

    public partial class MovieForm : Form
    {
        private int userId;
        private string userName;
        private List<Movie> movies;
        private List<dynamic> halls;
        private Dictionary<int, List<ShowTimeView>> movieShowTimes;

        private static readonly Color PRIMARY_COLOR = Color.FromArgb(112, 147, 189); // Dark Blue
        private static readonly Color SECONDARY_COLOR = Color.FromArgb(112, 147, 189); // Blue
        private static readonly Color ACCENT_COLOR = Color.FromArgb(112, 147, 189);
        private static readonly Color CARD_BACKGROUND = Color.FromArgb(250, 250, 255);
        private static readonly Color CARD_SHADOW = Color.FromArgb(220, 220, 230);
        private static readonly Color TEXT_PRIMARY = Color.FromArgb(33, 33, 33);
        private static readonly Color TEXT_SECONDARY = Color.FromArgb(33, 33, 33);

        private Font titleFont = new Font("Segoe UI Semibold", 14, FontStyle.Bold);
        private Font movieTitleFont = new Font("Segoe UI Semibold", 12, FontStyle.Bold);
        private Font regularFont = new Font("Segoe UI", 10);
        private Font smallFont = new Font("Segoe UI", 9);

        public MovieForm(int userId, string userName)
        {
            this.userId = userId;
            this.userName = userName;

            InitializeComponent();
            SetupProfessionalUI();
            LoadMovies();
            DisplayMovies();
        }

        private void SetupProfessionalUI()
        {
            this.Text = $"Cinema - Welcome, {userName}";
            this.BackColor = Color.FromArgb(248, 248, 252);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Padding = new Padding(20);

            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = PRIMARY_COLOR
            };

            Label titleLabel = new Label
            {
                Text = "NOW SHOWING",
                Font = titleFont,
                ForeColor = Color.White,
                AutoSize = true,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(0, 20, 0, 0)
            };

            headerPanel.Controls.Add(titleLabel);
            this.Controls.Add(headerPanel);



            flowPanel.BackColor = Color.Transparent;
            flowPanel.AutoScroll = true;
            flowPanel.Padding = new Padding(10);
            flowPanel.Location = new Point(0, 120);
            flowPanel.Height = this.ClientSize.Height - 120;
            flowPanel.Width = this.ClientSize.Width;
            flowPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            StatusStrip statusStrip = new StatusStrip
            {
                BackColor = PRIMARY_COLOR,
                ForeColor = Color.White
            };

            ToolStripStatusLabel statusLabel = new ToolStripStatusLabel
            {
                Text = "Select a showtime to book seats",
                ForeColor = Color.White
            };

            statusStrip.Items.Add(statusLabel);
            this.Controls.Add(statusStrip);
        }

        private void LoadMovies()
        {
            try
            {
                var fsharpMovies = MovieService.getAllMoviesService();
                movies = fsharpMovies.Select(m => (dynamic)m)
                    .Select(mDyn => new Movie
                    {
                        MovieId = (int)mDyn.MovieId,
                        Title = (string)mDyn.Title,
                        Duration = (int)mDyn.Duration
                    }).ToList();

                var fsharpHalls = HallService.getAllHallsService();
                halls = fsharpHalls.Select(h => (dynamic)h).ToList();

                movieShowTimes = new Dictionary<int, List<ShowTimeView>>();
                foreach (var movie in movies)
                {
                    var fsharpShowTimes = MovieService.getShowTimesByMovieService(movie.MovieId);
                    var showTimes = fsharpShowTimes
                        .Select(st => (dynamic)st)
                        .Where(stDyn => ((DateTime)stDyn.EndTime) > DateTime.Now) 
                        .Select(stDyn =>
                        {
                            int hallId = (int)stDyn.HallId;
                            var hallName = halls.FirstOrDefault(h => h.HallId == hallId)?.Name ?? "Unknown";

                            return new ShowTimeView
                            {
                                ShowId = (int)stDyn.ShowId,
                                HallId = hallId,
                                HallName = hallName,
                                StartTime = (DateTime)stDyn.StartTime,
                                EndTime = (DateTime)stDyn.EndTime
                            };
                        }).ToList();

                    if (showTimes.Any())
                        movieShowTimes.Add(movie.MovieId, showTimes);
                }

                movies = movies.Where(m => movieShowTimes.ContainsKey(m.MovieId)).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in load movies.\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                movies = new List<Movie>();
                halls = new List<dynamic>();
                movieShowTimes = new Dictionary<int, List<ShowTimeView>>();
            }
        }

        private void DisplayMovies()
        {
            flowPanel.Controls.Clear();

            if (!movies.Any())
            {
                ShowNoMoviesMessage();
                return;
            }

            var sortedMovies = movies.OrderBy(m => m.Title).ToList();

            Panel container = new Panel
            {
                AutoSize = true,
                BackColor = Color.Transparent
            };

            int x = 0, y = 0;
            const int cardWidth = 280;
            const int cardHeight = 380;
            const int spacing = 25;

            foreach (var movie in sortedMovies)
            {
                Panel card = CreateMovieCard(movie, cardWidth, cardHeight);
                card.Location = new Point(x, y);
                container.Controls.Add(card);

                x += cardWidth + spacing;

                if (x + cardWidth > flowPanel.Width - 20)
                {
                    x = 0;
                    y += cardHeight + spacing;
                }
            }

            flowPanel.Controls.Add(container);
        }

        private void ShowNoMoviesMessage()
        {
            Panel messagePanel = new Panel
            {
                Size = new Size(400, 200),
                Location = new Point((flowPanel.Width - 400) / 2, (flowPanel.Height - 200) / 2),
                BackColor = Color.Transparent
            };

            Label messageLabel = new Label
            {
                Text = "No movies available at the moment.\nPlease check back later.",
                Font = titleFont,
                ForeColor = TEXT_SECONDARY,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false,
                Size = new Size(400, 100),
                Location = new Point(0, 50)
            };

            PictureBox icon = new PictureBox
            {
                Image = SystemIcons.Information.ToBitmap(),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(64, 64),
                Location = new Point(168, 0)
            };

            messagePanel.Controls.Add(icon);
            messagePanel.Controls.Add(messageLabel);
            flowPanel.Controls.Add(messagePanel);
        }

        private Panel CreateMovieCard(Movie movie, int width, int height)
        {
            Panel card = new Panel
            {
                Width = width,
                Height = height,
                BackColor = CARD_BACKGROUND,
                Tag = movie.MovieId,
                Cursor = Cursors.Default
            };

            // Add shadow effect
            card.Paint += (sender, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                    Color.Transparent, 0, ButtonBorderStyle.None,
                    Color.Transparent, 0, ButtonBorderStyle.None,
                    Color.Transparent, 0, ButtonBorderStyle.None,
                    Color.Transparent, 0, ButtonBorderStyle.None);

                using (var shadowPath = GetRoundedRect(new Rectangle(2, 2, width - 4, height - 4), 10))
                using (var shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
                {
                    e.Graphics.FillPath(shadowBrush, shadowPath);
                }

                using (var cardPath = GetRoundedRect(new Rectangle(0, 0, width - 4, height - 4), 10))
                using (var cardBrush = new SolidBrush(CARD_BACKGROUND))
                {
                    e.Graphics.FillPath(cardBrush, cardPath);
                    e.Graphics.DrawPath(new Pen(CARD_SHADOW, 1), cardPath);
                }
            };

            Panel titlePanel = new Panel
            {
                Height = 60,
                Width = card.Width,
                Location = new Point(0, 0)
            };

            titlePanel.Paint += (sender, e) =>
            {
                using (var gradientBrush = new LinearGradientBrush(
                    titlePanel.ClientRectangle,
                    SECONDARY_COLOR,
                    Color.FromArgb(41, 98, 255),
                    LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(gradientBrush, titlePanel.ClientRectangle);
                }

                using (var path = GetRoundedRect(new Rectangle(0, 0, titlePanel.Width, titlePanel.Height), 10, true, false))
                {
                    e.Graphics.FillPath(new SolidBrush(SECONDARY_COLOR), path);
                }

                TextRenderer.DrawText(e.Graphics, movie.Title, movieTitleFont,
                    titlePanel.ClientRectangle, Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            };

            card.Controls.Add(titlePanel);

            // Movie info
            int yPos = titlePanel.Bottom + 15;

            // Duration
            Label lblDuration = new Label
            {
                Text = $"⏱ Duration: {movie.Duration} min",
                Font = regularFont,
                ForeColor = TEXT_SECONDARY,
                AutoSize = true,
                Location = new Point(15, yPos)
            };
            card.Controls.Add(lblDuration);

            yPos += lblDuration.Height + 20;

            // Showtime selection dropdown
            Label lblSelectShowtime = new Label
            {
                Text = "SELECT SHOWTIME",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = PRIMARY_COLOR,
                AutoSize = true,
                Location = new Point(15, yPos)
            };
            card.Controls.Add(lblSelectShowtime);

            yPos += lblSelectShowtime.Height + 8;

            ComboBox cbShowtime = new ComboBox
            {
                Width = width - 30,
                Height = 35,
                Location = new Point(15, yPos),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = regularFont,
                Tag = movie.MovieId,
                FlatStyle = FlatStyle.Flat,
                DrawMode = DrawMode.OwnerDrawFixed
            };

            cbShowtime.DrawItem += (sender, e) =>
            {
                e.DrawBackground();

                if (e.Index >= 0)
                {
                    var item = cbShowtime.Items[e.Index] as ComboBoxItem;
                    if (item != null)
                    {
                        string icon = e.Index == 0 ? "" : "🎬";
                        e.Graphics.DrawString(icon, regularFont, Brushes.Gray, e.Bounds.Left + 5, e.Bounds.Top + 3);

                        using (var brush = (e.State & DrawItemState.Selected) == DrawItemState.Selected ?
                            new SolidBrush(Color.White) : new SolidBrush(TEXT_PRIMARY))
                        {
                            e.Graphics.DrawString(item.Display, regularFont, brush,
                                e.Bounds.Left + 25, e.Bounds.Top + 3);
                        }
                    }
                }
                e.DrawFocusRectangle();
            };

            // Add showtime options
            if (movieShowTimes.ContainsKey(movie.MovieId) && movieShowTimes[movie.MovieId].Any())
            {
                // Add placeholder as first item
                cbShowtime.Items.Add(new ComboBoxItem("-- Select Showtime --", 0));

                // Add all showtimes sorted by time
                var sortedShowtimes = movieShowTimes[movie.MovieId]
                    .OrderBy(st => st.StartTime)
                    .ThenBy(st => st.HallName);

                foreach (var showtime in sortedShowtimes)
                {
                    // Format: "11:30 - Hall A"
                    string displayText = $"{showtime.StartTime:HH:mm} - {showtime.HallName}";
                    cbShowtime.Items.Add(new ComboBoxItem(displayText, showtime.ShowId));
                }
                cbShowtime.SelectedIndex = 0;
            }
            else
            {
                cbShowtime.Items.Add(new ComboBoxItem("No showtimes available", 0));
                cbShowtime.Enabled = false;
            }

            cbShowtime.SelectedIndexChanged += (sender, e) =>
            {
                var btnSelect = card.Controls.OfType<Button>().FirstOrDefault();
                if (btnSelect != null)
                {
                    if (cbShowtime.SelectedIndex > 0) 
                    {
                        var selectedItem = cbShowtime.SelectedItem as ComboBoxItem;
                        btnSelect.Tag = selectedItem.Value; // Store showId
                        btnSelect.BackColor = ACCENT_COLOR;
                        btnSelect.Enabled = true;
                        btnSelect.Cursor = Cursors.Hand;
                        btnSelect.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 117, 158);
                        btnSelect.FlatAppearance.MouseDownBackColor = Color.FromArgb(194, 24, 91);
                    }
                    else 
                    {
                        btnSelect.Tag = 0;
                        btnSelect.BackColor = Color.FromArgb(200, 200, 200);
                        btnSelect.Enabled = false;
                        btnSelect.Cursor = Cursors.Default;
                    }
                }
            };

            card.Controls.Add(cbShowtime);
            yPos += cbShowtime.Height + 25;

            Button btnSelect = new Button
            {
                Text = " BOOK SEATS",
                Size = new Size(width - 30, 45),
                Location = new Point(15, height - 60),
                BackColor = Color.FromArgb(200, 200, 200), 
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Tag = 0, 
                Cursor = Cursors.Default,
                Enabled = false
            };

            btnSelect.FlatAppearance.BorderSize = 0;

            SetRoundedButton(btnSelect, 8);
            btnSelect.Click += (sender, e) =>
            {
                BtnSelect_Click(sender, e, movie.MovieId, cbShowtime);
            };
            card.Controls.Add(btnSelect);

            card.MouseEnter += (sender, e) =>
            {
                card.Location = new Point(card.Location.X - 1, card.Location.Y - 1);
                card.Width += 2;
                card.Height += 2;
                card.BringToFront();
            };

            card.MouseLeave += (sender, e) =>
            {
                card.Location = new Point(card.Location.X + 1, card.Location.Y + 1);
                card.Width -= 2;
                card.Height -= 2;
            };

            return card;
        }

        private void SetRoundedButton(Button button, int radius)
        {
            button.FlatAppearance.BorderSize = 0;
            button.Paint += (sender, e) =>
            {
                using (GraphicsPath path = GetRoundedRect(button.ClientRectangle, radius))
                {
                    button.Region = new Region(path);

                    using (SolidBrush brush = new SolidBrush(button.BackColor))
                        e.Graphics.FillPath(brush, path);

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
        }

        private GraphicsPath GetRoundedRect(Rectangle bounds, int radius, bool topOnly = false, bool bottomOnly = false)
        {
            GraphicsPath path = new GraphicsPath();

            if (radius <= 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

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

        private void BtnSelect_Click(object sender, EventArgs e, int movieId, ComboBox cbShowtime)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            int selectedShowId = (int)btn.Tag;

            if (selectedShowId == 0 || cbShowtime.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a showtime first.",
                    "Showtime Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var selectedShow = movieShowTimes.Values
                .SelectMany(list => list)
                .FirstOrDefault(st => st.ShowId == selectedShowId);

            if (selectedShow == null)
            {
                MessageBox.Show("The selected showtime is no longer available.",
                    "Showtime Unavailable",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            dynamic selectedHall = halls.FirstOrDefault(h => h.HallId == selectedShow.HallId);

            using (var loadingForm = new Form()
            {
                Size = new Size(200, 100),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.None,
                BackColor = SECONDARY_COLOR,
                Opacity = 0.9
            })
            {
                Label loadingLabel = new Label()
                {
                    Text = "Loading seats...",
                    ForeColor = Color.White,
                    Font = titleFont,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                loadingForm.Controls.Add(loadingLabel);

                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() { Interval = 100 };
                timer.Tick += (s, ev) =>
                {
                    loadingForm.Close();
                    timer.Stop();

                    MovieSeatsForm seatForm = new MovieSeatsForm(userId, userName,selectedShowId, selectedHall);
                    seatForm.Show();
                    this.Hide();
                };

                timer.Start();
                loadingForm.ShowDialog(this);
            }
        }

        private class ComboBoxItem
        {
            public string Display { get; set; }
            public int Value { get; set; }
            public ComboBoxItem(string display, int value)
            {
                Display = display;
                Value = value;
            }
            public override string ToString() => Display;
        }
    }
}
