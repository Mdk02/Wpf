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
    public partial class Responses : Window
    {
        List<Response> response = MainWindow.responses.Where(resp => resp.IdUser == MainWindow.Current_user.Id).ToList();
        public Responses()
        {
            InitializeComponent();

            ShowResponses();
        }

        void ShowResponses()
        {
            List<Border> borders = new List<Border>();

            for (int i = 0; i < response.Count; i++)
            { 
                Border border = new Border
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.Black
                };

                StackPanel stackPanel = new StackPanel
                {
                    MinHeight = 60
                };

                stackPanel.Children.Add(new Label
                {
                    Content = response[i].Message
                });

                border.Child = stackPanel;
                borders.Add(border);
            }

            borders.ForEach(i => MainContent.Children.Add(i));
        }
    }
}
