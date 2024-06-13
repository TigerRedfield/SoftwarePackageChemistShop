using Microsoft.Win32;
using OOO_ChemistShop.Classes;
using OOO_ChemistShop.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для EditCatalogWindow.xaml
    /// </summary>
    public partial class EditCatalogWindow : Window
    {
        OpenFileDialog dlg = new OpenFileDialog();
        bool isPhoto = false;		//Наличие фото
        string filePath;			//Путь к фото из диалога
        //Путь к папке с фотографиями
        string pathPhoto = System.IO.Directory.GetCurrentDirectory() + @"\Images\";
        string pathWebPhoto = @"C:\Users\Tiger\Desktop\SoftwarePackageChemistShop-master\OOO_ChemistShopWeb\OOO_ChemistShopWeb\Images\";
        Model.Medicine medicine;	//Товар, с которым сейчас работают

        public EditCatalogWindow()
        {
            InitializeComponent();
            tbTitle.Text = "Добавление товара";
            this.DataContext = this;            //Элементы интерфейса связать с данными
            tbDiscr.Clear();
            tbId.IsEnabled = true;		//Блокировать ID
            txtId.Visibility = Visibility.Hidden;
            tbId.Visibility = Visibility.Hidden;
            cbCat.SelectedIndex = 0;
            cbManuf.SelectedIndex = 0;
        }

        /// Конструктор окна с параметром - при редактировании товара
        /// <param name="productSelected">Переданный товар</param>
        public EditCatalogWindow(Classes.ProductExtended productSelected)
        {
            InitializeComponent();
            tbTitle.Text = "Редактирование товара";
            //Отобразить фото
            imagePhoto.Source = new BitmapImage(new
                                Uri(productSelected.ProductPhotoPath, UriKind.RelativeOrAbsolute));
            //Информация о продукте
            medicine = productSelected.Medicines;
            //Блокировать кнопку изменения фото
            butSelectPhoto.Visibility = Visibility.Collapsed;
            txtId.Visibility = Visibility.Visible;
            tbId.Visibility = Visibility.Visible;
            tbId.IsEnabled = false;		//Блокировать ID
            tbId.Text = medicine.MedicineId.ToString();
            //Все остальные поля товара вывести в элементы интерфейса
            tbName.Text = medicine.MedicineName;
            cbManuf.SelectedValue = medicine.MedicineManufacturerId;
            cbCat.SelectedValue = medicine.MedicineCategory;
            tbRank.Text = medicine.MedicineRank.ToString();
            tbCost.Text = medicine.MedicineCost.ToString();
            tbCount.Text = medicine.MedicineCount.ToString();
            tbMaxDisc.Text = medicine.MedicineDiscountMax.ToString();
            tbCurDisc.Text = medicine.MedicineDiscount.ToString();
            tbDiscr.Text = medicine.MedicineDescription;
            DpManuf.Text = medicine.MedicineDateManufacturing.ToString();
            tbExp.Text = medicine.MedicineExpirationDate.ToString();
        }

        /// <summary>
        /// Событие кнопки назад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите вернуться на экран выбора товаров?", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close(); //Закрытие окна
            }
        }

        /// <summary>
        /// Событие заполнения и настройка списков из БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Model.Manufacturers> manufacturers = Helper.DB.Manufacturers.ToList();
            cbManuf.DisplayMemberPath = "ManufacturerName";
            cbManuf.SelectedValuePath = "MedicineManufacturerId";
            cbManuf.ItemsSource = manufacturers.ToList();

            List<Model.Categories> categories = Helper.DB.Categories.ToList();
            cbCat.DisplayMemberPath = "CategoryName";
            cbCat.SelectedValuePath = "CategoryId";
            cbCat.ItemsSource = categories.ToList();
            //Настройка диалога
            dlg.InitialDirectory = @"C:\Users\user\Pictures\";
            dlg.Filter = "Image files (*.jpg)|*.jpg";
        }

        /// <summary>
        /// Событие сохранения в БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butSaveInDB_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();	//Строка с сообщениями
            sb.Clear();
            //Проверка непустых значений для обязательных полей
            if (String.IsNullOrEmpty(tbName.Text))
            { sb.Append("Вы не ввели название." + Environment.NewLine); }
            if (String.IsNullOrEmpty(tbCost.Text))
            { sb.Append("Вы не ввели цену." + Environment.NewLine); }
            if (String.IsNullOrEmpty(tbMaxDisc.Text))
            { sb.Append("Вы не ввели максимальную скидку." + Environment.NewLine); }
            if (String.IsNullOrEmpty(tbCurDisc.Text))
            { sb.Append("Вы не ввели действующую скидку." + Environment.NewLine); }
            if (String.IsNullOrEmpty(tbCount.Text))
            { sb.Append("Вы не ввели количество." + Environment.NewLine); }
            if (sb.Length > 0)			//Есть сообщения об ошибках
            {
                MessageBox.Show(sb.ToString());
            }
            else					//Ошибок нет
            {
                if (tbId.IsEnabled)		//При добавлении
                {
                    medicine = new Model.Medicine();
                    tbId.Text = medicine.MedicineId.ToString();

                    //Есть фото
                    if (isPhoto)
                    {
                        medicine.MedicinePhoto = tbName.Text + ".jpg";     //Для записи в БД
                        string newPath = pathPhoto + medicine.MedicinePhoto;    //Полное имя файла цели
                        string newPathWeb = pathWebPhoto + medicine.MedicinePhoto;    //Полное имя файла цели
                        System.IO.File.Copy(filePath, newPath, true); //Копирование из диалога в целевую
                        System.IO.File.Copy(filePath, newPathWeb, true); //Копирование из диалога в целевую
                    }
                }

                
                //Получаем значения для всех остальных полей при добавлении/редактировании
                medicine.MedicineName = tbName.Text;
                medicine.MedicineManufacturerId = (int)cbManuf.SelectedValue;
                medicine.MedicineCategory = (int)cbCat.SelectedValue;
                medicine.MedicineCost = Convert.ToDouble(tbCost.Text);
                medicine.MedicineRank = Convert.ToDouble(tbRank.Text);
                medicine.MedicineCount = Convert.ToInt32(tbCount.Text);
                medicine.MedicineDiscountMax = Convert.ToInt32(tbMaxDisc.Text);
                medicine.MedicineDiscount = Convert.ToInt32(tbCurDisc.Text);
                medicine.MedicineDescription = tbDiscr.Text;
                medicine.MedicineExpirationDate = Convert.ToInt32(tbExp.Text);
                medicine.MedicineDateManufacturing = Convert.ToDateTime(DpManuf.Text);
                if (tbId.IsEnabled)				//При добавлении
                {
                    Helper.DB.Medicine.Add(medicine);		//Добавить в кэш новую запись
                }
                try
                {
                    Helper.DB.SaveChanges();        //Обновить БД и при добавлении/редактировании
                    (this.Owner as CatalogWindow).ShowMedicines();
                    MessageBox.Show("БД успешно обновлена");

                }
                catch
                {
                    MessageBox.Show("При обновлении БД возникли проблемы");
                }
                this.Close();
            }
        }

        /// <summary>
        /// Событие для выбора фото лекарства
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butSelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            if (dlg.ShowDialog() == true)
            {
                filePath = dlg.FileName;
                //Отобразить фото в компоненте
                imagePhoto.Source = new BitmapImage(new Uri(filePath, UriKind.Absolute));
                isPhoto = true;		 //Есть фото                
            }
        }

        /// <summary>
        /// Разделяемое событие для ввода только цифр и одной точки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoubleNumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {

            bool approvedDecimalPoint = false;

            if (e.Text == ",")
            {
                if (!((TextBox)sender).Text.Contains(","))
                    approvedDecimalPoint = true;
            }
            if (!(char.IsDigit(e.Text, e.Text.Length - 1) || approvedDecimalPoint))
                e.Handled = true;
        }

        /// <summary>
        /// Разделяемое событие на запрет ввод пробела 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }


        /// <summary>
        /// Разделяемое событие для ввода только цифр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
                e.Handled = true;
        }
    }
}
