using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OOO_ChemistShopWeb.Models;
namespace OOO_ChemistShopWeb.Classes
{
    public class ProductsInOrder
    {
        /// <summary>
        /// Глобальная модель
        /// </summary>
        public Medicine Medicine { get; set; }

        /// Глобальная переменная
        public int MedicineCountOrder { get; set; } 

        /// <summary>
        /// Общий метод для товаров в заказе
        /// </summary>
        /// <param name="medicine">данные о лекарстве</param>
        /// <param name="medicineCountOrder">количество лекарства в заказе</param>
        public ProductsInOrder(Medicine medicine,int medicineCountOrder) { 
        
            Medicine = medicine;
            MedicineCountOrder = medicineCountOrder;
        }
    }
}