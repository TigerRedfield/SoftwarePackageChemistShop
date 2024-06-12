using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace OOO_ChemistShop.Classes
{
    public class OrderExtended
    {
        //Свойства класса
        public Model.Order Order { get; set; }		//Связь с моделью

        public double TotalSumma { get; set; }	//Сумма всего заказа
        public double TotalDiscount { get; set; }   //Суммарная скидка

         /// <summary>
         /// Свойство класса – суммарная скидка в %
         /// </summary>
        public double TotalDiscountPercent
        { 
            get
            {
                return TotalDiscount * 100 / TotalSumma;
            }
        }

        /// <summary>
        /// Метод расчета суммы заказа по номеру заказа
        /// </summary>
        /// <param name="productsInOrder"></param>
        /// <returns></returns>
        public double CalcTotalSummma(List<Model.MedicineOrder> productsInOrder)
        {
            double total = 0;
            //Перебор всех заказанных товаров
            foreach (var item in productsInOrder)
            {
                if (item.OrderId == Order.OrderId)	//Выделение только товаров текущего заказа
                {
                    total += (int)item.Medicine.MedicineCost * item.ProductCount;
                }
            }
            return total;
        }


        /// <summary>
        /// Метод расчета суммарной скидки заказа по номеру заказа
        /// </summary>
        /// <param name="productsInOrder"></param>
        /// <returns></returns>
        public double CalcTotalDiscount(List<Model.MedicineOrder> productsInOrder)
        {
            double total = 0;
            foreach (var item in productsInOrder)
            {
                if (item.OrderId == Order.OrderId)
                {
                    //Стоимость товара с учетом скидки
                    double discountAmount = (double)item.Medicine.MedicineCost *
                                                               (item.Medicine.MedicineDiscount / 100.0);
                    total += discountAmount * item.ProductCount;
                }
            }
            return total;
        }

        /// <summary>
        /// Цвет заказов в зависимости от статуса
        /// </summary>
        public SolidColorBrush BackgroundColor
        {
            get
            {
                var status = true; //статус завершён

                foreach (var item in Order.Status.Order)
                {
                    if (item.OrderStatusId == 1)
                    {
                        status = false;
                        break;
                    }
                }

                if (!status)
                {
                    return new SolidColorBrush(Color.FromArgb(255, 255, 102, 102));

                }
                else
                {
                    return new SolidColorBrush(Color.FromArgb(255, 84, 236, 208));
                }
            }
        }
    }
}
