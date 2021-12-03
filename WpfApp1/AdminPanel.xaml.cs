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
    public partial class AdminPanel : Window
    {
        List<User> users;
        public AdminPanel()
        {
            InitializeComponent();
            this.Closed += AdminPanel_Closed;

            users = new DataMethodsUser().GetAllDataUsers().ToList();

            ShowUsers();
        }

        private void AdminPanel_Closed(object sender, EventArgs e)
        {
            new DataMethodsUser().RewriteAllUsers(users);
        }

        void ShowUsers()
        {
            users.ForEach(i => UsersListBox.Items.Add(i.Login + "  " + i.Lastname + "  " + i.Name + "  " + i.Patronymic + "  " + i.Role));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UsersListBox.SelectedItem != null)
            {
                User tempUser = users.FirstOrDefault(t => t.Login == UsersListBox.SelectedItem.ToString().Split(' ')[0]);

                users.Remove(tempUser);
                UsersListBox.Items.Remove(UsersListBox.SelectedItem);

                if (tempUser.Role.Equals("Rieltor"))
                {
                    tempUser.Role = "user";
                }
                else if (tempUser.Role.Equals("user"))
                {
                    tempUser.Role = "Rieltor";
                }

                users.Add(tempUser);
                UsersListBox.Items.Add(tempUser.Login + "  " + tempUser.Lastname + "  " + tempUser.Name + "  " + tempUser.Patronymic + "  " + tempUser.Role);
            }
        }
    }
}
