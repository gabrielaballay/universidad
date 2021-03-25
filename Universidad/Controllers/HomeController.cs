using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Universidad.Models;

namespace Universidad.Controllers
{
    [Authorize(Roles = "Administrador,Alumno")]
    public class HomeController : Controller
    {
        private readonly DataContext contexto;
        private readonly IConfiguration config;

        public HomeController(DataContext contexto, IConfiguration config)
        {
            this.contexto = contexto;
            this.config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (TempData.ContainsKey("Error")) ViewBag.Error = TempData["Error"].ToString();

            return View();
        }

        // POST: Home/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginView loginView)
        {
            string role = "";
            try
            {
               
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: loginView.Clave,
                    salt: System.Text.Encoding.ASCII.GetBytes(config["Salt"]),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8));
                var u = contexto.Users.FirstOrDefault(x => x.Docket == loginView.Dni_Docket || x.DNI == loginView.Dni_Docket && x.Active==1);

                if (u != null && hashed == u.Password)
                {
                    if(u.Id_type==1)
                    {
                        role = "Administrador";
                    }
                    else
                    {
                        role = "Alumno";
                    }
                    var claims = new List<Claim> {
                            new Claim("Legajo", u.Docket.ToString()),
                            new Claim("FullName", u.Name + " " + u.Last_name),
                            new Claim(ClaimTypes.Role, role),
                        };
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties

                    {
                        AllowRefresh = true,

                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Legajo/DNI o Contraseña Incorrectos!";
                    return View();
                }

        }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Error += ex.StackTrace;
                return View();
            }
        }

        // GET: Home/Logout
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

    }
}
