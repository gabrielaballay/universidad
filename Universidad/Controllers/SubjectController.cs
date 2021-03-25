using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Universidad.Models;

namespace Universidad.Controllers
{
    [Authorize(Roles = "Administrador,Alumno")]
    public class SubjectController : Controller
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;

        public SubjectController(DataContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        // GET: SubjectController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Lista()
        {
            var subjects = context.Subjects.Where(t => t.Active == 1).ToList();
            return Json(new { data = subjects });
        }

        // GET: SubjectController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SubjectController/Create
        public ActionResult Create()
        {
            if (TempData.ContainsKey("Mensaje")) ViewBag.Mensaje = TempData["Mensaje"].ToString();

            if (TempData.ContainsKey("Error")) ViewBag.Error = TempData["Error"].ToString();

            return View();
        }

        // POST: SubjectController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Subject subject)
        {
            try
            {                
                if (ModelState.IsValid)
                {
                    var logeado = User.Claims.ToArray();
                    var docket = Convert.ToInt32(logeado[0].Value);
                    var user_logeado = context.Users.FirstOrDefault(x => x.Docket == docket);

                    subject.Active = 1;
                    subject.Id_User = user_logeado.Id_user;                    

                    context.Subjects.Add(subject);
                    context.SaveChanges();

                    ViewBag.Mensaje = "Se agrego la materia en forma correcta";
                }
                else
                {
                    ViewBag.Error = "Ocurrio un error intentelo mas tarde.";
                }
                return View();
            }
            catch(Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

        // GET: SubjectController/Edit/5
        public ActionResult Edit(int id)
        {
            var subject = context.Subjects.Find(id);
            return View(subject);
        }

        // POST: SubjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Subject subject)
        {
            subject.Id_subject = id;
            try
            {
                if (ModelState.IsValid)
                {
                    var logeado = User.Claims.ToArray();
                    var docket = Convert.ToInt32(logeado[0].Value);
                    var user_logeado = context.Users.FirstOrDefault(x => x.Docket == docket);
                    subject.Active = 1;
                    subject.Id_User = user_logeado.Id_user;

                    context.Subjects.Update(subject);
                    context.SaveChanges();
                }
                else
                {
                    ViewBag.Error = "Algo salio mal revise los datos e intente de nuevo.";
                    return View(subject);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View(subject);
            }
        }

        [HttpPost]
        public JsonResult Delete(int? id)
        {
            string Mensaje = "Ocurio un error intentelo mas tarde.";
            bool okResult = false;
            try
            {
                if (id != null)
                {
                    var logeado = User.Claims.ToArray();
                    var docket = Convert.ToInt32(logeado[0].Value);
                    var user_logeado = context.Users.FirstOrDefault(x => x.Docket == docket);
                    
                    var subject = context.Subjects.Find(id);
                    subject.Active = 0;
                    subject.Id_User = user_logeado.Id_user;

                    context.Subjects.Update(subject);
                    context.SaveChanges();

                    Mensaje = "Se modifico la materia en forma correcta.";
                    okResult = true;
                }

                return Json(new { IsSuccess = okResult, Message = Mensaje, Id = id });

            }
            catch
            {
                return Json(new { IsSuccess = okResult, Message = Mensaje, Id = id });
            }
        }


        // GET: SubjectController/ListInscripcion
        public ActionResult ListInscripcion()
        {
            return View();
        }


        // POST: SubjectController/Edit/5
        [HttpPost]
        public ActionResult VerSubject(int? id)
        {
            string Mensaje = "Ocurio un error intentelo mas tarde.";
            bool okResult = false;
            try
            {
                if (id != null)
                {
                    var subject = context.Subjects.Include(x => x.profe).FirstOrDefault(s => s.Id_subject == id);
                    Mensaje = "Exito.";
                    okResult = true;
                    return Json(new { IsSuccess = okResult, Message = Mensaje, SubjectVer = subject });
                }
                else{
                    return Json(new { IsSuccess = okResult, Message = Mensaje, Id = id });
                }                
            }
            catch
            {
                return Json(new { IsSuccess = okResult, Message = Mensaje, Id = id });
            }
        }

        // GET: SubjectController/Edit/5
        public ActionResult SignOn(int id)
        {
            if (TempData.ContainsKey("Mensaje")) ViewBag.Mensaje = TempData["Mensaje"].ToString();

            if (TempData.ContainsKey("Error")) ViewBag.Error = TempData["Error"].ToString();

            var subject = context.Subjects.Include(x => x.profe).FirstOrDefault(s => s.Id_subject == id);

            return View(subject);
        }

        // POST: SubjectController/Edit/5
        //[HttpPost]
        public ActionResult SignOnConfirm(int id)
        {
            var subject =context.Subjects.Find( id);
            List<Subject> lista = new List<Subject>();
            bool bandera = false;
            try
            {
                if (id>0)
                {
                    var logeado = User.Claims.ToArray();
                    var docket = Convert.ToInt32(logeado[0].Value);
                    var user_logeado = context.Users.FirstOrDefault(x => x.Docket == docket);

                    var verifica= context.Registereds.FirstOrDefault(x=>x.Id_subject==id && x.Id_user==user_logeado.Id_user);

                    if (verifica==null)
                    {

                        var materia = context.Subjects.Find(id);
                        
                        var materias_incripto = context.Registereds.Where(m => m.Id_user == user_logeado.Id_user);
                        var cantidad_inscriptos = context.Registereds.Where(x => x.Id_subject == id).Count();
                        
                        foreach (var item in materias_incripto)
                        {
                            lista = context.Subjects.Where(x => x.Id_subject == item.Id_subject).ToList();
                        }

                        foreach(var item in lista)
                        {
                            if(item.Start_time > materia.Start_time && item.End_time > materia.Start_time)
                            {
                                bandera = true;
                            }
                        }

                        if (bandera)
                        {
                            TempData["Error"] = "Ud tiene una materia que se suporpone con esta.";
                            return RedirectToAction("SignOn", new { id = id });
                        }

                        if (cantidad_inscriptos >= materia.Max_flake) {
                            TempData["Error"] = "No hay mas cupo para esta Materia";
                            return RedirectToAction("SignOn", new { id = id });
                        }

                        var registrado=new Registered { Active = 1, Id_subject = id, Id_user = user_logeado.Id_user };

                        context.Registereds.Add(registrado);
                        context.SaveChanges();

                        TempData["Mensaje"] = "Ud se inscribio de forma correcta";
                        return RedirectToAction("SignOn", new { id = id });

                    }
                    else
                    {
                        TempData["Error"] = "Ud ya esta registrado en esa materia.";
                        return RedirectToAction("SignOn", new { id = id });
                    }
                    
                    
                }
                else
                {
                    TempData["Error"] = "Algo salio mal revise los datos e intente de nuevo.";
                    return RedirectToAction("SignOn", new { id = id } );
                }
                //return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction("SignOn", new { id = id });
            }
        }
    }
}
