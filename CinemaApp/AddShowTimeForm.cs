using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Services;             // الوصول لدوال F# مثل addShowTimeForExistingMovie
using Microsoft.FSharp.Collections; // التعامل مع FSharpList
using Microsoft.FSharp.Core;        // التعامل مع FSharpResult
using Models;                 // Movie و Hall من F#

namespace CinemaApp
{
    public partial class AddShowTimeForm : Form
    {
        // 💡 التعديل: استخدام MovieDisplay لتجنب التضارب مع Models.Movie
        private List<MovieDisplay> movies; // MovieDisplay list
        private Dictionary<string, int> hallsMap;

        public AddShowTimeForm()
        {
            InitializeComponent();

            LoadMovies();
            LoadHalls();
        }

        private void LoadMovies()
        {
            // جلب الأفلام من F# وتحويلها إلى C# list
            var fsharpMovies = MovieService.getAllMoviesService();

            // 💡 التعديل: التحويل إلى MovieDisplay
            movies = fsharpMovies
                .Select(m => new MovieDisplay { MovieId = m.MovieId, Title = m.Title, Duration = m.Duration })
                .ToList();

            cmbMovies.Items.Clear();
            foreach (var m in movies)
                cmbMovies.Items.Add($"{m.MovieId}: {m.Title}");

            if (cmbMovies.Items.Count > 0)
                cmbMovies.SelectedIndex = 0;
        }

        private void LoadHalls()
        {
            var fsharpHalls = HallService.getAllHallsService(); // ✅ استخدام HallService
            hallsMap = fsharpHalls.ToDictionary(h => h.Name, h => h.HallId);

            cmbHalls.Items.Clear();
            foreach (var h in hallsMap.Keys)
                cmbHalls.Items.Add(h);

            if (cmbHalls.Items.Count > 0)
                cmbHalls.SelectedIndex = 0;
        }

        private void btnSaveShowTime_Click(object sender, EventArgs e)
        {
            if (cmbMovies.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a movie.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbHalls.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a hall.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // MovieId
            string selectedMovie = cmbMovies.SelectedItem.ToString();
            int movieId = int.Parse(selectedMovie.Split(':')[0].Trim());

            // HallId
            string hallName = cmbHalls.SelectedItem.ToString();
            int hallId = hallsMap[hallName];

            DateTime startTime = dtpShowTime.Value;

            // استدعاء دالة F# لإضافة ShowTime
            FSharpResult<string, string> result = MovieService.addShowTimeForExistingMovie(movieId, hallId, startTime);

            if (result.IsOk)
            {
                MessageBox.Show(result.ResultValue, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(result.ErrorValue, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // 💡 الإضافة: كلاس MovieDisplay (بدلاً من Movie) لتجنب التضارب
    public class MovieDisplay
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
    }
}