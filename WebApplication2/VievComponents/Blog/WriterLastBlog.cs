using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.VievComponents.Blog
{
    public class WriterLastBlog : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            BlogManager bm = new BlogManager(new EfBlogRepository());
            var values = bm.GetBlogListByWriter(1);
            return View(values);
        }
    }
}
