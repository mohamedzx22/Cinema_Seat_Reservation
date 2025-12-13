using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinemaApp
{
    public partial class AdminHomeForm : Form
    {
        private List<Movie> moviesList;


        public AdminHomeForm()
        {
            InitializeComponent();
        }



        private void btnAddMovieWithTime_Click(object sender, EventArgs e)
        {
            AddMovieWithShowTimeForm form = new AddMovieWithShowTimeForm();
            form.ShowDialog();
        }

        private void btnViewMovies_Click(object sender, EventArgs e)
        {
            try
            {
                ViewMoviesForm view = new ViewMoviesForm();
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening movies form: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }

    }
}
