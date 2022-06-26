using Books.Data;
using Books.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly UserManager<student> userManager;
        private readonly SignInManager<student> signInManager;
        private readonly ApplicationDbContext context;

        public AccountsController(UserManager<student> _userManager, SignInManager<student> _signInManager, ApplicationDbContext context)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            this.context = context;
        }
        public IActionResult Index()
        {
            var model = context.students.Where(a => a.IsAdmin==false).ToList();
            return View(model);
        }
        public IActionResult Register()
        {
            var model = new student();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(student model)
        {
            if (ModelState.IsValid)
            {
                var user = new student
                {
                    Id = Guid.NewGuid().ToString(),
                    name=model.name,
                    level=model.level,
                    UserName = model.Email,
                    Email = model.Email,
                    IsAdmin=false,
                    
                    PhoneNumber = model.PhoneNumber,
                   
                };
                user.Id = Guid.NewGuid().ToString();
                var result = await userManager.CreateAsync(user, model.PasswordHash);
                if (result.Succeeded)
                {
                    HttpContext.Session.SetString("msgtype", "success");
                    HttpContext.Session.SetString("title", "تم الحفظ");
                    HttpContext.Session.SetString("msg", "تم التعديل بنجاح");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    HttpContext.Session.SetString("msgtype", "Falied");
                    HttpContext.Session.SetString("title", "لم يتم التعديل ");
                    HttpContext.Session.SetString("msg", "لم يتم التعديل ");
                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(string id)
        {
            if (ModelState.IsValid )
            {
                var model = context.students.Where(a => a.Id == id).FirstOrDefault();
                model.IsAdmin = true;
                context.students.Update(model);
                context.SaveChanges();
                HttpContext.Session.SetString("msgtype", "success");
                HttpContext.Session.SetString("title", "تم الحفظ");
                HttpContext.Session.SetString("msg", "تم التعديل بنجاح");
                return RedirectToAction(nameof(Index));
            }
            HttpContext.Session.SetString("msgtype", "Falied");
            HttpContext.Session.SetString("title", "لم يتم التعديل ");
            HttpContext.Session.SetString("msg", "لم يتم التعديل ");
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(string id)
        {
            try
            {
                var model = context.students.Where(a => a.Id == id).FirstOrDefault();
                context.students.Remove(model);
                context.SaveChanges();
                HttpContext.Session.SetString("msgtype", "success");
                HttpContext.Session.SetString("title", "تم الحذف");
                HttpContext.Session.SetString("msg", "تم الحذف بنجاح");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                HttpContext.Session.SetString("msgtype", "Falied");
                HttpContext.Session.SetString("title", "لم يتم الحذف ");
                HttpContext.Session.SetString("msg", "لم يتم الحذف ");
                return RedirectToAction(nameof(Index));
            }
        }
    }
}


