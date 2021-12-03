using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    public partial class Registr : Window
    {
        List<User> users;
        DataMethodsUser methodsUser;
        public Registr()
        {
            InitializeComponent();

            methodsUser = new DataMethodsUser();
            users = methodsUser.GetAllDataUsers().ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach(UIElement elem in grid.Children)
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

            var duplicateLogin = users.FirstOrDefault(u => u.Login.Equals(Login.Text));

            if (duplicateLogin != default)
            {
                MessageBox.Show("логин занят");
            }
            else if (!IsValidName(Name.Text) && !IsValidName(Patronymic.Text) && !IsValidName(LastName.Text))
            {
                MessageBox.Show("Имя должно содержать только буквы");
            }
            else if (!IsValidTelNum(TelNumber.Text))
            {
                MessageBox.Show("это не телефонный номер");
            }
            else if (users.FirstOrDefault(u => u.TelNumber.Equals(TelNumber.Text)) != default)
            {
                MessageBox.Show("телефон занят");
            }
            else if (!BirthDate.SelectedDate.HasValue)
            {
                MessageBox.Show("дата не выбрана");
            }
            else if (!IsValidEmail(Login.Text))
            {
                MessageBox.Show("это не почта");
            }
            else if (((DateTime.Now - BirthDate.SelectedDate.Value).TotalDays / 365.25) < 18)
            {
                MessageBox.Show("Вам должно быть больше 18 лет");
            }
            else
            {
                User new_user = new User()
                {
                    Id = users.LastOrDefault() == default ? 0 : users.LastOrDefault().Id + 1,
                    Login = Login.Text,
                    Password = Password.Text,
                    Name = Name.Text,
                    Lastname = LastName.Text,
                    Patronymic = Patronymic.Text,
                    Role = "user",
                    BirthDate = BirthDate.SelectedDate.Value,
                    TelNumber = TelNumber.Text
                };

                users.Add(new_user);
                methodsUser.AddUser(new_user);

                MessageBox.Show("регистрация прошла успешно");
            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        bool IsValidTelNum(string tel)
        {
            if((tel.StartsWith("8") && tel.Length == 11 || tel.StartsWith("+7") && tel.Length == 12) && long.TryParse(tel.TrimStart(), out _))
            {
                return true;
            }

            return false;
        }

        bool IsValidName(string name)
        {
            foreach (char i in name)
            {
                if (!char.IsLetter(i))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
