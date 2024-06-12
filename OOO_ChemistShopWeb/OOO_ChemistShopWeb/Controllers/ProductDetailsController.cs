using OOO_ChemistShopWeb.Classes;
using OOO_ChemistShopWeb.Models;
using OOO_ChemistShopWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OOO_ChemistShopWeb.Controllers
{
    public class ProductDetailsController : Controller
    {
        /// <summary>
        /// Запрос на получение данных о лекарстве
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Product(int id = 0)
        {
            Models.Medicine medicine = Helper.db.Medicine.Find(id);

            if(medicine == null)
            {
                return HttpNotFound();
            }

            return View(medicine);
        }
    }
}