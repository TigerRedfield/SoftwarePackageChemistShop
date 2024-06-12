using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace OOO_ChemistShopWeb.ViewModels
{
    public class ModelMedicines
    {
        /// <summary>
        /// Класс-модель для показа лекарств на главной странице
        /// </summary>
        public class MedicinesViewModelStart
        {
            public IEnumerable<Models.Medicine> Medicines { get; set; }
        }

        /// <summary>
        /// Класс для пагинации
        /// </summary>
        public class PageInfo
        {
            public int PageNumber { get; set; } // номер текущей страницы
            public int PageSize { get; set; } // кол-во объектов на странице
            public int TotalItems { get; set; } // всего объектов
            public int TotalPages  // всего страниц
            {
                get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
            }
        }

        /// <summary>
        /// Класс-модель для показа лекарств в каталоге
        /// </summary>
        public class MedicinesViewModel
        {
            public IEnumerable<Models.Medicine> Medicines { get; set; }

            public IEnumerable<Models.Categories> Categories { get; set; }
            public PageInfo PageInfo { get; set; }
        }

    }
}