using OOO_ChemistShopWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OOO_ChemistShopWeb.Classes;
using System.EnterpriseServices.CompensatingResourceManager;
using OOO_ChemistShopWeb.Models;

namespace OOO_ChemistShopWeb.Controllers
{
    public class ShoppingCartViewsController : Controller
    {
        private readonly string strCart = "Cart"; //Строка для хранения ключа к сессии

        /// <summary>
        /// Показ страницы корзины
        /// </summary>
        /// <returns></returns>
        public ActionResult ShoppingCart()
        {
            return View();
        }

        /// <summary>
        /// Метод для определения что лекарство существует в корзине
        /// </summary>
        /// <param name="id">ID товара</param>
        /// <returns></returns>
        private int IsExistingCheck(int? id)
        {
            List<ProductsInOrder> listProductsOrder = (List<ProductsInOrder>)Session[strCart];
            for (int i = 0; i < listProductsOrder.Count; i++)
            {
                if (listProductsOrder[i].Medicine.MedicineId == id) return i;
            }
            return -1;
        }

        /// <summary>
        /// Запрос для создания лекарства в списке заказов в корзине
        /// </summary>
        /// <param name="id">ID товара</param>
        /// <returns></returns>
        public ActionResult OrderList(int? id)
        {
          
            if (Session[strCart] == null)
            {
                List<ProductsInOrder> listProductsOrder = new List<ProductsInOrder>();
                {
                    new ProductsInOrder(Helper.db.Medicine.Find(id), 1);
                };
                listProductsOrder.Add(new ProductsInOrder(Helper.db.Medicine.Find(id), 1));
                Session[strCart] = listProductsOrder;
            }
            else
            {
                List <ProductsInOrder> listProductsOrder = (List<ProductsInOrder>)Session[strCart];
                int check = IsExistingCheck(id);
                if(check == -1)
                {
                    listProductsOrder.Add(new ProductsInOrder(Helper.db.Medicine.Find(id), 1));
                    Session["Message"] = null;
                }
                else
                {
                    if (listProductsOrder[check].MedicineCountOrder >= listProductsOrder[check].Medicine.MedicineCount)
                    {
                        Session["Message"] = "Товара " + listProductsOrder[check].Medicine.MedicineName + " на складе сейчас " + listProductsOrder[check].Medicine.MedicineCount + " штук, поэтому добавить больше не можем";
                    }
                    else
                    {
                        Session["Message"] = null;
                        listProductsOrder[check].MedicineCountOrder++;
                    }
                }
                Session[strCart] = listProductsOrder;
               
            }
            return RedirectToRoute(new { controller = "ShoppingCartViews", action = "ShoppingCart" });
        }

        /// <summary>
        /// Запрос на удаление конкретного товара из корзины
        /// </summary>
        /// <param name="id">ID товара</param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            int check = IsExistingCheck(id);
            List<ProductsInOrder> lstProductsOrder = (List<ProductsInOrder>)Session[strCart];
            if(lstProductsOrder.Count != 1)
            {
                lstProductsOrder.RemoveAt(check);
                Session[strCart] = lstProductsOrder;
            }
            else
            {
                Session[strCart] = null;
            }
            return RedirectToRoute(new { controller = "ShoppingCartViews", action = "ShoppingCart" });
        }

        /// <summary>
        /// Запрос на обновление и на перерасчёт данных заказа в корзине
        /// </summary>
        /// <param name="frc">список товаров в заказе</param>
        /// <returns></returns>
        public ActionResult UpdateOrder(FormCollection frc)
        {
            var Message = "";
            string[] counts = frc.GetValues("quantity");
            List<ProductsInOrder> lstProductsOrder = (List<ProductsInOrder>)Session[strCart];
            for (int i = 0; i < lstProductsOrder.Count; i++)
            {
                if (Convert.ToInt32(counts[i]) > lstProductsOrder[i].Medicine.MedicineCount)
                {
                    var messageCounts = lstProductsOrder.FirstOrDefault(x => x.Medicine.MedicineId == lstProductsOrder[i].Medicine.MedicineId);
                    lstProductsOrder[i].MedicineCountOrder = lstProductsOrder[i].Medicine.MedicineCount;
                    Message += ("Товара " + messageCounts.Medicine.MedicineName + " на складе сейчас " + messageCounts.Medicine.MedicineCount.ToString() + " штук, поэтому добавить больше не можем\n").Replace("\n", "<br>");
   
                }
                else
                {
                    lstProductsOrder[i].MedicineCountOrder = Convert.ToInt32(counts[i]);
                }
            }
            Session[strCart] = lstProductsOrder;
            ViewData["Msg"] = Message;
            Session["Message"] = ViewData["Msg"];
            return RedirectToRoute(new { controller = "ShoppingCartViews", action = "ShoppingCart" });
        }

        /// <summary>
        /// Запрос на удаление всех товаров из корзины
        /// </summary>
        /// <returns></returns>
        public ActionResult RemoveAllShoppingCart()
        {
            Session[strCart] = null;
            return RedirectToRoute(new { controller = "ShoppingCartViews", action = "ShoppingCart" });
        }

        /// <summary>
        /// Показ страницы оформления заказа и данных об адресах пункта выдачи
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckOut()
        {
            ViewBag.PointsId = Helper.db.Point.ToList();
            return View();
        }

        /// <summary>
        /// Запрос на оформление заказа
        /// </summary>
        /// <param name="orders">Модель для списка товаров в заказе</param>
        /// <returns></returns>
        public ActionResult ProcessOrder(Order orders)
        {
            ViewBag.PointsId = Helper.db.Point.ToList();
            List<ProductsInOrder> lstOrder = (List<ProductsInOrder>)Session[strCart];

            var order = new Order
            {
                OrderPointId = orders.OrderPointId,
                OrderClient = Session["UserFIO"].ToString(),
                DateOrder = DateTime.Now,
                OrderCode = new Random().Next(100, 1000),
                OrderStatusId = 1,
                DateDelivery = DateTime.Now.AddDays(3)
            };
            foreach (var item in lstOrder)
            {
                Medicine medicines = Helper.db.Medicine.Find(item.Medicine.MedicineId);
                if (medicines.MedicineCount != 0)
                {
                    medicines.MedicineCount -= item.MedicineCountOrder;
                }
                else
                {
                    return RedirectToRoute(new { controller = "OrderViews", action = "OrderFailure" });
                }
            }
            Helper.db.Order.Add(order);
            try
            {
                Helper.db.SaveChanges();
                foreach (var item in lstOrder)
                {
                    MedicineOrder orderMedicine = new Models.MedicineOrder
                    {
                        OrderId = order.OrderId,
                        MedicineId = item.Medicine.MedicineId,
                        ProductCount = item.MedicineCountOrder,
                    };
                    Helper.db.MedicineOrder.Add(orderMedicine);
                    Helper.db.SaveChanges();
                    Session["OrderId"] = order.OrderId;
                    Session[strCart] = null;
                }
                return RedirectToRoute(new { controller = "OrderViews", action = "OrderSuccess" });
            }
            catch
            {
                return RedirectToRoute(new { controller = "OrderViews", action = "OrderFailure" });
            }
        }

    }
}