using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.VievComponents.Writer
{
    public class WriterInformationDashboard : ViewComponent
    {
        UserManager<AppUser> _userManager;
        
        public WriterInformationDashboard(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var username = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.ad = username.NameSurname;
            ViewBag.resim = username.ImageUrl;
            return View();
        }
    }
}
