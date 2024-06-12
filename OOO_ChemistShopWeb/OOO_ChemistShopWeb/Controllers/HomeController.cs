using OOO_ChemistShopWeb.Classes;
using OOO_ChemistShopWeb.Models;
using OOO_ChemistShopWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using static OOO_ChemistShopWeb.ViewModels.ModelMedicines;

namespace OOO_ChemistShopWeb.Controllers
{
    public class HomeController : Controller
    {

        /// <summary>
        /// Контроллер для показа данных о лекарствах на главной странице
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            var modelMedicines = Helper.db.Medicine.ToList();

            ModelMedicines.MedicinesViewModelStart ivm = new ModelMedicines.MedicinesViewModelStart { Medicines = modelMedicines };

            return View(ivm);
        }

        /// <summary>
        /// Контроллер для показа данных в каталоге
        /// </summary>
        /// <param name="category">Категория товаров</param>
        /// <param name="page">постраничная навигация товаров</param>
        /// <returns></returns>
        public ActionResult ProductsView(int? category = 0, int? page = 1)
        {
            int pageSize = 3; // количество объектов на страницу

            List<Categories> categories = new List<Categories>();
            List<Medicine> medicines = new List<Medicine>();
            IEnumerable<Medicine> modelMedicines = medicines;
            IEnumerable<Categories> modelCategories = categories;
            
            if (category != 0)
            {
                Session["CategorySelected"] = Helper.db.Categories.Where(m => m.CategoryId == category).Select(x => x.CategoryName).FirstOrDefault();
                Session["NumberSelected"] = Helper.db.Medicine.Where(m => m.MedicineCategory == category).Select(m => m.MedicineCategory).FirstOrDefault();
                modelMedicines = Helper.db.Medicine.Where(m => m.MedicineCategory == category).OrderBy(x => x.MedicineId).Skip(((int)page - 1) * pageSize).Take(pageSize);
                modelCategories = Helper.db.Categories.ToList();
                PageInfo pageInfo = new PageInfo { PageNumber = (int)page, PageSize = pageSize, TotalItems = Helper.db.Medicine.Where(x => x.MedicineCategory == category).Count() };
                ModelMedicines.MedicinesViewModel ivm = new ModelMedicines.MedicinesViewModel { PageInfo = pageInfo, Medicines = modelMedicines, Categories = modelCategories };
                return View(ivm);
            }
            else 
            {
                Session["CategorySelected"] = "Все лекарства";
                Session["NumberSelected"] = 0;
                modelMedicines = Helper.db.Medicine.OrderBy(x => x.MedicineId).Skip(((int)page - 1) * pageSize).Take(pageSize);
                modelCategories = Helper.db.Categories.ToList();
                PageInfo pageInfo = new PageInfo { PageNumber = (int)page, PageSize = pageSize, TotalItems = Helper.db.Medicine.Count() };
                ModelMedicines.MedicinesViewModel ivm = new ModelMedicines.MedicinesViewModel { PageInfo = pageInfo, Medicines = modelMedicines, Categories = modelCategories };
                return View(ivm);
            }
        }

        /// <summary>
        /// Контроллер для поиска лекарств
        /// </summary>
        /// <param name="searchProduct">строка поиска товаров</param>
        /// <param name="page">постраничная навигация товаров</param>
        /// <returns></returns>
        [HttpPost, ActionName("ProductsView")]
        public ActionResult ProductFind(string searchProduct, int page = 1)
        {

            List<Categories> categories = new List<Categories>();
            List<Medicine> medicines = new List<Medicine>();
            IEnumerable<Medicine> modelMedicines = medicines;
            IEnumerable<Categories> modelCategories = categories;

            int pageSize = 3;

            if (!String.IsNullOrEmpty(searchProduct))
            {
                @Session["CategorySelected"] = null;
                modelMedicines = Helper.db.Medicine.Where(m => m.MedicineName.ToUpper().Contains(searchProduct.ToUpper())).ToList();
                modelCategories = Helper.db.Categories.ToList();
                ModelMedicines.MedicinesViewModel ivm = new ModelMedicines.MedicinesViewModel { Medicines = modelMedicines, Categories = modelCategories };
                return View(ivm);
            }
            else
            {
                Session["CategorySelected"] = "Все лекарства";
                modelMedicines = Helper.db.Medicine.OrderBy(x => x.MedicineId).Skip((page - 1) * pageSize).Take(pageSize);
                modelCategories = Helper.db.Categories.ToList();
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Helper.db.Medicine.Count() };
                ModelMedicines.MedicinesViewModel ivm = new ModelMedicines.MedicinesViewModel { PageInfo = pageInfo, Medicines = modelMedicines, Categories = modelCategories };
                return View(ivm);
            }


        }
    }
}