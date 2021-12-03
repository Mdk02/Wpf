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

namespace WpfApp1
{
    public partial class AddProposalWindow : Window
    {
        public AddProposalWindow()
        {
            InitializeComponent();
        }

        //add proposal
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Proposal proposal = null;

            //foreach (UIElement elem in grid.Children)
            //{
            //    if (elem is TextBox)
            //    {
            //        if (string.IsNullOrEmpty((elem as TextBox).Text))
            //        {
            //            MessageBox.Show("не все поля заполнены");
            //            return;
            //        }
            //    }
            //}

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
            else
            {             
                proposal = new Proposal
                {
                    Id = MainWindow.proposals.Last().Id + 1,
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
                    PrimaryOrSecondaryHousing = (bool)PrimaryOrSecondaryHousing.IsChecked
                };

                new DataMethodsProposal().AddProposal(proposal);
                MainWindow.proposals.Add(proposal);

                MessageBox.Show("предложение было создано");
            }
        }
    }
}
