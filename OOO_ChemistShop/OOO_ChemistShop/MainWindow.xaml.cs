using EasyCaptcha.Wpf;
using OOO_ChemistShop.Classes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace OOO_ChemistShop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String captchaText;
        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            const int lengthCaptcha = 4;            //Длина каптчи
            captcha.CreateCaptcha(Captcha.LetterOption.Alphanumeric, lengthCaptcha); //Создание каптчи
            captchaText = captcha.CaptchaText;	//Сформированная строка каптчи

            try
            {
                Helper.DB = new Model.OOODBChemistShop(); //Проверяем связь с базой
            }
            catch
            {
                MessageBox.Show("Не удаётся установить связь с базой");
                return;
            }

        }


        /// <summary>
        /// Показывать подсказку "Введите каптчу"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textboxcaptcha_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textboxcaptcha.Text.Length == 0)
            {
                CaptchaHint.Visibility = Visibility.Visible;
            }
            else
            {
                CaptchaHint.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Показать пароль при нажатии на иконку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPassword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TbPasswordAutho.Text = textboxpassword.Password;
            textboxpassword.Password = "";
            TbPasswordAutho.Visibility = Visibility.Visible;
            textboxpassword.Visibility = Visibility.Hidden;
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
            textboxpassword.Password = TbPasswordAutho.Text;
            TbPasswordAutho.Text = "";
            TbPasswordAutho.Visibility = Visibility.Hidden;
            textboxpassword.Visibility = Visibility.Visible;
            ShowPassword.Visibility = Visibility.Visible;
            HiddenPassword.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Завершение работы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
           if(MessageBox.Show("Вы точно хотите выйти из приложения", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// Заход как гость
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonGuest_Click(object sender, RoutedEventArgs e)
        {
            Helper.User = null;
            GoCatalog();
        }

        /// <summary>
        /// Переход в каталог при авторизации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ButtonAutho_Click(object sender, RoutedEventArgs e)
        {
            
            //Форумируем переменные для хранения текста объектов интерфейса
            string login = TbLoginAutho.Text;
            string password = textboxpassword.Password;
            string tbpassword = TbPasswordAutho.Text;
            string tbcaptcha = textboxcaptcha.Text;

            StringBuilder sb = new StringBuilder();

            if(String.IsNullOrEmpty(login))
            {
                sb.AppendLine("Вы забыли ввести логин");
            }
            if (String.IsNullOrEmpty(password) && TbPasswordAutho.Text == "" || String.IsNullOrEmpty(tbpassword) && textboxpassword.Password == "")
            {
                sb.AppendLine("Вы забыли ввести пароль");
            }
            if (String.IsNullOrEmpty(tbcaptcha))
            {
                sb.AppendLine("Вы забыли ввести каптчу");
            }
            else if(captcha.CaptchaText != tbcaptcha) //условие, если каптча неверная
            {
                sb.AppendLine("Каптча неверная");
                ButtonAutho.IsEnabled = false;
                captcha.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 4);
                captchaText = captcha.CaptchaText;  //Сохранение строки каптчи
                timer.Tick += new EventHandler(TimerTick);
                timer.Interval = new TimeSpan(40000000);
                timer.Start();
            }
            if(sb.Length > 0)
            {
                MessageBox.Show(sb.ToString());
                return;
            }

            //Поиск в БД пользователя с таким логином и паролем
            //Получение списка пользователей в листе
            List<Model.Users> users = new List<Model.Users>();

            users = Helper.DB.Users.Where(x => x.UserLogin == login && x.UserPassword == password || x.UserLogin == login && x.UserPassword == tbpassword).ToList();

            Helper.User = users.FirstOrDefault();
            sb.Clear();
            if(Helper.User != null)
            {
                sb.AppendLine("Здравствуйте, уважаемый(-ая) " + Helper.DB.Roles.Where(x => x.RoleId == Helper.User.Roles.RoleId).Select(x => x.RoleName).FirstOrDefault() + " " + Helper.User.UserFullName + "!"); ;
                MessageBox.Show(sb.ToString());
                GoCatalog();
            }
            else
            {
                MessageBox.Show("Неверный пользователь или пароль");
            }
        }


        /// <summary>
        /// Функция активация таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTick(object sender, EventArgs e)
        {
            ButtonAutho.IsEnabled = true;
            timer.Stop();
        }


        /// <summary>
        /// Функция перехода в каталог
        /// </summary>
        private void GoCatalog()
        {
            View.CatalogWindow catalog = new View.CatalogWindow();
            this.Hide();
            catalog.ShowDialog();
            this.Show();
        }

        /// <summary>
        /// Функция перехода на окно регистрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRegistration_Click(object sender, RoutedEventArgs e)
        {
            View.RegistrationWindow registrationWindow = new View.RegistrationWindow();
            this.Hide();
            registrationWindow.ShowDialog();
            this.Show();
        }
    }
}
