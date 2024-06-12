using OOO_ChemistShopWeb.Classes;
using OOO_ChemistShopWeb.Models;
using OOO_ChemistShopWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OOO_ChemistShopWeb.Controllers
{
    public class AccountController : Controller
    {

        /// <summary>
        /// Отображение страницы авторизации
        /// </summary>
        /// <returns></returns>
        public ActionResult UserLogin()
        {
            ViewModels.ModelUserLogin userLogin = new ViewModels.ModelUserLogin();  
            return View(userLogin);
        }

        /// <summary>
        /// Запрос на авторизацию пользователя
        /// </summary>
        /// <param name="userSet">Взятие данных пользователя</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserLogin(ViewModels.ModelUserLogin userSet)
        {
            if (ModelState.IsValid)
            {
                if (Helper.db.Users.Where(m => m.UserLogin == userSet.UserLogin && m.UserPassword == userSet.UserPassword && m.UserRoleId == 1).FirstOrDefault() != null)
                {

                    Session["UserId"] = Helper.db.Users.Single(x => x.UserLogin == userSet.UserLogin).UserId;
                    Session["UserLogin"] = userSet.UserLogin;
                    Session["UserFIO"] = Helper.db.Users.Where(u => u.UserLogin == userSet.UserLogin).Select(u => u.UserFullName).FirstOrDefault();
                    Session.Timeout = 1440;
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
                else
                {
                    ModelState.AddModelError("Ошибка", "Такого пользователя в базе клиентов не существует");
                    return View(userSet);

                }
            }
                return View(userSet);
        }

        /// <summary>
        /// Отображение страницы регистрации
        /// </summary>
        /// <returns></returns>
        public ActionResult UserRegistration()
        {
            ViewModels.ModelUserRegistration NewUser = new ViewModels.ModelUserRegistration();

            return View(NewUser);
        }

        /// <summary>
        /// Запрос на регистрацию пользователя
        /// </summary>
        /// <param name="NewUser">взятие данных пользователя</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserRegistration(ViewModels.ModelUserRegistration NewUser)
        {
            if (ModelState.IsValid)
            {
                if(NewUser.UserPassword == NewUser.UserPasswordVerif)
                {
                    if (!Helper.db.Users.Any(m => m.UserLogin == NewUser.UserLogin))
                    {
                        Models.Users dbNewUser = new Models.Users
                        {
                            UserLogin = NewUser.UserLogin,
                            UserPassword = NewUser.UserPassword,
                            UserFullName = NewUser.UserFullName,
                            UserRoleId = 1
                        };
                        Helper.db.Users.Add(dbNewUser);
                        Helper.db.SaveChanges();
                        return RedirectToRoute(new { controller = "Account", action = "UserLogin" });
                    }
                    else
                    {
                        ModelState.AddModelError("Ошибка", "Пользователь с логином " + NewUser.UserLogin + " уже существует");
                        return View(NewUser);
                    }
                }
                else
                {
                    ModelState.AddModelError("Ошибка", "Пароли не совпадают");
                    return View(NewUser);
                }
              
            }

                return View(NewUser);
        }

        /// <summary>
        ///  Выход пользователя из системы
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToRoute(new { controller = "Account", action = "UserLogin" });
        }
        
    }
}