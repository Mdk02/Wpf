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
    public partial class Autoriz : Window
    {
        List<User> users;
        string path = @"C:\Users\1\Desktop\2\WpfApp1\Data\LastUser.txt";
        public Autoriz()
        {
            InitializeComponent();

            Closing += CloseWindow;

            ShowSave();
        }

        //Enter
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            users = new DataMethodsUser().GetAllDataUsers().ToList();            

            if (!string.IsNullOrEmpty(loginTextBox.Text) && !string.IsNullOrEmpty(passwordTextBox.Password))
            {
                MessageBox.Show(loginTextBox.Text);
                MessageBox.Show(passwordTextBox.Password);

                var user = users.FirstOrDefault(u => u.Login.Equals(loginTextBox.Text) && u.Password.Equals(passwordTextBox.Password));

                if (user == default)
                {
                    MessageBox.Show("error");
                }
                else
                {
                    MainWindow.Current_user = user;
                    Save(user);
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

        void Save(User user)
        {
            if (SavePassword.IsChecked.Value)
            {               
                File.WriteAllLines(path, new string[] { user.Login, user.Password });
            }
        }

        void ShowSave()
        {
            string log = "", pass = "";

            using (StreamReader sw = new StreamReader(path))
            {
                string line;
                List<string> lines = new List<string>();
                while ((line = sw.ReadLine()) != null)
                {
                    lines.Add(line);
                }

                log = lines[0];
                pass = lines[1];
            }

            if (!string.IsNullOrEmpty(log))
            {
                loginTextBox.Text = log;
                passwordTextBox.Password = pass;
            }
        }
    }
}
