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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string user = "";
        List<string[]> Db = new List<string[]>();

        public MainWindow()
        {
            InitializeComponent();

            using (StreamReader sr = new StreamReader("Stored.csv"))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    Db.Add(line.Split(','));
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string[] add = new string[4];
            add[0] = TbName.Text;
            add[1] = TbAddress.Text;
            add[2] = TbNumber.Text;
            add[3] = TbEmail.Text;

            Db.Add(add);

            using (StreamWriter sw = new StreamWriter("Stored.csv"))
            {
                foreach (string[] entry in Db)
                {
                    for (int y = 0; y < entry.Length; y++)
                    {
                        sw.Write(entry[y]);
                        if (y < entry.Length - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.WriteLine();
                }
            }

            TbName.Text = "";
            TbAddress.Text = "";
            TbNumber.Text = "";
            TbEmail.Text = "";
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("Stored.csv"))
            {
                foreach (string[] entry in Db)
                {
                    for (int y = 0; y < entry.Length; y++)
                    {
                        sw.Write(entry[y]);
                        if (y < entry.Length - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.WriteLine();
                }
            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
