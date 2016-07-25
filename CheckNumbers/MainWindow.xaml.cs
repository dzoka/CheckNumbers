// (c) 2016 Dzoka

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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CheckNumbers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int maxDigits = 30;
        bool[] digitMismatch = new bool[maxDigits];
        List<string> numbers = new List<string>();
        Brush digitMismatchTrue = Brushes.Red;
        Brush digitMismatchFalse = Brushes.Black;
        Brush digitBackground = Brushes.White;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add item to ListBox control and highlight mismatches
        /// </summary>
        /// <param name="textString">item to add</param>
        private void addItemToListBox(string textString)
        {
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            for (int i = 0; i < textString.Length; i++)
            {
                TextBlock tb = new TextBlock();
                tb.Background = digitBackground;
                if (digitMismatch[i])
                {
                    tb.Foreground = digitMismatchTrue;
                }
                else
                {
                    tb.Foreground = digitMismatchFalse;
                }
                tb.Inlines.Add(new Run(textString.Substring(i, 1)));
                sp.Children.Add(tb);
            }
            listBox.Items.Add(sp);
        }

        private void findMismatches(string textString)
        {
            if (numbers.Count > 0)
            {
                int j = 0;                      // shorter in length
                if (textString.Length < numbers.ElementAt(0).Length)
                {
                    j = textString.Length;
                }
                else
                {
                    j = numbers.ElementAt(0).Length;
                }
                for (int i = 0; i < j; i++)
                {
                    if (textString.Substring(i,1).Equals(numbers.ElementAt(0).Substring(i,1)) == false)
                    {
                        digitMismatch[i] = true;
                    }
                }
                for (int i = j; i < maxDigits; i++)
                {
                    digitMismatch[i] = true;    // fill rest as mismatch
                }
            }
            numbers.Add(textString);
            updateListBox();
        }

        private void updateListBox()
        {
            listBox.Items.Clear();
            foreach (string s in numbers) { addItemToListBox(s); }
        }

        private void buttonSort_Click(object sender, RoutedEventArgs e)
        {
            numbers.Sort();
            updateListBox();
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            numbers.Clear();
            for (int i = 0; i < maxDigits; i++) { digitMismatch[i] = false; }
            updateListBox();
        }

        private void textBoxNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                if (textBoxNumber.Text.ToString().Length > maxDigits)
                {
                    findMismatches(textBoxNumber.Text.ToString().Substring(0, maxDigits));
                }
                else
                {
                    findMismatches(textBoxNumber.Text.ToString());
                }
                textBoxNumber.Text = "";
            }
        }
    }
}
