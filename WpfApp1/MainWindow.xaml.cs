using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        List<string[]> Db = new List<string[]>();

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            UpdateListView();
        }

        private void LoadData()
        {
            try
            {
                using (StreamReader sr = new StreamReader("Stored.csv"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Db.Add(line.Split(','));
                    }
                }
            }
            catch (FileNotFoundException)
            {
                // Handle file not found error
                MessageBox.Show("Data file not found.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void UpdateListView()
        {
            LbContacts.Items.Clear();
            foreach (string[] entry in Db)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Content = entry[0];
                LbContacts.Items.Add(lvi);
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
            SaveData();
            UpdateListView();

            TbName.Text = "";
            TbAddress.Text = "";
            TbNumber.Text = "";
            TbEmail.Text = "";
        }

        private void SaveData()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("Stored.csv"))
                {
                    foreach (string[] entry in Db)
                    {
                        sw.WriteLine(string.Join(",", entry));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while saving data: " + ex.Message);
            }
        }

        private void LbContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LbContacts.SelectedIndex >= 0 && LbContacts.SelectedIndex < Db.Count)
            {
                string selectedItem = LbContacts.SelectedItem.ToString();
                for (int x = 0; x < Db.Count; x++)
                {
                    if (Db[x][0] == selectedItem)
                    {
                        TbName.Text = Db[x][0];
                        TbAddress.Text = Db[x][1];
                        TbNumber.Text = Db[x][2];
                        TbEmail.Text = Db[x][3];
                        break; // Exit the loop once the item is found
                    }
                }
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (LbContacts.SelectedIndex >= 0 && LbContacts.SelectedIndex < Db.Count)
            {
                string[] selectedEntry = Db[LbContacts.SelectedIndex];
                selectedEntry[0] = TbName.Text;
                selectedEntry[1] = TbAddress.Text;
                selectedEntry[2] = TbNumber.Text;
                selectedEntry[3] = TbEmail.Text;
                SaveData();
                UpdateListView();

                TbName.Text = "";
                TbAddress.Text = "";
                TbNumber.Text = "";
                TbEmail.Text = "";
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LbContacts.SelectedIndex >= 0 && LbContacts.SelectedIndex < Db.Count)
            {
                Db.RemoveAt(LbContacts.SelectedIndex);
                SaveData();
                UpdateListView();

                TbName.Text = "";
                TbAddress.Text = "";
                TbNumber.Text = "";
                TbEmail.Text = "";
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            string searchText = TbSearch.Text.ToLower();

            List<string[]> filteredContacts = new List<string[]>();

            foreach (string[] contact in Db)
            {
                if (contact[0].ToLower().Contains(searchText))
                {
                    filteredContacts.Add(contact);
                }
            }

            LbContacts.Items.Clear(); // Clear existing items

            foreach (string[] contact in filteredContacts)
            {
                LbContacts.Items.Add(contact[0]); // Add matching contact name to ListBox
            }
        }
    }
}
