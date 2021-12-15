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
using System.Windows.Shapes;
using WpfApp1.Models;

namespace WpfApp1
{
    public partial class SeparateProposal : Window
    {
        Proposal current_propos;
        public SeparateProposal(Proposal proposal)
        {
            InitializeComponent();

            current_propos = proposal;
            
            ShowSomething();
        }

        void ShowSomething()
        {
            if (MainWindow.Current_user.Role.Equals("user") && MainWindow.Current_user.Id != current_propos.UserId)
            {
                send.Visibility = Visibility.Visible;
            }

            string[] file = Directory.GetFiles(MainWindow.pathPhotos + current_propos.Id);

            if(file.Length == 1)
            {
                first.Source = new BitmapImage(
                        new Uri(file[0]));
                first.Stretch = Stretch.Uniform;
            }
            else if(file.Length == 2)
            {
                first.Source = new BitmapImage(
                        new Uri(file[0]));
                first.Stretch = Stretch.Uniform;

                second.Source = new BitmapImage(
                        new Uri(file[1]));
                second.Stretch = Stretch.Uniform;
            }
            else if(file.Length == 3)
            {
                first.Source = new BitmapImage(
                        new Uri(file[0]));
                first.Stretch = Stretch.Uniform;

                second.Source = new BitmapImage(
                        new Uri(file[1]));
                second.Stretch = Stretch.Uniform;

                third.Source = new BitmapImage(
                        new Uri(file[2]));
                third.Stretch = Stretch.Uniform;
            }

            header.Text = (current_propos.PrimaryOrSecondaryHousing ? "Первичное" : "Вторичное") + ", " + current_propos.CountRooms.ToString() + " комнатная, " + current_propos.Square.ToString() + "м2";
            priceTextBox.Text = current_propos.Price.ToString();
            desc.Text = current_propos.Description;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {       
            Request request = new Request
            {
                Id = MainWindow.requests.Any() ? MainWindow.requests.Last().Id + 1 : 0,
                IdProposal = current_propos.Id,
                IdUser = MainWindow.Current_user.Id,
            };

            if (MainWindow.requests.Any() && MainWindow.requests.Exists(t => t.IdProposal == request.IdProposal && t.IdUser == request.IdUser))
            {
                MessageBox.Show("запрос уже отправлен");
                return;
            }

            MessageBox.Show("запрос отправлен");
            MainWindow.requests.Add(request);
            new DataMethodRequests().AddRequest(request);           
        }
    }
}
