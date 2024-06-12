using OOO_ChemistShop.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOO_ChemistShop.Classes
{
    public class ProductExtended
    {
        /// <summary>
        /// Расширенный класс
        /// </summary>
        /// <param name="medicines"></param>
        public ProductExtended(Model.Medicine medicines)
        {
            this.Medicines = medicines;
        }
        public Model.Medicine Medicines { get; set; }

        /// <summary>
        /// строка с заглушкой или фото+папка
        /// </summary>
        public string ProductPhotoPath
        {
            get
            {
                string temp = Environment.CurrentDirectory + "/Images/" + Medicines.MedicinePhoto;
                if(!File.Exists(temp))
                {
                    temp = "/Resources/Pictures.png";
                }
                return temp;
            }
        }

        private double productCostWithDiscount;

        /// <summary>
        /// цена со скидкой
        /// </summary>
        public double ProductCostWithDiscount
        {
            get
            {
                double temp = 0.0;
                double discount = (double)Medicines.MedicineCost * (double)Medicines.MedicineDiscount / 100;
                temp = (double)Medicines.MedicineCost - discount;
                return temp;
            }
            set
            {
                productCostWithDiscount = value;
            }
        }
    }
}
