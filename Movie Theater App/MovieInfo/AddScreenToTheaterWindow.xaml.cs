using System;
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
    /// Interaction logic for AddScreenToTheaterWindow.xaml
    /// </summary>
    public partial class AddScreenToTheaterWindow : Window {
        public AddScreenToTheaterWindow() {
            InitializeComponent();

            using (SqlConnection conn = new SqlConnection("Server=MSSQL.CS.KSU.EDU;Database=cis560_team24;TrustServerCertificate=true;Integrated Security=SSPI;"))
            {
                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("MovieInfo.RetrieveTheatres", conn);

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
                        theaters.Items.Add(rdr[0]);
                    }
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {

        }
    }
}
