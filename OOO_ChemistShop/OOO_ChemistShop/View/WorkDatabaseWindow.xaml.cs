using Microsoft.Office.Interop.Word;
using OOO_ChemistShop.Classes;
using OOO_ChemistShop.Model;
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
    /// Логика взаимодействия для WorkDatabaseWindow.xaml
    /// </summary>
    public partial class WorkDatabaseWindow : System.Windows.Window
    {
        /// <summary>
        /// Создание классов для работы с данными БД
        /// </summary>
        Model.Categories categoryWork = new Model.Categories();
        Model.ManufacturerCountry countriesWork = new ManufacturerCountry();
        Model.Manufacturers manufacturersWork = new Manufacturers();
        Model.Point pointWork = new Model.Point();
        public WorkDatabaseWindow()
        {
            InitializeComponent();
            this.DataContext = this; //Элементы интерфейса связать с данными
            
        }

        /// <summary>
        /// Событие кнопки закрытия окна
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
        /// Функция обновления списка категорий
        /// </summary>
        private void ShowCategory()
        {
            List<Model.Categories> categories = Helper.DB.Categories.ToList();
            DataGridCateg.ItemsSource = categories;

            txtCatId.Text = "";
            txtCatName.Text = "";

            DataGridCateg.UnselectAllCells();
        }

        /// <summary>
        /// Функция обновления списка стран производителей
        /// </summary>
        private void ShowCountry()
        {
            List<Model.ManufacturerCountry> countries = Helper.DB.ManufacturerCountry.ToList();
            DataGridCountries.ItemsSource = countries;
            CbManufCountry.ItemsSource = Helper.DB.ManufacturerCountry.ToList();

            txtCountryId.Text = "";
            txtCountryName.Text = "";

            DataGridCountries.UnselectAllCells();


        }

        /// <summary>
        /// Функция обновления списка производителей
        /// </summary>
        private void ShowManuf()
        {
            List<Model.Manufacturers> manufacturers = Helper.DB.Manufacturers.ToList();
            DataGridManufacturers.ItemsSource = manufacturers;
            CbManufCountry.SelectedIndex = -1;
            txtManufName.Text = "";
            DataGridManufacturers.UnselectAllCells();
        }

        /// <summary>
        /// Функция обновления списка адресов
        /// </summary>
        private void ShowPoint()
        {
            List<Model.Point> points = Helper.DB.Point.ToList();
            DataGridPoints.ItemsSource = points;

            txtPointId.Text = "";
            txtPointName.Text = "";

            DataGridPoints.UnselectAllCells();
        }



        /// <summary>
        /// Событие добавление категории
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptCateg_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            if (String.IsNullOrEmpty(txtCatName.Text))
            {
                sb.AppendLine("Не введено название категории!");
            }

            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString(), "Внимание");
            }
            else
            {
                categoryWork = Helper.DB.Categories.FirstOrDefault(x => x.CategoryName == txtCatName.Text);
                if (categoryWork == null)
                {
                    txtCatId.Text = categoryWork.CategoryId.ToString();
                    categoryWork.CategoryName = txtCatName.Text;

                    try
                    {
                        Helper.DB.Categories.Add(categoryWork);
                        Helper.DB.SaveChanges();
                        MessageBox.Show("БД успешно обновлена");
                
                    }
                    catch
                    {
                        MessageBox.Show("С обновлением БД проблемы");
                    }
                    ShowCategory();
                }
                else
                {
                    MessageBox.Show("Категория с таким названием уже есть в БД!");
                }
            }
        }

        /// <summary>
        /// Событие редактирования категории
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditCateg_Click(object sender, RoutedEventArgs e)
        {
            if (txtCatId.Text == "")
            {
                MessageBox.Show("Не выбрана строка для редактирования!", "Внимание!");
                return;
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                if (String.IsNullOrEmpty(txtCatName.Text))
                {
                    sb.AppendLine("Не введено название категории!");
                }

                if (sb.Length > 0)
                {
                    MessageBox.Show(sb.ToString(), "Внимание");
                }
                else
                {
                    int ID = int.Parse(txtCatId.Text);

                    var item = Helper.DB.Categories.SingleOrDefault(x => x.CategoryId == ID);

                    if (item != null)
                    {
                        categoryWork = Helper.DB.Categories.FirstOrDefault(x => x.CategoryName == txtCatName.Text);
                        if (categoryWork == null)
                        {
                            item.CategoryName = txtCatName.Text.Trim();

                            try
                            {
                                Helper.DB.Entry(item).State = EntityState.Modified;
                                Helper.DB.SaveChanges();
                                DataGridCateg.UnselectAllCells();
                                MessageBox.Show("БД успешно обновлена");
                            }
                            catch
                            {
                                MessageBox.Show("С обновлением БД проблемы");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Категория с таким названием уже есть в БД!");
                        }

                    }

                }
            }
            ShowCategory();
            
        }

        /// <summary>
        /// Событие удаления категории
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteCateg_Click(object sender, RoutedEventArgs e)
        {
            if (txtCatId.Text == "")
            {
                MessageBox.Show("Не выбрана строка для удаления!", "Внимание!");
                return;
            }
            else
            {
                if (MessageBox.Show("Вы точно хотите удалить эту категорию?", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    int ID = int.Parse(txtCatId.Text);

                    var item = Helper.DB.Categories.SingleOrDefault(x => x.CategoryId == ID);

                    if (item != null)
                    {
                        Model.Medicine medicine = Helper.DB.Medicine.FirstOrDefault(x => x.MedicineCategory == ID);
                        if(medicine == null)
                        {
                            try
                            {
                                Helper.DB.Categories.Remove(item);
                                Helper.DB.SaveChanges();
                                DataGridCateg.UnselectAllCells();
                                MessageBox.Show("БД успешно обновлена");
                            }
                            catch
                            {
                                MessageBox.Show("С обновлением БД проблемы");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Удалить нельзя, т.к. лекарство есть в категории!");
                            DataGridCateg.UnselectAllCells();
                            return;
                        }

                    }
                }
                else
                {
                    DataGridCateg.UnselectAllCells();
                }

            }
            ShowCategory();
        }

        /// <summary>
        /// Событие на отмену выделения позиции категории
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelCateg_Click(object sender, RoutedEventArgs e)
        {
            ShowCategory();
            DataGridCateg.UnselectAllCells();
        }

        /// <summary>
        /// Событие добавление адреса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptPoint_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            if (String.IsNullOrEmpty(txtPointName.Text))
            {
                sb.AppendLine("Не введён адрес пункта выдачи!");
            }

            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString(), "Внимание");
            }
            else
            {
                pointWork = Helper.DB.Point.FirstOrDefault(x => x.PointAddress == txtCatName.Text);
                if (pointWork == null)
                {
                    txtPointId.Text = pointWork.PointId.ToString();
                    pointWork.PointAddress = txtPointName.Text;

                    try
                    {
                        Helper.DB.Point.Add(pointWork);
                        Helper.DB.SaveChanges();
                        MessageBox.Show("БД успешно обновлена");

                    }
                    catch
                    {
                        MessageBox.Show("С обновлением БД проблемы");
                    }

                    ShowPoint();
                }
                else
                {
                    MessageBox.Show("Адрес пункта выдачи с таким названием уже есть в БД!");
                }
            }
        }

        /// <summary>
        /// Событие редактирования адреса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditPoint_Click(object sender, RoutedEventArgs e)
        {
            if (txtPointId.Text == "")
            {
                MessageBox.Show("Не выбрана строка для редактирования!", "Внимание!");
                return;
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                if (String.IsNullOrEmpty(txtPointName.Text))
                {
                    sb.AppendLine("Не введён адрес пункта выдачи!");
                }

                if (sb.Length > 0)
                {
                    MessageBox.Show(sb.ToString(), "Внимание");
                }
                else
                {
                    int ID = int.Parse(txtPointId.Text);

                    var item = Helper.DB.Point.SingleOrDefault(x => x.PointId == ID);

                    if (item != null)
                    {
                        pointWork = Helper.DB.Point.FirstOrDefault(x => x.PointAddress == txtPointName.Text);
                        if (pointWork == null)
                        {
                            item.PointAddress = txtPointName.Text.Trim();
                            try
                            {
                                Helper.DB.Entry(item).State = EntityState.Modified;
                                Helper.DB.SaveChanges();
                                DataGridPoints.UnselectAllCells();
                                MessageBox.Show("БД успешно обновлена");
                            }
                            catch
                            {
                                MessageBox.Show("С обновлением БД проблемы");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Адрес пункта выдачи с таким названием уже есть в БД!");
                        }
                    }   
                }
            }
            ShowPoint();
        }


        /// <summary>
        /// Событие удаления адреса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeletePoint_Click(object sender, RoutedEventArgs e)
        {
            if (txtPointId.Text == "")
            {
                MessageBox.Show("Не выбрана строка для удаления!", "Внимание!");
                return;
            }
            else
            {
                if (MessageBox.Show("Вы точно хотите удалить этот адрес?", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    int ID = int.Parse(txtPointId.Text);

                    var item = Helper.DB.Point.SingleOrDefault(x => x.PointId == ID);

                    if (item != null)
                    {
                        Model.Order order = Helper.DB.Order.FirstOrDefault(x => x.OrderPointId == ID);
                        if (order == null)
                        {
                            try
                            {
                                Helper.DB.Point.Remove(item);
                                Helper.DB.SaveChanges();
                                DataGridPoints.UnselectAllCells();
                                MessageBox.Show("БД успешно обновлена");
                            }
                            catch
                            {
                                MessageBox.Show("С обновлением БД проблемы");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Удалить нельзя, т.к. адрес есть в заказе!");
                            DataGridPoints.UnselectAllCells();
                            return;
                        }
                    }
                }
                else
                {
                    DataGridPoints.UnselectAllCells();
                }

            }
            ShowPoint();
        }

        /// <summary>
        /// Событие на отмену выделения позиции адреса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelPoint_Click(object sender, RoutedEventArgs e)
        {
            ShowPoint();
            DataGridPoints.UnselectAllCells();
        }

        /// <summary>
        /// Событие добавление страны
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptManufCountry_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();


            if (String.IsNullOrEmpty(txtCountryName.Text))
            {
                sb.AppendLine("Не введена страна производителей!");
            }

            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString(), "Внимание");
            }
            else
            {
                countriesWork = Helper.DB.ManufacturerCountry.FirstOrDefault(x => x.ManufacturerCountryName == txtCountryName.Text);
                if (countriesWork == null)
                {
                    txtCountryId.Text = countriesWork.ManufacturerCountryId.ToString();
                    countriesWork.ManufacturerCountryName = txtCountryName.Text;

                    try
                    {
                        Helper.DB.ManufacturerCountry.Add(countriesWork);
                        Helper.DB.SaveChanges();
                        MessageBox.Show("БД успешно обновлена");

                    }
                    catch
                    {
                        MessageBox.Show("С обновлением БД проблемы");
                    }
                    ShowCountry();
                }
                else
                {
                    MessageBox.Show("Страна производителя с таким названием уже есть в БД!");
                }

            }
        }

        /// <summary>
        /// Событие редактирования страны
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditManufCountry_Click(object sender, RoutedEventArgs e)
        {
            if (txtCountryId.Text == "")
            {
                MessageBox.Show("Не выбрана строка для редактирования!", "Внимание!");
                return;
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                if (String.IsNullOrEmpty(txtCountryName.Text))
                {
                    sb.AppendLine("Не введена страна производителей!");
                }

                if (sb.Length > 0)
                {
                    MessageBox.Show(sb.ToString(), "Внимание");
                }
                else
                {
                    int ID = int.Parse(txtCountryId.Text);

                    var item = Helper.DB.ManufacturerCountry.SingleOrDefault(x => x.ManufacturerCountryId == ID);

                    if (item != null)
                    {
                        countriesWork = Helper.DB.ManufacturerCountry.FirstOrDefault(x => x.ManufacturerCountryName == txtCountryName.Text);
                        if (countriesWork == null)
                        {
                            item.ManufacturerCountryName = txtCountryName.Text.Trim();
                            try
                            {
                                Helper.DB.Entry(item).State = EntityState.Modified;
                                Helper.DB.SaveChanges();
                                DataGridCountries.UnselectAllCells();
                                MessageBox.Show("БД успешно обновлена");
                            }
                            catch
                            {
                                MessageBox.Show("С обновлением БД проблемы");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Страна производителя с таким названием уже есть в БД!");
                        }

                    }

                }
            }
            ShowCountry();
          
        }


        /// <summary>
        /// Событие удаления страны
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteManufCountry_Click(object sender, RoutedEventArgs e)
        {
            if (txtCountryId.Text == "")
            {
                MessageBox.Show("Не выбрана строка для удаления!", "Внимание!");
                return;
            }
            else
            {
                if (MessageBox.Show("Вы точно хотите удалить эту страну?", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    int ID = int.Parse(txtCountryId.Text);

                    var item = Helper.DB.ManufacturerCountry.SingleOrDefault(x => x.ManufacturerCountryId == ID);

                    if (item != null)
                    {
                        Model.Manufacturers manufacturers = Helper.DB.Manufacturers.FirstOrDefault(x => x.ManufacturerCountryId == ID);
                        if (manufacturers == null)
                        {
                            try
                            {
                                Helper.DB.ManufacturerCountry.Remove(item);
                                Helper.DB.SaveChanges();
                                DataGridCountries.UnselectAllCells();
                                MessageBox.Show("БД успешно обновлена");
                            }
                            catch
                            {
                                MessageBox.Show("С обновлением БД проблемы");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Удалить нельзя, т.к. страна есть в списке производителей !");
                            DataGridCountries.UnselectAllCells();
                            return;
                        }
                    }
                }
                else
                {
                    DataGridCountries.UnselectAllCells();
                }


            }
            ShowCountry();
        }

        /// <summary>
        /// Событие на отмену выделения позиции страны
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelManufCountry_Click(object sender, RoutedEventArgs e)
        {
            ShowCountry();
            DataGridCountries.UnselectAllCells();
        }

        /// <summary>
        /// Событие добавление производителя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptManuf_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            if (CbManufCountry.SelectedIndex == -1)
            {
                sb.AppendLine("Не выбрана страна производителя!");
            }
            if (String.IsNullOrEmpty(txtManufName.Text))
            {
                sb.AppendLine("Не введен производитель!");
            }

            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString(), "Внимание");
            }
            else
            {
                manufacturersWork = Helper.DB.Manufacturers.FirstOrDefault(x => x.ManufacturerName == txtManufName.Text);
                if (manufacturersWork == null)
                {
                    txtManufacturerId.Text = manufacturersWork.MedicineManufacturerId.ToString();
                    manufacturersWork.ManufacturerCountryId = (int)CbManufCountry.SelectedValue;
                    manufacturersWork.ManufacturerName = txtManufName.Text;

                    try
                    {
                        Helper.DB.Manufacturers.Add(manufacturersWork);
                        Helper.DB.SaveChanges();
                        MessageBox.Show("БД успешно обновлена");

                    }
                    catch
                    {
                        MessageBox.Show("С обновлением БД проблемы");
                    }

                    ShowManuf();
                }
                else
                {
                    MessageBox.Show("Производитель с таким названием уже есть в БД!");
                }
            }
        }

        /// <summary>
        /// Событие редактирования производителя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditManuf_Click(object sender, RoutedEventArgs e)
        {
            if (txtManufacturerId.Text == "")
            {
                MessageBox.Show("Не выбрана строка для редактирования!", "Внимание!");
                return;
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                if (CbManufCountry.SelectedIndex == -1)
                {
                    sb.AppendLine("Не выбрана страна производителя!");
                }
                if (String.IsNullOrEmpty(txtManufName.Text))
                {
                    sb.AppendLine("Не введен производитель!");
                }

                if (sb.Length > 0)
                {
                    MessageBox.Show(sb.ToString(), "Внимание");
                }
                else
                {
                    int ID = int.Parse(txtManufacturerId.Text);

                    var item = Helper.DB.Manufacturers.SingleOrDefault(x => x.MedicineManufacturerId == ID);

                    if (item != null)
                    {
                        manufacturersWork = Helper.DB.Manufacturers.FirstOrDefault(x => x.ManufacturerName == txtManufName.Text);
                        if (manufacturersWork == null)
                        {
                            item.ManufacturerCountryId = (int)CbManufCountry.SelectedValue;
                            item.ManufacturerName = txtManufName.Text.Trim();
                            try
                            {
                                Helper.DB.Entry(item).State = EntityState.Modified;
                                Helper.DB.SaveChanges();
                                DataGridManufacturers.UnselectAllCells();
                                MessageBox.Show("БД успешно обновлена");
                            }
                            catch
                            {
                                MessageBox.Show("С обновлением БД проблемы");
                            }
                        }
                        else
                        {
                            manufacturersWork = Helper.DB.Manufacturers.FirstOrDefault(x => x.ManufacturerCountryId != (int)CbManufCountry.SelectedValue);
                            if (manufacturersWork != null)
                            {
                                item.ManufacturerName = Helper.DB.Manufacturers.Where(x => x.MedicineManufacturerId == ID).Select(x => x.ManufacturerName).FirstOrDefault();
                                item.ManufacturerCountryId = (int)CbManufCountry.SelectedValue;
                                try
                                {
                                    Helper.DB.Entry(item).State = EntityState.Modified;
                                    Helper.DB.SaveChanges();
                                    DataGridManufacturers.UnselectAllCells();
                                    MessageBox.Show("БД успешно обновлена");
                                }
                                catch
                                {
                                    MessageBox.Show("С обновлением БД проблемы");
                                }
                            }

                            else
                            {
                                MessageBox.Show("Производитель с таким названием уже есть в БД!");
                            }
                        }

                    }

                }
            }
            ShowManuf();
           
        }


        /// <summary>
        /// Событие удаления производителя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteManuf_Click(object sender, RoutedEventArgs e)
        {
            if (txtManufacturerId.Text == "")
            {
                MessageBox.Show("Не выбрана строка для удаления!", "Внимание!");
                return;
            }
            else
            {
                if(MessageBox.Show("Вы точно хотите удалить данного производителя?", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    int ID = int.Parse(txtManufacturerId.Text);

                    var item = Helper.DB.Manufacturers.SingleOrDefault(x => x.MedicineManufacturerId == ID);

                    if (item != null)
                    {
                        Model.Medicine medicine = Helper.DB.Medicine.FirstOrDefault(x => x.MedicineManufacturerId == ID);
                        if (medicine == null)
                        {
                            try
                            {
                                Helper.DB.Manufacturers.Remove(item);
                                Helper.DB.SaveChanges();
                                DataGridManufacturers.UnselectAllCells();
                                MessageBox.Show("БД успешно обновлена");
                            }
                            catch
                            {
                                MessageBox.Show("С обновлением БД проблемы");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Удалить нельзя, т.к. у лекарств есть производитель !");
                            DataGridManufacturers.UnselectAllCells();
                            return;
                        }
                    }
                }
                else
                {
                    DataGridManufacturers.UnselectAllCells();
                }


            }
            ShowManuf();
        }

       /// <summary>
       /// Событие на отмену выделения позиции производителя
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void CancelManuf_Click(object sender, RoutedEventArgs e)
        {
            ShowManuf();
            DataGridManufacturers.UnselectAllCells();
        }

        /// <summary>
        /// Подготовка данных при загрузке окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Model.ManufacturerCountry> countries = new List<Model.ManufacturerCountry>();

            countries.AddRange(Helper.DB.ManufacturerCountry.ToList());

            CbManufCountry.DisplayMemberPath = "ManufacturerCountryName";
            CbManufCountry.SelectedValuePath = "ManufacturerCountryId";
            CbManufCountry.ItemsSource = countries;

        }
    }
}
