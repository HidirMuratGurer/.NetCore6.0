using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Areas.Admin.Controllers
{
    [Route("Admin/[Controller]/[Action]/{id?}")]
    [Authorize(Roles = "Admin,Moderator")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
