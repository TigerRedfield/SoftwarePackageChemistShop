using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OOO_ChemistShopWeb.ViewModels
{
    /// <summary>
    /// Класс-модель для регистрации
    /// </summary>
    public partial class ModelUserRegistration
    {
        public int UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, введите логин!")]
        [Display(Name = "Введите логин:")]
        public string UserLogin { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, введите пароль!")]
        [Display(Name = "Введите пароль:")]
        public string UserPassword { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, подтвердите пароль!")]
        [Display(Name = "Подтвердите пароль:")]
        public string UserPasswordVerif { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, укажите ФИО!")]
        [Display(Name = "Введите ФИО:")]
        public string UserFullName { get; set; }
    }
}