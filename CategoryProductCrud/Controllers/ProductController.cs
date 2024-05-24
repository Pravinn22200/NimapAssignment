using CategoryProductCrud.DataAccessLayer;
using CategoryProductCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CategoryProductCrud.Controllers
{
    public class ProductController : Controller
    {
        private readonly MyAppDBContext _context;

        public ProductController(MyAppDBContext context)
        {
            _context = context;
        }

        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var products = _context.Products.Include(p => p.Categories)
                               .OrderBy(p => p.ProductId)
                               .Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRecords = _context.Products.Count();

            //var products = _context.Products.Include("Categories");
            return View(products);
        }
       
           
        

        [HttpGet]
        public IActionResult Create()
        {
            LoadCategories();
            return View(); 
        }

        [NonAction]
        private void LoadCategories()
        {
            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories,"CategoryId","CategoryName");

        }
        [HttpPost]
        public IActionResult Create(Product model)
        {

            _context.Products.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id !=null )
            {
                NotFound();
            }
            LoadCategories();
            var product = _context.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            ModelState.Remove("Categories");
            if(ModelState.IsValid)
            {
                _context.Products.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if(id != null)
            {
                NotFound();
            }
            LoadCategories();
            var product = _context.Products.Find(id);
            return View(product);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(Product model)
        {
            _context.Products.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
