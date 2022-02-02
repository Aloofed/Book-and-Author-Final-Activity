using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Finale.Models;

namespace Finale.Controllers
{
    public class HomeController : Controller
    {
       private readonly bookContext _cont = new bookContext();
       public HomeController(bookContext cont)
       {
           _cont = cont;
       }
        [HttpGet]
        public IActionResult Index()
        {
            var lis = _cont.Books.ToList();
            return View(lis);
        }
        public IActionResult Author()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Author(Author at)
        {
            _cont.Authors.Add(at);
            _cont.SaveChanges();

            
            return RedirectToAction("Index");
        }
        
        public IActionResult Book()
        {
            List<Author> cl = new List<Author>();
            cl = (from c in _cont.Authors select c).ToList();
            cl.Insert(0, new Author {Name = ""});
            ViewBag.message = cl;
            return View();

        }
        [HttpPost]
        public IActionResult Book(Book bk)
        {
            _cont.Books.Add(bk);
            _cont.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult List()
        {
            var list = _cont.Authors.ToList();
            return View(list);

        }
        public IActionResult Delete(int id)
        {
            var del = _cont.Authors.Where(q => q.Id == id).FirstOrDefault();
            _cont.Authors.Remove(del);
            _cont.SaveChanges();
            return RedirectToAction("Index");

        }
        
        public IActionResult Update(int id)
        {
            var Up = _cont.Authors.Where(q => q.Id ==id).FirstOrDefault();
            return View(Up);

        }
        [HttpPost]
        public IActionResult Update(Author at)
        {
            if(ModelState.IsValid)
            {
                _cont.Authors.Update(at);
                _cont.SaveChanges();
                return RedirectToAction("List");

            }
            return View(at);

        }
        public IActionResult Bupdate(int id)
        {
             List<Author> cl = new List<Author>();
            cl = (from c in _cont.Authors select c).ToList();
            cl.Insert(0, new Author {Name = ""});
            ViewBag.message = cl;
           
            var Up = _cont.Books.Where(q => q.Id ==id).FirstOrDefault();
            return View(Up);

        }

        [HttpPost]
        public IActionResult Bupdate(Book bk)
        {
            if(ModelState.IsValid)
            {
                _cont.Books.Update(bk);
                _cont.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(bk);
        }
         public IActionResult Bdelete(int id)
        {
            var del = _cont.Books.Where(q => q.Id == id).FirstOrDefault();
            _cont.Books.Remove(del);
            _cont.SaveChanges();
            return RedirectToAction("Index");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
