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
    public partial class UserProfile : Window
    {
        public UserProfile()
        {
            InitializeComponent();
            ShowProposals();
            Funcs();
        }

        void ShowProposals()
        {
            List<Proposal> proposals = MainWindow.proposals.Where(prop => prop.UserId == MainWindow.Current_user.Id).ToList();

            MainWindow.PrintProposals(proposals, true).ForEach(i => MainContent.Children.Add(i));
        }

        void Funcs()
        {
            Button button = new Button
            {
                Content = "добавить предложение"
            };

            button.Click += new RoutedEventHandler((object sender, RoutedEventArgs target) =>
            {
                new AddProposalWindow().ShowDialog();
            });

            Button reset = new Button
            {
                Content = "обновить",
                Margin = new Thickness(10,0,0,0)
            };

            reset.Click += new RoutedEventHandler((object sender, RoutedEventArgs target) =>
            {
                MainContent.Children.Clear();
                ShowProposals();
            });

            Functions.Children.Add(button);
            Functions.Children.Add(reset);

            if (MainWindow.Current_user.Role.Equals("Rieltor"))
            {
                var button1 = new Button
                {
                    Content = "Заявки",
                    Margin = new Thickness(10,0,0,0),
                    MinWidth = 50
                };

                button1.Click += new RoutedEventHandler((object sender, RoutedEventArgs target) =>
                {
                    new Requests().ShowDialog();
                });

                Functions.Children.Add(button1);
            }
            else if (MainWindow.Current_user.Role.Equals("user"))
            {
                var button1 = new Button
                {
                    Content = "Ответы",
                    Margin = new Thickness(10,0,0,0),
                    MinWidth = 50
                };

                button1.Click += new RoutedEventHandler((object sender, RoutedEventArgs target) =>
                {
                    new Responses().ShowDialog();
                });

                Functions.Children.Add(button1);
            }
        }
    }
}
