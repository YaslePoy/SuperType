using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SuperType.Pages
{
    /// <summary>
    /// Логика взаимодействия для TestingPage.xaml
    /// </summary>
    public partial class TestingPage : Page
    {
        int letterCount;
        int TypedCount;
        int TrueTypedCount;
        DateTime StartTime;
        bool running;
        void LoadText()
        {
            var text = File.ReadAllText("Textarium\\shop1.txt");
            letterCount = text.Length;
            foreach (var c in text)
            {
                var addTB = new TextBlock() { Text = c.ToString(), FontSize = 19 };
                LettersPanel.Children.Add(addTB);
            }
        }

        public TestingPage()
        {
            InitializeComponent();
            LoadText();
            DispatcherTimer dt = new DispatcherTimer();
            dt.Tick += (o, e) =>
            {
                if (!running)
                    return;
                if(letterCount == TrueTypedCount)
                {
                    running = false;
                    return;
                }
                var testTime = DateTime.Now - StartTime;
                var spm = TrueTypedCount / testTime.TotalMinutes;
                SpeedTB.Text = Math.Round(spm, 1).ToString();
                AccTB.Text = (Math.Round((double)TrueTypedCount / (double)TypedCount * 100, 1)).ToString();

            };
            dt.Interval = TimeSpan.FromMilliseconds(500);
            dt.Start();
        }

        private void ScrollViewer_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (TypedCount == 0)
            {
                StartTime = DateTime.Now;
                running = true;
            }
            TypedCount++;
            Console.WriteLine(e.Text);
            var last = LettersPanel.Children[0] as TextBlock;
            var current = last.Text;
            if (current == e.Text)
            {
                LettersPanel.Children.RemoveAt(0);
                (LettersPanel.Children[0] as TextBlock).Background = new SolidColorBrush(Colors.Orange);
                TrueTypedCount++;
            }
            else
            {
                (LettersPanel.Children[0] as TextBlock).Background = new SolidColorBrush(Colors.Red);
            }

        }
    }
}
