using OOO_ChemistShop.Classes;
using OOO_ChemistShop.Model;
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
using System.Xml.Linq;

namespace OOO_ChemistShop.View
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        Model.Users users;  
        public RegistrationWindow()
        {
            InitializeComponent();
            tbId.IsEnabled = true;		//Блокировать ID
        }

        /// <summary>
        /// Событие кнопки выхода из окна регистрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите закрыть это окно?", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close(); //Закрытие окна
            }
        }

        /// <summary>
        /// Событие кнопки регистрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRegistration_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();	//Строка с сообщениями
            sb.Clear();
            //Проверка непустых значений для обязательных полей
            if (String.IsNullOrEmpty(tbFIOName.Text))
            { sb.Append("Вы не ввели ФИО." + Environment.NewLine); }
            if (String.IsNullOrEmpty(tbLogin.Text))
            { sb.Append("Вы не ввели логин." + Environment.NewLine); }
            if (String.IsNullOrEmpty(pbPassword.Password) && tbPassword.Text == "" || String.IsNullOrEmpty(tbPassword.Text) && pbPassword.Password == "")
            { sb.Append("Вы не ввели пароль." + Environment.NewLine); }
            if (String.IsNullOrEmpty(PBVerifpassword.Password) && tbVerifPassword.Text == "" || String.IsNullOrEmpty(tbVerifPassword.Text) && PBVerifpassword.Password == "")
            { sb.Append("Вы не подтвердили пароль." + Environment.NewLine); }
            if (sb.Length > 0)			//Есть сообщения об ошибках
            {
                MessageBox.Show(sb.ToString());
                return;
            }
            else
            {
                if ( pbPassword.Password != PBVerifpassword.Password && tbPassword.Text == "" && tbVerifPassword.Text == ""
                || tbPassword.Text != tbVerifPassword.Text && pbPassword.Password == "" && PBVerifpassword.Password == ""
                || pbPassword.Password != tbVerifPassword.Text && tbVerifPassword.Text == "" && PBVerifpassword.Password == ""
                || tbPassword.Text != PBVerifpassword.Password && pbPassword.Password == "" && tbVerifPassword.Text == "") //проверяем что пароли совпадают, чтобы регистрация прошла успешно
                {
                    MessageBox.Show("Пароли не совпадают!", "Информация");
                }
                else
                {
                    users = Helper.DB.Users.FirstOrDefault(x => x.UserLogin == tbLogin.Text);

                    if (users == null)
                    {
                        users = new Model.Users();

                        if (tbId.IsEnabled)     //При добавлении
                        {
                            tbId.Text = users.UserId.ToString();
                        }

                        users.UserRoleId = 1;
                        users.UserFullName = tbFIOName.Text;
                        users.UserLogin = tbLogin.Text;

                        if (!String.IsNullOrEmpty(pbPassword.Password) && tbPassword.Text == "")
                        {
                            users.UserPassword = pbPassword.Password; //заносим пароль из PB в базу
                        }
                        else if (!String.IsNullOrEmpty(tbPassword.Text) && pbPassword.Password == "")
                        {
                            users.UserPassword = tbPassword.Text; //заносим пароль из TB в базу
                        }

                        if (tbId.IsEnabled)             //При добавлении
                        {
                            Helper.DB.Users.Add(users);
                        }
                        try
                        {
                            Helper.DB.SaveChanges();        //Обновить БД и при добавлении/редактировании

                            this.Close();
                            MessageBox.Show("Пользователь успешно зарегистрирован");
                            Application.Current.Shutdown();
                            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                        }
                        catch
                        {
                            MessageBox.Show("При регистрации пользователя возникли проблемы");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пользователь с таким логином уже есть в БД!");
                    }
                }

            }
        }

        /// <summary>
        /// Показать пароль при нажатии на иконку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RetryShowPassword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tbVerifPassword.Text = PBVerifpassword.Password;
            PBVerifpassword.Password = "";
            tbVerifPassword.Visibility = Visibility.Visible;
            PBVerifpassword.Visibility = Visibility.Hidden;
            RetryShowPassword.Visibility = Visibility.Hidden;
            RetryHiddenPassword.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Скрыть пароль при нажатии на иконку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RetryHiddenPassword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PBVerifpassword.Password = tbVerifPassword.Text;
            tbVerifPassword.Text = "";
            tbVerifPassword.Visibility = Visibility.Hidden;
            PBVerifpassword.Visibility = Visibility.Visible;
            RetryShowPassword.Visibility = Visibility.Visible;
            RetryHiddenPassword.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Показать пароль при нажатии на иконку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPassword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tbPassword.Text = pbPassword.Password;
            pbPassword.Password = "";
            tbPassword.Visibility = Visibility.Visible;
            pbPassword.Visibility = Visibility.Hidden;
            ShowPassword.Visibility = Visibility.Hidden;
            HiddenPassword.Visibility = Visibility.Visible;
        }


        /// <summary>
        /// Скрыть пароль при нажатии на иконку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HiddenPassword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            pbPassword.Password = tbPassword.Text;
            tbPassword.Text = "";
            tbPassword.Visibility = Visibility.Hidden;
            pbPassword.Visibility = Visibility.Visible;
            ShowPassword.Visibility = Visibility.Visible;
            HiddenPassword.Visibility = Visibility.Hidden;
        }

    }
}
