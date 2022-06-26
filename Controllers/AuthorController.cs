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
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext context;

        public AuthorController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: AuthorController
        public ActionResult Index()
        {
            
            List<Author> authors = new List<Author>()
            {
                
        };
            return View(authors);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult search(List<Author> model,string authorName)
        {

            if (ModelState.IsValid)
            {
                var authors = context.Authors.Where(a => a.name.Contains(authorName)).ToList();
                return View(nameof(Index), authors);
            }
            
            return View(nameof(Index), model);
        }
        // GET: AuthorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            try
            {
                context.Authors.Add(author);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(author);
            }
        }

        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuthorController/Edit/5
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

        // GET: AuthorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuthorController/Delete/5
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
