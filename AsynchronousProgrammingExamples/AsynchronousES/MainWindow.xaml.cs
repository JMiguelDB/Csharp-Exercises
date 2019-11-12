using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace AsynchronousES
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();

            damageButton.Click += async (o, e) =>
            {
                int actualDamage = int.Parse(damageText.Text);
                int damageResult = await Task.Run(() => CalculateDamage(actualDamage));
                damageText.Text = damageResult.ToString();
            };
        }

        private async void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            var stringData = await _httpClient.GetStringAsync("https://www.dotnetfoundation.org");
            Console.WriteLine(stringData);
        }

        private int CalculateDamage(int damage)
        {
            return damage - 10;
        }

        private async void ButtonHtmlMatches_Click(object sender, RoutedEventArgs e)
        {
            // Capture the task handle here so we can await the background task later.
            var getDotNetFoundationHtmlTask = _httpClient.GetStringAsync("https://www.dotnetfoundation.org");

            // Any other work on the UI thread can be done here, such as enabling a Progress Bar.
            // This is important to do here, before the "await" call, so that the user
            // sees the progress bar before execution of this method is yielded.
            progressBarMatches.IsEnabled = true;
            progressBarMatches.Visibility = Visibility.Visible;

            // The await operator suspends SeeTheDotNets_Click, returning control to its caller.
            // This is what allows the app to be responsive and not block the UI thread.
            var html = await getDotNetFoundationHtmlTask;
            int count = Regex.Matches(html, @"\.NET").Count;

            textHtmlMatches.Text = $"Number of .NETs on dotnetfoundation.org: {count}";

            progressBarMatches.IsEnabled = false;
            progressBarMatches.Visibility = Visibility.Collapsed;
        }
    }
}
