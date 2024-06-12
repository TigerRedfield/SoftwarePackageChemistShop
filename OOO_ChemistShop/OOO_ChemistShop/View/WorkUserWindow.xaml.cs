using OOO_ChemistShop.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace OOO_ChemistShop.View
{
    /// <summary>
    /// Логика взаимодействия для WorkUserWindow.xaml
    /// </summary>
    public partial class WorkUserWindow : Window
    {
        public WorkUserWindow()
        {
            InitializeComponent();
            this.DataContext = this; //Элементы интерфейса связать с данными
        }

        Model.Users workUsers = new Model.Users(); //объект для работы с пользователями

        /// <summary>
        /// Подготовка окна при первоначальной загрузки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Model.Roles> roles = new List<Model.Roles>();

            roles.AddRange(Helper.DB.Roles.ToList());

            CbRoleUser.DisplayMemberPath = "RoleName";
            CbRoleUser.SelectedValuePath = "RoleId";
            CbRoleUser.ItemsSource = roles;
            ShowUsers();
        }

        /// <summary>
        /// Событие обновления данных пользователя
        /// </summary>
        private void ShowUsers()
        {
            List<Model.Users> users = Helper.DB.Users.Where(x => x.UserId != Helper.User.UserId).ToList();
            DataGridUsers.ItemsSource = users;

            txtUserId.Text = "";
            CbRoleUser.SelectedIndex = -1;
            txtFioUser.Text = "";
            txtPasswordUser.Text = "";
            txtLoginUser.Text = "";
        }

        /// <summary>
        /// Событие закрытия окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            //Предупреждение о том, что пользователь вернётся на окно каталога
            if (MessageBox.Show("Вы точно хотите вернуться на окно каталога?", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close(); //Закрытие окна
            }
        }

        /// <summary>
        /// Отменить выделение, если выбрана строка в DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelUser_Click(object sender, RoutedEventArgs e)
        {
            ShowUsers();
            DataGridUsers.UnselectAllCells();
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserId.Text == "")
            {
                MessageBox.Show("Не выбрана строка для удаления!", "Внимание!");
                return;
            }
            else
            {
                if (MessageBox.Show("Вы точно хотите удалить данного пользователя?", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    int ID = int.Parse(txtUserId.Text);

                    var item = Helper.DB.Users.SingleOrDefault(x => x.UserId == ID);

                    if (item != null)
                    {
                    
                        try
                        {
                            Helper.DB.Users.Remove(item);
                            Helper.DB.SaveChanges();
                            DataGridUsers.UnselectAllCells();
                            MessageBox.Show("БД успешно обновлена");
                        }
                        catch
                        {
                            MessageBox.Show("С обновлением БД проблемы");
                        }
                        
                    }
                }
                else
                {
                DataGridUsers.UnselectAllCells();
                }

            }

            ShowUsers();
        }

        /// <summary>
        /// Событие редактирования данных пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserId.Text == "")
            {
                MessageBox.Show("Не выбрана строка для редактирования!", "Внимание!");
                return;
            }
            else
            {
                int ID = int.Parse(txtUserId.Text);

                var item = Helper.DB.Users.SingleOrDefault(x => x.UserId == ID);

                if (item != null)
                {
                    try
                    {
                        workUsers.UserRoleId = (int)CbRoleUser.SelectedValue;
                        workUsers.UserFullName = txtFioUser.Text;
                        workUsers.UserLogin = txtLoginUser.Text;
                        workUsers.UserPassword = txtPasswordUser.Text;
                        Helper.DB.Entry(item).State = EntityState.Modified;
                        Helper.DB.SaveChanges();
                        DataGridUsers.UnselectAllCells();
                        MessageBox.Show("БД успешно обновлена");
                    }
                    catch
                    {
                        MessageBox.Show("С обновлением БД проблемы");
                    }

                }

            }
            ShowUsers();
        }


        /// <summary>
        /// Событие добавления нового пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptUser_Click(object sender, RoutedEventArgs e)
        {
            txtUserId.Text = workUsers.UserId.ToString();
            workUsers.UserRoleId = (int)CbRoleUser.SelectedValue;
            workUsers.UserFullName = txtFioUser.Text;
            workUsers.UserLogin = txtLoginUser.Text;
            workUsers.UserPassword = txtPasswordUser.Text;

            try
            {
                Helper.DB.Users.Add(workUsers);
                Helper.DB.SaveChanges();
                MessageBox.Show("БД успешно обновлена");

            }
            catch
            {
                MessageBox.Show("С обновлением БД проблемы");
            }

            ShowUsers();
        }
    }
}
