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

namespace SuperType.Pages
{
    /// <summary>
    /// Логика взаимодействия для TestingPage.xaml
    /// </summary>
    public partial class TestingPage : Page
    {
        int TypedCount;
        int TrueTypedCount;
        TimeSpan StartTime;
        void LoadText()
        {
            var text = File.ReadAllText("Textarium\\shop1.txt");
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
        }

        private void ScrollViewer_TextInput(object sender, TextCompositionEventArgs e)
        {
            Console.WriteLine(e.Text);
            var last = LettersPanel.Children[0] as TextBlock;
            var current = last.Text;
            if(current == e.Text)
            {
                LettersPanel.Children.RemoveAt(0);
                (LettersPanel.Children[0] as TextBlock).Background = new SolidColorBrush(Colors.Orange);
            }
            else
            {
                (LettersPanel.Children[0] as TextBlock).Background = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
