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
        int repeats = 1;
        string textToFind = "f";
        string currentText;
        bool haveFound = false;

        public MainWindow()
        {
            InitializeComponent();

            currentText = System.IO.File.ReadAllText(
                System.IO.Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName,
                "staticTextFile.txt")
                );
        }
        private void initInputData()
        {
            textToFind = userWordTextBox.Text.ToString();
            repeats = int.Parse(repeatsTextBox.Text.ToString());
        }
        private void showResult(Stopwatch stopwatch)
        {
            if (haveFound)
                resultLabel.Content = "searching phrase " + stopwatch.ElapsedMilliseconds.ToString() + "s word '" + textToFind + "' had been found";
            else
                resultLabel.Content = "searching phrase " + stopwatch.ElapsedMilliseconds.ToString() + "s word '" + textToFind + "' not found";
        }
        private void btnBruteForce_Click(object sender, RoutedEventArgs e)
        {
            initInputData();

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (int i = 0; i < repeats; i++)
            {
                haveFound = Search.bruteForce(textToFind, currentText);
            }

            stopwatch.Stop();
            showResult(stopwatch);
        }

        private void btnKmp_Click(object sender, RoutedEventArgs e)
        {
            initInputData();

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (int i = 0; i < repeats; i++)
            {
                haveFound = Search.kmp(textToFind, currentText);
            }

            stopwatch.Stop();
            showResult(stopwatch);

        }

        private void btnBouerMoor_Click(object sender, RoutedEventArgs e)
        {
            initInputData();

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (int i = 0; i < repeats; i++)
            {
                haveFound = Search.boyerMoore(textToFind, currentText);
            }

            stopwatch.Stop();
            showResult(stopwatch);
        }

        private void btnRabinKarp_Click(object sender, RoutedEventArgs e)
        {
            initInputData();

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (int i = 0; i < repeats; i++)
            {
                haveFound = Search.rabinKarp(textToFind, currentText, 252);
            }

            stopwatch.Stop();
            showResult(stopwatch);
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
