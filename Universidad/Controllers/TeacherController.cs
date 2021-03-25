using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ubiety.Dns.Core;
using Universidad.Models;

namespace Universidad.Controllers
{
    public class TeacherController : Controller
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;

        public TeacherController(DataContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        // GET: TeacherController
        public ActionResult Index()
        {
            //var teachers = context.Teachers.Where(t=>t.Active==1).ToList();
            return View();
        }

        [HttpPost]
        public JsonResult Lista()
        {
            var teachers = context.Teachers.Where(t => t.Active == 1).ToList();
            return Json(new { data = teachers });
        }
        // GET: TeacherController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TeacherController/Create
        public ActionResult Create()
        {
            if (TempData.ContainsKey("Mensaje")) ViewBag.Mensaje = TempData["Mensaje"].ToString();

            if (TempData.ContainsKey("Error")) ViewBag.Error = TempData["Error"].ToString();

            return View();
        }

        // POST: TeacherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Teacher teacher)
        {
            try
            {
                if (ModelState.IsValid) {
                    var logeado = User.Claims.ToArray();
                    var docket = Convert.ToInt32( logeado[0].Value);
                    var user_logeado = context.Users.FirstOrDefault(x => x.Docket == docket);

                    teacher.Active = 1;
                    teacher.Id_user = user_logeado.Id_user;

                    var verifica = context.Teachers.FirstOrDefault(t=>t.DNI==teacher.DNI);

                    if (verifica == null)
                    {
                        context.Teachers.Add(teacher);
                        context.SaveChanges();
                        ViewBag.Mensaje = "Se agrego el profesor en forma correcta";
                    }
                    else
                    {
                        ViewBag.Error = "Ya existe un profesor con ese DNI.";
                    }
                    
                }
                else
                {
                    ViewBag.Error = "Algo salio mal revise los datos e intente de nuevo.";
                    return View();
                }
                //return RedirectToAction(nameof(Index));

                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                //ViewBag.Error += e.StackTrace;
                return View();
            }
        }

        // GET: TeacherController/Edit/5
        public ActionResult Edit(int id)
        {
            var teacher = context.Teachers.Find(id);
            return View(teacher);
        }

        // POST: TeacherController/Edit/5
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

        //// GET: TeacherController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: TeacherController/Delete/5
        [HttpPost]
        
        public JsonResult Delete(int? id)
        {
            string Mensaje = "Ocurio un error intentelo mas tarde.";
            bool okResult = false;
            try
            {
                if (id != null)
                {
                    var teacher = context.Teachers.Find(id);
                    teacher.Active = 0;
                    context.Teachers.Update(teacher);
                    context.SaveChanges();
                    Mensaje = "Se modifico el profesor en forma correcta.";
                    okResult = true;
                }

                return Json(new { IsSuccess = okResult, Message = Mensaje, Id = id });

            }
            catch
            {
                return Json(new { IsSuccess = okResult, Message = Mensaje, Id = id });
            }
        }
    }
}
