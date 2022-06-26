using Books.Data;
using Books.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Controllers
{
    [Authorize]
    public class lendingController : Controller
    {
        private readonly ApplicationDbContext context;

        public lendingController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: lendingController
        public ActionResult Index()
        {
            var lending = new List<lending>();
            return View(lending);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult search(List<Publisher> model, int authorName)
        {

            if (ModelState.IsValid)
            {
                var authors = context.lendings.Where(a => a.studentid==authorName).ToList();
                return View(nameof(Index), authors);
            }

            return View(nameof(Index), model);
        }
        // GET: lendingController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: lendingController/Create
        public ActionResult Create()
        {
            ViewBag.books = context.Books.ToList();
            return View();
        }

        // POST: lendingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(lending lending)
        {
            try
            {
                context.lendings.Add(lending);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(lending);
            }
        }

        // GET: lendingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: lendingController/Edit/5
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

        // GET: lendingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: lendingController/Delete/5
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
