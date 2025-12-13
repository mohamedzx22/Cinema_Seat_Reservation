using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Services; 
using Microsoft.FSharp.Core;

namespace CinemaApp
{
    public partial class AddMovieWithShowTimeForm : Form
    {
        private List<string> showTimes = new List<string>();

        public AddMovieWithShowTimeForm()
        {
            InitializeComponent();
        }

        private void btnAddShowTime_Click(object sender, EventArgs e)
        {
            DateTime selectedTime = dtpShowTime.Value;
            showTimes.Add(selectedTime.ToString("yyyy-MM-dd HH:mm"));
            lstShowTimes.DataSource = null;
            lstShowTimes.DataSource = showTimes;
        }


        private void btnSaveMovie_Click(object sender, EventArgs e)
        {
            string title = txtName.Text.Trim();
            if (!int.TryParse(txtDuration.Text.Trim(), out int duration))
            {
                MessageBox.Show("Please enter a valid duration in minutes.", "Invalid Duration");
                return;
            }

            string hallName = txtHallName.Text.Trim();
            if (string.IsNullOrEmpty(hallName))
            {
                MessageBox.Show("Please enter the hall name.", "Error");
                return;
            }

            int hallId = GetHallIdByName(hallName);
            if (hallId == -1)
            {
                MessageBox.Show("Hall not found.", "Error");
                return;
            }

            if (showTimes.Count == 0)
            {
                MessageBox.Show("Please add at least one show time.", "Error");
                return;
            }

            foreach (string t in showTimes)
            {
                DateTime startTime = DateTime.ParseExact(t, "yyyy-MM-dd HH:mm", null);

                FSharpResult<string, string> result = MovieService.addMovieWithShowTime(
                    title,
                    duration,
                    hallId,
                    startTime
                );

                if (result.IsOk)
                {
                    MessageBox.Show(result.ResultValue, "Success");
                }
                else
                {
                    MessageBox.Show(result.ErrorValue, "Error");
                }
            }


            this.Close();
        }

        private int GetHallIdByName(string name)
        {
            var halls = new Dictionary<string, int>()
            {
                { "Hall A", 1 },
                { "Hall B", 2 },
                { "Hall C", 3 }
            };

            return halls.TryGetValue(name, out int id) ? id : -1;
        }
    }
}
