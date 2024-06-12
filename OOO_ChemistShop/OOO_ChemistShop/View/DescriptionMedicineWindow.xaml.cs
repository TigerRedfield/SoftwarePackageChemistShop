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
using static System.Net.Mime.MediaTypeNames;

namespace OOO_ChemistShop.View
{
    /// <summary>
    /// Логика взаимодействия для DescriptionMedicineWindow.xaml
    /// </summary>
    public partial class DescriptionMedicineWindow : Window
    {
        Model.Medicine medicine;	//Товар, с которым сейчас работают
        public DescriptionMedicineWindow()
        {
            InitializeComponent();
        }

        /// Конструктор окна с параметром - при редактировании товара
        /// <param name="productSelected">Переданный товар</param>
        public DescriptionMedicineWindow(Classes.ProductExtended productSelected)
        {
            InitializeComponent();
            //Отобразить фото
            imagePhoto.Source = new BitmapImage(new
                                Uri(productSelected.ProductPhotoPath, UriKind.RelativeOrAbsolute));
            //Информация о продукте
            medicine = productSelected.Medicines;
            //Блокировать кнопку изменения фото
            //Все остальные поля товара вывести в элементы интерфейса
            tbName.Text = medicine.MedicineName;
            txtManuf.Text = medicine.Manufacturers.ManufacturerName;
            txtCat.Text = medicine.Categories.CategoryName;
            tbRank.Text = medicine.MedicineRank.ToString() + " баллов";
            txtCost.Text = medicine.MedicineCost.ToString() + " рублей";
            tbCount.Text = medicine.MedicineCount.ToString() + " штук";
            tbMaxDisc.Text = medicine.MedicineDiscountMax.ToString() + "%";
            tbCurDisc.Text = medicine.MedicineDiscount.ToString() + "%";
            txtCountry.Text = medicine.Manufacturers.ManufacturerCountry.ManufacturerCountryName;

            if (medicine.MedicineDescription != null)
            {
                txtDescription.Text = medicine.MedicineDescription.ToString();
            }
            else
            {
                txtDescription.Text = "Описание лекарства временно отсутствует";
            }

            txtDateManuf.Text = medicine.MedicineDateManufacturing.ToString("yyyy");
            txtDateExp.Text = medicine.MedicineExpirationDate.ToString() + " месяцев";

        }


        /// <summary>
        /// Событие кнопки назад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
