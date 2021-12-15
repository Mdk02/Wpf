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
using WpfApp1.Models;
using Microsoft.Win32;
using System.IO;

namespace WpfApp1
{
    public partial class AddProposalWindow : Window
    {
        List<string> files = new List<string>();
        public AddProposalWindow()
        {
            InitializeComponent();
        }

        //add proposal
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Proposal proposal = null;

            foreach (UIElement elem in grid.Children)
            {
                if (elem is TextBox)
                {
                    if (string.IsNullOrEmpty((elem as TextBox).Text))
                    {
                        MessageBox.Show("не все поля заполнены");
                        return;
                    }
                }
            }

            if (!int.TryParse(Square.Text, out _))
            {
                MessageBox.Show("площадь неккоректна");
            }
            else if (!int.TryParse(CountRooms.Text, out _))
            {
                MessageBox.Show("количество комнат неккоректно");
            }
            else if (!int.TryParse(Price.Text, out _))
            {
                MessageBox.Show("цена неккоректна");
            }
            else if (PrimaryOrSecondaryHousing.IsChecked == null)
            {
                MessageBox.Show("неккоректно что-то");
            }
            else if(MainWindow.proposals.Where(u => u.Number.Equals(Number.Text) && u.NumberOfHouse.Equals(NumberOfHouse.Text) &&
                u.Street.Equals(Street.Text) && u.District.Equals(District.Text) && u.City.Equals(City.Text)).Any())
            {
                MessageBox.Show("такая запись есть");
            }
            else if (files.Count > 3 || files.Count < 1)
            {
                MessageBox.Show("фотографий должно быть от 1 до 3");
                files.Clear();
            }
            else
            {
                int id = MainWindow.proposals.LastOrDefault() == default ? 0 : MainWindow.proposals.Last().Id + 1;
                string path = $@"C:\Users\1\Desktop\2\WpfApp1\Photos\{id}";
                
                if (Directory.Exists(path))
                {
                    var files = Directory.GetFiles(path);
                    foreach (var f in files)
                    {
                        File.Delete(f);
                    }
                }

                proposal = new Proposal
                {
                    Id = id,
                    Square = Convert.ToInt32(Square.Text),
                    CountRooms = Convert.ToInt32(CountRooms.Text),
                    Number = Number.Text,
                    NumberOfHouse = NumberOfHouse.Text,
                    Street = Street.Text,
                    District = District.Text,
                    City = City.Text,
                    Description = Description.Text,
                    UserId = MainWindow.Current_user.Id,
                    Price = Convert.ToInt32(Price.Text),
                    PrimaryOrSecondaryHousing = (bool)PrimaryOrSecondaryHousing.IsChecked,
                    Photos = path
                };

                new DataMethodsProposal().AddProposal(proposal);
                MainWindow.proposals.Add(proposal);

                AddFiles(proposal.Id);

                MessageBox.Show("предложение было создано");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == true)
            {                
                foreach(var f in openFileDialog1.FileNames)
                {
                    string ext = System.IO.Path.GetExtension(f);
                    if (ext == ".png" || ext == ".jpg")
                    {
                        files.Add(f);       
                    }
                    else
                    {
                        MessageBox.Show("должны быть картинки");
                        files.Clear();
                        return;
                    }
                }
            }
        }

        void AddFiles(int id)
        {
            string path = $@"C:\Users\1\Desktop\2\WpfApp1\Photos\{id}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory($@"C:\Users\1\Desktop\2\WpfApp1\Photos\{id}");
            }

            foreach (string file in files)
            {
                string name = file.Split('\\')[file.Split('\\').Length - 1];
                File.Copy(file, $@"C:\Users\1\Desktop\2\WpfApp1\Photos\{id}\{name}");
            }
        }
    }
}
