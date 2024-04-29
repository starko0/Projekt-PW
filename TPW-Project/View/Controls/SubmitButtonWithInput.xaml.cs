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

namespace TPW_Project.View.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy SubmitButtonWithInput.xaml
    /// </summary>
    public partial class SubmitButtonWithInput : UserControl
    {
        public SubmitButtonWithInput()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            string newText;

            if (string.IsNullOrEmpty(textBox.Text) && e.Text == "0")
            {
                e.Handled = true;
                return;
            }

            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
                return;
            }

            if (string.IsNullOrEmpty(textBox.Text))
            {
                newText = e.Text;
            }
            else
            {
                newText = textBox.Text + e.Text;
            }

            if (!string.IsNullOrEmpty(newText))
            {
                if (!int.TryParse(newText, out int number) || number < 1 || number > 5)
                {
                    e.Handled = true;
                    return;
                }
            }
        }



    }
}
