using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using OOO_ChemistShopWeb.Models;
using static OOO_ChemistShopWeb.ViewModels.ModelMedicines;

namespace OOO_ChemistShopWeb.Classes
{
    public class Helper
    {
        /// <summary>
        /// статический класс для взаимодействия с БД 
        /// </summary>
        public static OOO_DBChemistShop db = new OOO_DBChemistShop();

    }
}