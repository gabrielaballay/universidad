using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Universidad.Models;

namespace Universidad.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;

        public UserController(DataContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Register()
        {
            if (TempData.ContainsKey("Mensaje")) ViewBag.Mensaje = TempData["Mensaje"].ToString();

            if(TempData.ContainsKey("Error")) ViewBag.Error= TempData["Error"].ToString();

            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            try
            {
                user.Active = 1;
                if (ModelState.IsValid)
                {
                    string hashedclave = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: user.Password,
                    salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8));
                    user.Password = hashedclave;

                    context.Users.Add(user);
                    context.SaveChanges();
                    ViewBag.Mensaje = "Se registro con exito, vaya al login para ingresar.";

                }
                else
                {
                    ViewBag.Error = "Ocurrio un error, Verifique los datos e intentelo de nuevo.";
                }
                                
                return View();
            }
            catch(Exception e)
            {
                ViewBag.Error = e.StackTrace;
                ViewBag.Error += e.Message;
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
