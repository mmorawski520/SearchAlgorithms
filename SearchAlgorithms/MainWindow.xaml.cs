using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace SearchAlgorithms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int repeats;
        string textToFind;
        string currentText;

        public MainWindow()
        {
            InitializeComponent();

            currentText = System.IO.File.ReadAllText(
                System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName,
                "staticTextFile.txt")
                );
        }
        private void initInputData()
        {
            textToFind = userWordTextBox.Text.ToString();
            repeats = int.Parse(repeatsTextBox.Text.ToString());
        }
        private void btnBruteForce_Click(object sender, RoutedEventArgs e)
        {
            initInputData();

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (int i = 0; i < repeats; i++)
            {
                Search.bruteForce(textToFind, currentText);
            }

            stopwatch.Stop();
            resultLabel.Content = stopwatch.ElapsedMilliseconds.ToString();
        }

        private void btnKmp_Click(object sender, RoutedEventArgs e)
        {
            initInputData();

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (int i = 0; i < repeats; i++)
            {
                Search.kmp(textToFind, currentText);
            }

            stopwatch.Stop();
            resultLabel.Content = stopwatch.ElapsedMilliseconds.ToString();

        }

        private void btnBouerMoor_Click(object sender, RoutedEventArgs e)
        {
            initInputData();

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (int i = 0; i < repeats; i++)
            {
                Search.boyerMoore(textToFind, currentText);
            }

            stopwatch.Stop();
            resultLabel.Content = stopwatch.ElapsedMilliseconds.ToString();
        }

        private void btnRabinKarp_Click(object sender, RoutedEventArgs e)
        {
            initInputData();

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (int i = 0; i < repeats; i++)
            {
                Search.rabinKarp(textToFind, currentText, 252);
            }

            stopwatch.Stop();
            resultLabel.Content = stopwatch.ElapsedMilliseconds.ToString();
        }

        private void btnFileUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                currentText = File.ReadAllText(openFileDialog.FileName);
            }
        }
    }
}
