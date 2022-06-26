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
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext context;

        public CategoryController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: CategoryController
        public ActionResult Index()
        {
            var model = context.categories.ToList();
            return View(model);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            var model = new category();
            return View(model);
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(category collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.categories.Add(collection);
                    context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }



        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = context.categories.Find(id);
            return View(model);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var category = context.categories.Find(id);
                context.categories.Remove(category);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
