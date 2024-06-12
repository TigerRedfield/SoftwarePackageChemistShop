using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OOO_ChemistShopWeb.Controllers
{
    public class OrderViewsController : Controller
    {

        /// <summary>
        /// Показ страницы о том, что заказ удался
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderSuccess()
        {
            return View();
        }

        /// <summary>
        /// Показ страницы о том, что заказ не удался
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderFailure()
        {
            return View();
        }
    }
}