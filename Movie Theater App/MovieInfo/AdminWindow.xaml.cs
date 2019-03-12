using System;
using System.Collections.Generic;
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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window {
        public AdminWindow() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            AddDirectorWindow adw = new AddDirectorWindow();
            adw.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            AddMovieWindow amw = new AddMovieWindow();
            amw.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) {
            AddTheaterWindow atw = new AddTheaterWindow();
            atw.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) {
            AddDirectorToMoviesWindow adtmw = new AddDirectorToMoviesWindow();
            adtmw.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e) {
            AddMovieToTheatersWindow amttw = new AddMovieToTheatersWindow();
            amttw.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e) {
            AddShowtimesWindow asw = new AddShowtimesWindow();
            asw.Show();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e) {
            AddScreenToTheaterWindow asttw = new AddScreenToTheaterWindow();
            asttw.Show();
        }
    }
}
