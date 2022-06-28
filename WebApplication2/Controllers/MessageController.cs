using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [AllowAnonymous]
    public class MessageController : Controller
    {
        Message2Manager mm = new Message2Manager(new EfMessage2Repository());
        UserManager<AppUser> _userManager;

        public MessageController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> InBox()
        {

            var usermail = await _userManager.FindByNameAsync(User.Identity.Name);
            int id = usermail.Id;
            var values = mm.GetInboxListByWriter(id);
            return View(values);
        }
      
        public IActionResult MessageDetails(int id)
        {
            var values = mm.GetByID(id);
            return View(values);
        }
        
    }
}
