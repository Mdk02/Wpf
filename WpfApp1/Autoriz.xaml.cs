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
    public partial class Autoriz : Window
    {
        List<User> users;
        public Autoriz()
        {
            InitializeComponent();

            Closing += CloseWindow;
        }

        //Enter
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            users = new DataMethodsUser().GetAllDataUsers().ToList();

            if (!string.IsNullOrEmpty(loginTextBox.Text) && !string.IsNullOrEmpty(passwordTextBox.Password))
            {
                var user = users.FirstOrDefault(u => u.Login.Equals(loginTextBox.Text) && u.Password.Equals(passwordTextBox.Password));

                if (user == default)
                {
                    MessageBox.Show("error");
                }
                else
                {
                    MainWindow.Current_user = user;
                    Closing -= CloseWindow;
                    Close();
                }
            }
            else
            {
                MessageBox.Show("поля пустые");
            }
        }

        //Reg
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new Registr().ShowDialog();
        }

        void CloseWindow(object sender, System.ComponentModel.CancelEventArgs target)
        {
            Application.Current.Shutdown();
        }

        //public User GetCurrentUser() { }
    }
}
