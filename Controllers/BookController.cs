using Books.Data;
using Books.Models;
using Books.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext context;

        public BookController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var model = new BookViewModel()
            {
                books=new List<Models.Book>(),
                categories=context.categories.ToList(),
                book=new Models.Book() 
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexAsync(BookViewModel model)
        {
           
            if (ModelState.IsValid)
            {

                var book = new Book
                {
                    AuthorName=model.book.AuthorName,
                     bookId=model.book.bookId,
                     bookName=model.book.bookName,
                     category=model.book.category,
                     categoryId = model.book.categoryId,
                     desc=model.book.desc
                };

                if (model.book.bookId < 0)
                {
                    //create
                    try
                    {
                        try
                        {
                            if (model.image != null)
                            {

                                string extention = Path.GetExtension(model.image.FileName);
                                string newfilename = DateTime.Now.Ticks.ToString() + extention; //""637891830433253261.png"
                                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//Uploads//");

                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }
                                string uploadedPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//Uploads//", newfilename);

                                var stream = new FileStream(uploadedPath, FileMode.Create);
                                await model.image.CopyToAsync(stream);
                                book.imgPath = "/Uploads/" + newfilename;
                                ViewBag.msg = "Image Successfully Uploaded";
                            }
                        }
                        catch
                        {
                            ViewBag.msg = "Error Image Uploaded";
                            HttpContext.Session.SetString("msgtype", "Falied");
                            HttpContext.Session.SetString("title", "لم يتم الاضافه ");
                            HttpContext.Session.SetString("msg", "لم يتم اضافه الكتاب ");
                            return RedirectToAction("index");
                        }
                        book.bookId = 0;
                        context.Books.Add(book);
                        context.SaveChanges();
                     
                        HttpContext.Session.SetString("msgtype", "success");
                        HttpContext.Session.SetString("title", "تم الحفظ");
                        HttpContext.Session.SetString("msg", "تم حفظ الكتاب بنجاح");
                        return RedirectToAction("index");
                    }
                    catch
                    {
                        HttpContext.Session.SetString("msgtype", "Falied");
                        HttpContext.Session.SetString("title", "لم يتم الاضافه ");
                        HttpContext.Session.SetString("msg", "لم يتم اضافه الكتاب ");
                        return RedirectToAction("index");
                    }

                }
                else
                {
                    // edit
                    try
                    {
                        context.Books.Update(book);
                        context.SaveChanges();
                        HttpContext.Session.SetString("msgtype", "success");
                        HttpContext.Session.SetString("title", "تم التعديل");
                        HttpContext.Session.SetString("msg", "تم تعديل الكتاب بنجاح");
                        return RedirectToAction("index");
                    }
                    catch
                    {
                        HttpContext.Session.SetString("msgtype", "Falied");
                        HttpContext.Session.SetString("title", "لم يتم الاضافه ");
                        HttpContext.Session.SetString("msg", "لم يتم تعديل الكتاب ");
                        return RedirectToAction("index");
                    }
                }
            }
            HttpContext.Session.SetString("msgtype", "Falied");
            HttpContext.Session.SetString("title", "لم يتم الاضافه ");
            HttpContext.Session.SetString("msg", "لم يتم اضافه الكتاب ");
            model.categories = context.categories.ToList();
            model.books = new List<Models.Book>();
            return View(model);
        }
        public ActionResult Create()
        {
            ViewBag.categories = context.categories.ToList();
            return View();
        }

        // POST: PublisherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Book book,IFormFile image)
        {
            try
            {
                try
                {
                    if (image != null)
                    {

                        string extention = Path.GetExtension(image.FileName);
                        string newfilename = DateTime.Now.Ticks.ToString() + extention; //""637891830433253261.png"
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//Uploads//");

                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string uploadedPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//Uploads//", newfilename);

                        var stream = new FileStream(uploadedPath, FileMode.Create);
                        await image.CopyToAsync(stream);
                        book.imgPath = "/Uploads/" + newfilename;
                        ViewBag.msg = "Image Successfully Uploaded";
                    }
                }
                catch
                {
                    ViewBag.msg = "Error Image Uploaded";
                    HttpContext.Session.SetString("msgtype", "Falied");
                    HttpContext.Session.SetString("title", "لم يتم الاضافه ");
                    HttpContext.Session.SetString("msg", "لم يتم اضافه الكتاب ");
                    return RedirectToAction("index");
                }
                
                context.Books.Add(book);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(book);
            }
        }
        public IActionResult Delete(int id)
        {
            var book = context.Books.Where(a => a.bookId == id).FirstOrDefault();
            
            try
            {
            
                context.Books.Remove(book);
                context.SaveChanges();
                HttpContext.Session.SetString("msgtype", "success");
                HttpContext.Session.SetString("title", "تم الحذف");
                HttpContext.Session.SetString("msg", "تم حذف الكتاب بنجاح");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                HttpContext.Session.SetString("msgtype", "Falied");
                HttpContext.Session.SetString("title", "لم يتم الحذف ");
                HttpContext.Session.SetString("msg", "لم يتم حذف الكتاب ");
                return RedirectToAction(nameof(Index));
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult search(BookViewModel model)
        {
         
            if (ModelState.IsValid)
            {
                var books = new BookViewModel()
                {
                    books=context.Books.Include(a=>a.category).Where(a=>a.bookName.Contains(model.book.bookName) && a.categoryId==model.book.categoryId).ToList(),
                    categories = context.categories.ToList(),
                    book=new Models.Book()
                    {
                        bookName=model.book.bookName,
                        categoryId=model.book.categoryId
                    }
                };
                return View(nameof(Index), books);
            }
            model.categories = context.categories.ToList();
            model.books = new List<Models.Book>();
            return View(nameof(Index),model);
        }

      
    }
}
