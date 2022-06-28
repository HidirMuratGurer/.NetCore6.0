using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.VievComponents.Writer
{
    public class WriterInformationOnNavbar : ViewComponent
    {
        UserManager<AppUser> _userManager;

        public WriterInformationOnNavbar(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Name = user.NameSurname;
            ViewBag.Image = user.ImageUrl;

            return View();
        }
    }
}
