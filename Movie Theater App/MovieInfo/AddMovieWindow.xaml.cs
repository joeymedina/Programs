﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MovieInfo {
    /// <summary>
    /// Interaction logic for AddMovieWindow.xaml
    /// </summary>
    public partial class AddMovieWindow : Window {
        public AddMovieWindow() {
            InitializeComponent();

            using (SqlConnection conn = new SqlConnection("Server=MSSQL.CS.KSU.EDU;Database=cis560_team24;TrustServerCertificate=true;Integrated Security=SSPI;"))
            {
                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("MovieInfo.RetrieveDirector", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                //cmd.Parameters.Add(new SqlParameter("@CustomerID", custId));

                // execute the command
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    // iterate through results, printing each to console
                    while (rdr.Read())
                    {
                        directors.Items.Add(rdr[1]);
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection("Server=MSSQL.CS.KSU.EDU;Database=cis560_team24;TrustServerCertificate=true;Integrated Security=SSPI;"))
            {
                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("MovieInfo.CreateMovie", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@Name", name.Text));
                cmd.Parameters.Add(new SqlParameter("@Genre", genre.Text));
                cmd.Parameters.Add(new SqlParameter("@Runtime", TimeSpan.FromMinutes(Convert.ToDouble(runtime.Text))));
                cmd.Parameters.Add(new SqlParameter("@Director", directors.SelectedItem));
                // execute the command
                cmd.ExecuteNonQuery();
            }

            this.Close();
        }
    }
}
