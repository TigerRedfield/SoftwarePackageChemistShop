using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OOO_ChemistShopWeb.ViewModels
{
    /// <summary>
    /// Класс-модель для логина
    /// </summary>
    public partial class ModelUserLogin
    {
        public int UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, введите логин!")]
        [Display(Name = "Введите логин:")]
        public string UserLogin { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, введите пароль!")]
        [Display(Name = "Введите пароль:")]
        public string UserPassword { get; set; }
    }
}