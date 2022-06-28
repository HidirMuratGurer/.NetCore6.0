using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace WebApplication2.Controllers
{
    [Authorize(Roles = "Admin,Moderator,Writer")]
    public class DashboardController : Controller
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        CategoryManager cm = new CategoryManager(new EfCategoryRepository());
        WriterManager wm = new WriterManager(new EfWriterRepository());
        readonly UserManager<AppUser> _userManager;

        public DashboardController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var usermail = await _userManager.FindByNameAsync(User.Identity.Name);
            int id = usermail.Id;
            ViewBag.deneme = usermail.UserName;
            ViewBag.ToplamBlog = bm.GetList().Count();
            ViewBag.YazarBlogSayısı = bm.GetBlogListByWriter(id).Count();
            ViewBag.KategoriSayısı = cm.GetList().Count();
            return View();
        }
    }
}
