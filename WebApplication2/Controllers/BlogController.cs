using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        CategoryManager cm = new CategoryManager(new EfCategoryRepository());
        WriterManager wm = new WriterManager(new EfWriterRepository());
        BlogAddModel blm = new BlogAddModel();
        UserManager<AppUser> _userManager;

        public BlogController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var values = bm.GetListWithCategory();
            return View(values);
        }
        public IActionResult BlogReadAll(int id)
        {
            ViewBag.i = id;
            var values = bm.GetByBlogID(id);    
            return View(values);
        }

        public async Task<IActionResult> BlogListByWriter()
        {
            
            //int id = wm.TGetByFilter(x => x.WriterMail == User.Identity.Name).WriterID;
            // var values = bm.GetListWithCategoryByWriterBm(id);
            var id = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = bm.GetListWithCategoryByWriterBm(id.Id);
            return View(values);
        }
        [HttpGet]
        public IActionResult BlogAdd()
        {
            List<SelectListItem> categoryvalues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                   Text =x.CategoryName,
                                                   Value=x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryvalues;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BlogAdd([Bind(Prefix = "Item1")] Blog p, [Bind(Prefix = "Item2")] BlogAddModel blm)
        {
            if (blm.blogthumImage != null)
            {
                var extension = Path.GetExtension(blm.blogthumImage.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogImageFiles/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                blm.blogthumImage.CopyTo(stream);
                p.BlogThumbnailImage = "/BlogImageFiles/" + newimagename;
            }
            if (blm.blogImage != null)
            {
                var extension = Path.GetExtension(blm.blogImage.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogImageFiles/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                blm.blogImage.CopyTo(stream);
                p.BlogImage = "/BlogImageFiles/" + newimagename;
            }
            BlogValidator bv = new BlogValidator();
            ValidationResult result = bv.Validate(p);
            if (result.IsValid)
            {

                p.BlogStatus = true;
                p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                var id = await _userManager.FindByNameAsync(User.Identity.Name);
                p.AppUserId = id.Id;
                bm.TAdd(p);
                return RedirectToAction("BlogListByWriter", "Blog");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            List<SelectListItem> categoryvalues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryvalues;
            return View();
        }
        public IActionResult DeleteBlog(int id)
        {
            var blogvalue= bm.GetByID(id);
            bm.TDelete(blogvalue);
            return RedirectToAction("BlogListByWriter");
        }
        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            var blogvalue = bm.GetByID(id);
            List<SelectListItem> categoryvalues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryvalues;
            return View(blogvalue);
        }
        [HttpPost]
        public IActionResult EditBlog(Blog p)
        {
            bm.TUpdate(p);
            return RedirectToAction("BlogListByWriter");
        }
    }
}

