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
    public partial class Requests : Window
    {
        List<Request> requests = MainWindow.requests;
        User Rieltor = MainWindow.Current_user;
        public Requests()
        {
            InitializeComponent();

            ShowRequests();
        }

        void ShowRequests()
        {
            List<Border> borders = new List<Border>();

            for (int i = 0; i < requests.Count; i++)
            {
                Proposal proposal = MainWindow.proposals.FirstOrDefault(t => t.Id == requests[i].IdProposal);
                User user = new DataMethodsUser().GetAllDataUsers().FirstOrDefault(t => t.Id == requests[i].IdUser);

                if(proposal == default || user == default)
                {
                    continue;
                }

                Border border = new Border
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.Black
                };

                StackPanel stackPanel = new StackPanel
                {
                    MinHeight = 80
                };

                stackPanel.Children.Add(new Label
                {                 
                    Content = $"Поступил запрос на {proposal.City + " " + proposal.District + " " + proposal.Street + " " + proposal.NumberOfHouse + " " + proposal.Number} " +
                    $"от {user.Login + " " + user.Lastname + " " + user.Name + " " + user.Patronymic + " " + user.TelNumber}"
                });

                Label label = new Label();

                if (!MainWindow.responses.Exists(t => t.IdProposal == requests[i].IdProposal && t.IdUser == requests[i].IdUser))
                {
                    label.Content = "запрос открыт";
                    label.Foreground = Brushes.Green;
                }
                else
                {
                    label.Content = "запрос закрыт";
                    label.Foreground = Brushes.Red;
                }

                Button button = new Button
                {
                    Content = "Ответить",
                    HorizontalAlignment = HorizontalAlignment.Right
                };

                button.Click += new RoutedEventHandler((object sender, RoutedEventArgs target) =>
                {
                    Response response = new Response
                    {
                        Id = MainWindow.responses.Any() ? MainWindow.responses.Last().Id + 1 : 0,
                        IdProposal = proposal.Id,
                        IdRieltor = Rieltor.Id,
                        IdUser = user.Id,
                        Message = $"на вашу заявку по поводу {proposal.City + " " + proposal.District + " " + proposal.Street + " " + proposal.NumberOfHouse + " " + proposal.Number} " +
                        $"ответил {Rieltor.Lastname + " " + Rieltor.Name + " " + Rieltor.Patronymic + " " + Rieltor.TelNumber}"
                    };

                    if (MainWindow.responses.Any() && MainWindow.responses.Exists(t => t.IdProposal == response.IdProposal && t.IdUser == response.IdUser && t.IdRieltor == response.IdRieltor))
                    {
                        MessageBox.Show("запрос уже отправлен");
                        return;
                    }

                    label.Content = "запрос закрыт";
                    label.Foreground = Brushes.Red;

                    MainWindow.responses.Add(response);
                    new DataMethodResponse().AddResponse(response);
                    MessageBox.Show("Вы ответили");
                });

                stackPanel.Children.Add(button);
                stackPanel.Children.Add(label);
                border.Child = stackPanel;
                borders.Add(border);
            }

            borders.ForEach(i => MainContent.Children.Add(i));
        }
    }
}
