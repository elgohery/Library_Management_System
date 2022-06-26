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
    public class PublisherController : Controller
    {
        private readonly ApplicationDbContext context;

        public PublisherController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: PublisherController
        public ActionResult Index()
        {
            var publishers = new List<Publisher>();
            return View(publishers);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult search(List<Publisher> model, string authorName)
        {

            if (ModelState.IsValid)
            {
                var authors = context.publishers.Where(a => a.name.Contains(authorName)).ToList();
                return View(nameof(Index), authors);
            }

            return View(nameof(Index), model);
        }

        // GET: PublisherController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PublisherController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PublisherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Publisher publisher)
        {
            try
            {
                context.publishers.Add(publisher);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(publisher);
            }
        }

      

    }
}
