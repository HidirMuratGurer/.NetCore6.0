using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.VievComponents.Category
{
    public class CategoryList : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            CategoryManager cm = new CategoryManager(new EfCategoryRepository());
            var values = cm.GetList();
            return View(values);
        }
    }
}
