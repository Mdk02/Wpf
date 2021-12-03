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
using WpfApp1.Models;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public static User Current_user;
        public static List<Proposal> proposals;
        public static List<Request> requests;
        public static List<Response> responses;

        public MainWindow()
        {
            InitializeComponent();

            new Autoriz().ShowDialog();

            proposals = new DataMethodsProposal().GetAllDataProposals().ToList();
            requests = new DataMethodRequests().GetAllDataRequests().ToList();
            responses = new DataMethodResponse().GetAllDataResponses().ToList();

            ShowFilter();
            ShowProposals(proposals);
        }

        void ShowFilter()
        {
            var button = new Button
            {
                Content = "Profile",
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 60,
                Margin = new Thickness(15, 0, 0, 0),
                MaxHeight = 40
            };

            button.Click += new RoutedEventHandler((object sender, RoutedEventArgs e) =>
            {
                new UserProfile().ShowDialog();
            });

            var reset = new Button
            {
                Content = "обновить",
                Margin = new Thickness(15, 0, 0, 0),
                MaxHeight = 40
            };

            reset.Click += new RoutedEventHandler((object sender, RoutedEventArgs target) =>
            {
                MainContent.Children.Clear();
                ShowProposals(proposals);
            });

            var filt_price = new Button
            {
                Content = "цена по возрастанию",
                Margin = new Thickness(15, 0, 0, 0),
                MaxHeight = 40
            };

            filt_price.Click += new RoutedEventHandler((object sender, RoutedEventArgs target) => 
            {
                proposals.Sort((a, b) => a.Price.CompareTo(b.Price));
            });

            Filters.Children.Add(button);
            Filters.Children.Add(reset);
            Filters.Children.Add(filt_price);

            var filt_price_reverse = new Button
            {
                Content = "цена по убыванию",
                Margin = new Thickness(15, 0, 0, 0),
                MaxHeight = 40
            };

            filt_price_reverse.Click += new RoutedEventHandler((object sender, RoutedEventArgs target) =>
            {
                proposals.Sort((a, b) => a.Price.CompareTo(b.Price));
                proposals.Reverse();
            });

            Filters.Children.Add(filt_price_reverse);

            TextBox textBox = new TextBox
            {
                Text = "Введите название города",
                Margin = new Thickness(15, 0, 0, 0),
                MaxHeight = 40,
                MinWidth = 50
            };

            textBox.TextChanged += new TextChangedEventHandler((object sender, TextChangedEventArgs target) =>
            {
                MainContent.Children.Clear();
                ShowProposals(proposals.Where(pr => pr.City.StartsWith(textBox.Text)).ToList());
            });

            Filters.Children.Add(textBox);

            if (MainWindow.Current_user != null && MainWindow.Current_user.Role.Equals("Admin"))
            {
                var admin_button = new Button
                {
                    Content = "adminPanel",
                    Margin = new Thickness(15, 0, 0, 0),
                    MaxHeight = 40
                };

                admin_button.Click += ((object sender, RoutedEventArgs target) => {
                    new AdminPanel().ShowDialog();
                });

                Filters.Children.Add(admin_button);
            }     
        }

        void ShowProposals(List<Proposal> someProposals)
        {           
            PrintProposals(someProposals)?.ForEach(i => MainContent.Children.Add(i));
        }

        public static List<Border> PrintProposals(List<Proposal> proposals, bool deleteButton = false)
        {
            if (MainWindow.Current_user != null)
            {
                List<Border> borders = new List<Border>();

                for (int i = 0; i < proposals.Count; i++)
                {
                    Proposal prop = proposals[i];
                    Border border = new Border
                    {
                        BorderThickness = new Thickness(1),
                        BorderBrush = Brushes.Black
                    };

                    Label mainDescription = new Label();
                    mainDescription.Content = prop.Description;

                    Label header = new Label();
                    header.Content = (prop.PrimaryOrSecondaryHousing ? "Первичное" : "Вторичное") + ", " + prop.CountRooms.ToString() + " комнатная, " + prop.Square.ToString() + "м2"; ;

                    #region
                    //header.MouseEnter += new MouseEventHandler((object sender, MouseEventArgs e) =>
                    //{
                    //    header.Foreground = Brushes.Red;
                    //});

                    //header.MouseLeave += new MouseEventHandler((object sender, MouseEventArgs e) =>
                    //{
                    //    header.Foreground = Brushes.Black;
                    //});

                    //header.MouseLeftButtonDown += new MouseButtonEventHandler((object sender, MouseButtonEventArgs e) =>
                    //{
                    //    SeparateProposal separateProposal = new SeparateProposal(prop);
                    //    separateProposal.ShowDialog();
                    //});
                    #endregion

                    Label price = new Label()
                    {
                        Content = "Цена: " + prop.Price.ToString(),
                        HorizontalAlignment = HorizontalAlignment.Right
                    };

                    StackPanel stackPanel = new StackPanel
                    {
                        MinHeight = 80
                    };

                    stackPanel.Children.Add(header);
                    stackPanel.Children.Add(mainDescription);
                    stackPanel.Children.Add(price);
                    stackPanel.Children.Add(new Label
                    {
                        Content = prop.City + ", " + prop.District + ", " + prop.NumberOfHouse,
                    });

                    if (deleteButton || !MainWindow.Current_user.Role.Equals("user"))
                    {
                        Button delete_button = new Button
                        {
                            Content = "delete",
                            HorizontalAlignment = HorizontalAlignment.Right,
                            Width = 50,
                            Height = 30
                        };

                        delete_button.Click += new RoutedEventHandler((object sender, RoutedEventArgs target) =>
                        {
                            MainWindow.proposals.Remove(prop);
                            new DataMethodsProposal().DeleteProposal(prop);

                            Label label = new Label
                            {
                                Content = "удалено",
                                Foreground = Brushes.Red
                            };

                            stackPanel.Children.Add(label);
                        });

                        stackPanel.Children.Add(delete_button);
                    }

                    

                    if (MainWindow.Current_user.Role.Equals("user"))
                    {
                        var request_button = new Button
                        {
                            Content = "Откликнуться"
                        };

                        request_button.Click += new RoutedEventHandler((object sender, RoutedEventArgs target) =>
                        {
                            Request request = new Request
                            {
                                Id = MainWindow.requests.Any() ? MainWindow.requests.Last().Id + 1 : 0,
                                IdProposal = prop.Id,
                                IdUser = MainWindow.Current_user.Id,
                            };

                            if (MainWindow.requests.Any() && MainWindow.requests.Exists(t => t.IdProposal == request.IdProposal && t.IdUser == request.IdUser))
                            {
                                MessageBox.Show("запрос уже отправлен");
                                return;
                            }

                            MessageBox.Show("запрос отправлен");
                            requests.Add(request);
                            new DataMethodRequests().AddRequest(request);
                        });

                        stackPanel.Children.Add(request_button);
                    }

                    border.Child = stackPanel;
                    borders.Add(border);
                }

                return borders;
            }
            else
            {
                return null;
            }
        }
    }
}
