using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MovieInfo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Regex rx = new Regex(@"(?<== )(.*)(?= })");
        public MainWindow()
        {
            InitializeComponent();

            using (SqlConnection conn = new SqlConnection("Server=MSSQL.CS.KSU.EDU;Database=cis560_team24;TrustServerCertificate=true;Integrated Security=SSPI;"))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("MovieInfo.RetrieveTheatres", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var row = new { Theater = rdr[0] };
                        theaters.Items.Add(row);
                    }
                }
            }

            for (var dt = DateTime.Now; dt <= DateTime.Now.AddDays(14); dt = dt.AddDays(1))
            {
                var row = new { Day = dt.ToString("MM/dd/yyyy") };
                days.Items.Add(row);
            }

            using (SqlConnection conn = new SqlConnection("Server=MSSQL.CS.KSU.EDU;Database=cis560_team24;TrustServerCertificate=true;Integrated Security=SSPI;"))
            {
                conn.Open();
                
                SqlCommand cmd = new SqlCommand("MovieInfo.RetrieveMovies", conn);
    
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var row = new { Movie = rdr[0] };
                        movies.Items.Add(row);
                    }
                }
            }

            var dn = DateTime.Now;
            var ds = TimeSpan.FromMinutes(15);
            for (var d = new DateTime((dn.Ticks + ds.Ticks - 1) / ds.Ticks * ds.Ticks, dn.Kind); d <= DateTime.Now.AddDays(1); d = d.AddMinutes(15))
            {
                var row = new { Showtime = d.ToString("hh:mm tt") };
                showtimes.Items.Add(row);
            }
        }

        private void theaters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            refresh();
        }

        private void days_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            refresh();
        }

        private void movies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            refresh();
        }

        private void showtimes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            refresh();
        }

        private void refresh()
        {
            refreshMovies();
            refreshTheaters();
            refreshDays();
            refreshShowtimes();
        }

        private void refreshMovies()
        {
            if(movies.SelectedItem != null)
            {
                return;
            }

            movies.Items.Clear();

            string theater = theaters.SelectedItem != null ? rx.Match(theaters.SelectedItem.ToString()).Value : null;
            string day = days.SelectedItem != null ? rx.Match(days.SelectedItem.ToString()).Value : null;
            string showtime = showtimes.SelectedItem != null ? rx.Match(showtimes.SelectedItem.ToString()).Value : null;

            using (SqlConnection conn = new SqlConnection("Server=MSSQL.CS.KSU.EDU;Database=cis560_team24;TrustServerCertificate=true;Integrated Security=SSPI;"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("MovieInfo.RetrieveMovies");
                if (theater != null || day != null || showtime != null)
                {
                    if (theater != null && day == null && showtime == null)
                    {
                        cmd = new SqlCommand("MovieInfo.RetrieveMoviesByTheatre", conn);
                        cmd.Parameters.Add(new SqlParameter("@TheatreName", theater));
                    }

                    if ( theater == null && day != null && showtime == null)
                    {
                        cmd = new SqlCommand("MovieInfo.RetrieveMoviesByDate");
                        cmd.Parameters.Add(new SqlParameter("@Date", Convert.ToDateTime(day)));
                    }

                    if (theater == null && day == null && showtime != null)
                    {
                        cmd = new SqlCommand("MovieInfo.RetrieveMoviesByShowtime");
                        cmd.Parameters.Add(new SqlParameter("@Showtime", Convert.ToDateTime(showtime)));
                    }

                    if (theater != null && day != null && showtime == null)
                    {
                        cmd = new SqlCommand("MovieInfo.RetrieveMoviesByTheaterAndDate");
                        cmd.Parameters.Add(new SqlParameter("@TheaterName", theater));
                        cmd.Parameters.Add(new SqlParameter("@Date", Convert.ToDateTime(day)));
                    }

                    if(theater != null && day == null && showtime != null)
                    {
                        cmd = new SqlCommand("MovieInfo.RetrieveMoviesByTheaterAndShowTime");
                        cmd.Parameters.Add(new SqlParameter("@TheaterName", theater));
                        cmd.Parameters.Add(new SqlParameter("@Showtime", Convert.ToDateTime(showtime)));
                    }

                    if(theater == null && day != null && showtime != null)
                    {
                        cmd = new SqlCommand("MovieInfo.RetrieveMoviesByDateAndShowTime");
                        cmd.Parameters.Add(new SqlParameter("@Date", Convert.ToDateTime(day)));
                        cmd.Parameters.Add(new SqlParameter("@Showtime", Convert.ToDateTime(showtime)));
                    }

                    if(theater != null && day != null && showtime != null)
                    {
                        cmd = new SqlCommand("MovieInfo.RetrieveMoviesByTheaterAndDateAndShowTime");
                        cmd.Parameters.Add(new SqlParameter("@TheaterName", theater));
                        cmd.Parameters.Add(new SqlParameter("@Date", Convert.ToDateTime(day)));
                        cmd.Parameters.Add(new SqlParameter("@StartTime", Convert.ToDateTime(showtime)));
                    }
                }
                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var row = new { Movie = rdr[0] };
                        movies.Items.Add(row);
                    }
                }
            }
        }

        private void refreshTheaters()
        {
            if (theaters.SelectedItem != null)
            {
                return;
            }

            theaters.Items.Clear();

            string movie = movies.SelectedItem != null ? rx.Match(movies.SelectedItem.ToString()).Value : null;
            string day = days.SelectedItem != null ? rx.Match(days.SelectedItem.ToString()).Value : null;
            string showtime = showtimes.SelectedItem != null ? rx.Match(showtimes.SelectedItem.ToString()).Value : null;

            using (SqlConnection conn = new SqlConnection("Server=MSSQL.CS.KSU.EDU;Database=cis560_team24;TrustServerCertificate=true;Integrated Security=SSPI;"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("MovieInfo.RetrieveTheatres");
                if (movie != null || day != null || showtime != null)
                {
                    if (movie != null && day == null && showtime == null)
                    {
                        cmd = new SqlCommand("MovieInfo.RetrieveTheatreByMovie", conn);
                        cmd.Parameters.Add(new SqlParameter("@MovieName", movie));
                    }

                    if(movie == null && day != null && showtime == null)
                    {
                        cmd = new SqlCommand("MovieInfo.RetrieveTheatreByDate", conn);
                        cmd.Parameters.Add(new SqlParameter("@Date", Convert.ToDateTime(day)));
                    }

                    if(movie == null && day == null && showtime != null)
                    {
                        cmd = new SqlCommand("MovieInfo.RetrieveTheatreByShowtime", conn);
                        cmd.Parameters.Add(new SqlParameter("@Showtime", Convert.ToDateTime(showtime)));
                    }

                    if(movie != null && day != null && showtime == null)
                    {
                        cmd = new SqlCommand("MovieInfo.RetrieveTheatreByDateAndMovie", conn);
                        cmd.Parameters.Add(new SqlParameter("@Date", Convert.ToDateTime(day)));
                        cmd.Parameters.Add(new SqlParameter("@MovieName", movie));
                    }

                    if(movie != null && day == null && showtime != null)
                    {
                        cmd = new SqlCommand("MovieInfo.RetrieveTheatreByMovieAndShowtime", conn);
                        cmd.Parameters.Add(new SqlParameter("@Showtime", Convert.ToDateTime(showtime)));
                        cmd.Parameters.Add(new SqlParameter("@MovieName", movie));
                    }

                    if(movie == null && day != null && showtime != null)
                    {
                        cmd = new SqlCommand("MovieInfo.RetrieveTheatreByDateAndShowtime", conn);
                        cmd.Parameters.Add(new SqlParameter("@Showtime", Convert.ToDateTime(showtime)));
                        cmd.Parameters.Add(new SqlParameter("@Date", Convert.ToDateTime(day)));
                    }

                    if(movie != null && day != null && showtime != null)
                    {
                        cmd = new SqlCommand("MovieInfo.RetrieveTheatreByDateAndMovieAndShowtime", conn);
                        cmd.Parameters.Add(new SqlParameter("@Showtime", Convert.ToDateTime(showtime)));
                        cmd.Parameters.Add(new SqlParameter("@Date", Convert.ToDateTime(day)));
                        cmd.Parameters.Add(new SqlParameter("@MovieName", movie));
                    }
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var row = new { Theater = rdr[0] };
                        theaters.Items.Add(row);
                    }
                }
            }
        }

        private void refreshDays()
        {
            if (days.SelectedItem != null)
            {
                return;
            }

            days.Items.Clear();

            string movie = movies.SelectedItem != null ? rx.Match(movies.SelectedItem.ToString()).Value : null;
            string theater = theaters.SelectedItem != null ? rx.Match(theaters.SelectedItem.ToString()).Value : null;
            string showtime = showtimes.SelectedItem != null ? rx.Match(showtimes.SelectedItem.ToString()).Value : null;

            if(movie == null && theater == null && showtime == null)
            {
                for (var dt = DateTime.Now; dt <= DateTime.Now.AddDays(14); dt = dt.AddDays(1))
                {
                    var row = new { Day = dt.ToString("MM/dd/yyyy") };
                    days.Items.Add(row);
                }

                return;
            }

            using (SqlConnection conn = new SqlConnection("Server=MSSQL.CS.KSU.EDU;Database=cis560_team24;TrustServerCertificate=true;Integrated Security=SSPI;"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("MovieInfo.RetrieveTheatres");
                if(movie != null && theater == null && showtime == null)
                {
                    cmd = new SqlCommand("MovieInfo.RetrieveDateByMovie", conn);
                    cmd.Parameters.Add(new SqlParameter("@MovieName", movie));
                }

                if(movie == null && theater != null && showtime == null)
                {
                    cmd = new SqlCommand("MovieInfo.RetrieveDateByTheatre", conn);
                    cmd.Parameters.Add(new SqlParameter("@TheatreName", theater));
                }

                if(movie == null && theater == null && showtime != null)
                {
                    cmd = new SqlCommand("MovieInfo.RetrieveDateByShowtime", conn);
                    cmd.Parameters.Add(new SqlParameter("@Showtime", Convert.ToDateTime(showtime)));
                }

                if(movie != null && theater != null && showtime == null)
                {
                    cmd = new SqlCommand("MovieInfo.RetrieveDateByTheatreAndMovie", conn);
                    cmd.Parameters.Add(new SqlParameter("@MovieName", movie));
                    cmd.Parameters.Add(new SqlParameter("@TheatreName", theater));
                }

                if(movie != null && theater == null && showtime != null)
                {
                    cmd = new SqlCommand("MovieInfo.RetrieveDateByShowtimeAndMovie", conn);
                    cmd.Parameters.Add(new SqlParameter("@MovieName", movie));
                    cmd.Parameters.Add(new SqlParameter("@Showtime", Convert.ToDateTime(showtime)));
                }

                if(movie == null && theater != null && showtime != null)
                {
                    cmd = new SqlCommand("MovieInfo.RetrieveDateByTheatreAndShowtime", conn);
                    cmd.Parameters.Add(new SqlParameter("@TheatreName", theater));
                    cmd.Parameters.Add(new SqlParameter("@Showtime", Convert.ToDateTime(showtime)));
                }

                if(movie != null && theater != null && showtime != null)
                {
                    cmd = new SqlCommand("MovieInfo.RetrieveDateByTheatreMovieShowtime", conn);
                    cmd.Parameters.Add(new SqlParameter("@TheatreName", theater));
                    cmd.Parameters.Add(new SqlParameter("@Showtime", Convert.ToDateTime(showtime)));
                    cmd.Parameters.Add(new SqlParameter("@MovieName", movie));
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var row = new { Day = Convert.ToDateTime(rdr[0]).ToString("MM/dd/yyyy") };
                        days.Items.Add(row);
                    }
                }
            }
        }

        private void refreshShowtimes()
        {
            if (showtimes.SelectedItem != null)
            {
                return;
            }

            showtimes.Items.Clear();

            string movie = movies.SelectedItem != null ? rx.Match(movies.SelectedItem.ToString()).Value : null;
            string theater = theaters.SelectedItem != null ? rx.Match(theaters.SelectedItem.ToString()).Value : null;
            string day = days.SelectedItem != null ? rx.Match(days.SelectedItem.ToString()).Value : null;

            if (movie == null && theater == null && day == null)
            {
                var dn = DateTime.Now;
                var ds = TimeSpan.FromMinutes(15);
                for (var d = new DateTime((dn.Ticks + ds.Ticks - 1) / ds.Ticks * ds.Ticks, dn.Kind); d <= DateTime.Now.AddDays(1); d = d.AddMinutes(15))
                {
                    var row = new { Showtime = d.ToString("hh:mm tt") };
                    showtimes.Items.Add(row);
                }

                return;
            }

            using (SqlConnection conn = new SqlConnection("Server=MSSQL.CS.KSU.EDU;Database=cis560_team24;TrustServerCertificate=true;Integrated Security=SSPI;"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("MovieInfo.RetrieveTheatres");
                if(movie != null && theater == null && day == null)
                {
                    cmd = new SqlCommand("MovieInfo.RetrieveShowtimesByMovie", conn);
                    cmd.Parameters.Add(new SqlParameter("@MovieName", movie));
                }

                if(movie == null && theater != null && day == null)
                {
                    cmd = new SqlCommand("MovieInfo.RetrieveShowtimesByTheater", conn);
                    cmd.Parameters.Add(new SqlParameter("@TheatreName", theater));
                }

                if(movie == null && theater == null && day != null)
                {
                    cmd = new SqlCommand("MovieInfo.RetrieveShowtimesByDate", conn);
                    cmd.Parameters.Add(new SqlParameter("@Date", Convert.ToDateTime(day)));
                }

                if(movie != null && theater != null && day == null)
                {
                    cmd = new SqlCommand("MovieInfo.RetrieveShowtimesByTheaterAndMovie", conn);
                    cmd.Parameters.Add(new SqlParameter("@TheatreName", theater));
                    cmd.Parameters.Add(new SqlParameter("@MovieName", movie));
                }

                if(movie != null && theater == null && day != null)
                {
                    cmd = new SqlCommand("MovieInfo.RetrieveShowtimesByDateAndMovie", conn);
                    cmd.Parameters.Add(new SqlParameter("@Date", Convert.ToDateTime(day)));
                    cmd.Parameters.Add(new SqlParameter("@MovieName", movie));
                }

                if(movie == null && theater != null && day != null)
                {
                    cmd = new SqlCommand("MovieInfo.RetrieveShowtimesByTheaterAndDate", conn);
                    cmd.Parameters.Add(new SqlParameter("@Date", Convert.ToDateTime(day)));
                    cmd.Parameters.Add(new SqlParameter("@TheatreName", theater));
                }

                if(movie != null && theater != null && day != null)
                {
                    cmd = new SqlCommand("MovieInfo.RetrieveShowtimesByTheaterAndDateAndMovie", conn);
                    cmd.Parameters.Add(new SqlParameter("@Date", Convert.ToDateTime(day)));
                    cmd.Parameters.Add(new SqlParameter("@TheatreName", theater));
                    cmd.Parameters.Add(new SqlParameter("@MovieName", movie));
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var row = new { Showtime = DateTime.Parse(rdr[0].ToString()).ToString("hh:mm tt") };
                        showtimes.Items.Add(row);
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AdminWindow aw = new AdminWindow();
            aw.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            movies.Items.Clear();
            theaters.Items.Clear();
            days.Items.Clear();
            showtimes.Items.Clear();
            refresh();
        }
    }
}
