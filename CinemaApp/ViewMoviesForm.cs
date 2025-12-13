using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.FSharp.Collections;

namespace CinemaApp
{
    public partial class ViewMoviesForm : Form
    {
        private List<Models.Movie.Movie> movies;
        private Dictionary<int, List<Models.Movie.ShowTime>> showTimesByMovie;

        public ViewMoviesForm()
        {
            InitializeComponent();
            LoadMoviesAndShowTimes();
        }

        private void LoadMoviesAndShowTimes()
        {
            try
            {
                var fsharpMovies = Services.MovieService.getAllMoviesService();
                movies = new List<Models.Movie.Movie>();
                foreach (var movie in fsharpMovies)
                {
                    movies.Add(movie);
                }

                showTimesByMovie = new Dictionary<int, List<Models.Movie.ShowTime>>();
                foreach (var movie in movies)
                {
                    var fsharpShows = Services.MovieService.getShowTimesByMovieService(movie.MovieId);
                    var showList = new List<Models.Movie.ShowTime>();
                    foreach (var show in fsharpShows)
                    {
                        showList.Add(show);
                    }
                    showTimesByMovie[movie.MovieId] = showList;
                }

                var displayList = movies.Select(m =>
                {
                    if (showTimesByMovie.TryGetValue(m.MovieId, out var shows))
                    {
                        string showTimesStr = string.Join(", ",
                            shows.Select(s => s.StartTime.ToString("yyyy-MM-dd HH:mm")));
                        return new
                        {
                            m.MovieId,
                            m.Title,
                            m.Duration,
                            ShowTimes = showTimesStr
                        };
                    }
                    return new
                    {
                        m.MovieId,
                        m.Title,
                        m.Duration,
                        ShowTimes = "No showtimes"
                    };
                }).ToList();

                dataGridViewMovies.DataSource = displayList;

                dataGridViewMovies.Columns["MovieId"].HeaderText = "ID";
                dataGridViewMovies.Columns["Title"].HeaderText = "Title";
                dataGridViewMovies.Columns["Duration"].HeaderText = "Duration (min)";
                dataGridViewMovies.Columns["ShowTimes"].HeaderText = "Show Times";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading movies: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}