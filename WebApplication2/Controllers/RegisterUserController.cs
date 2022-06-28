using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [AllowAnonymous]
    public class RegisterUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterUserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserSignUpViewModel userSignUpViewModel)
        {
            if (!userSignUpViewModel.TermContract)
            {
                ModelState.AddModelError("TermContract", "Kayıt olabilmek için lütfen gizlilik sözleşmesini kabul edin!");
                return View(userSignUpViewModel);
            }
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    Email = userSignUpViewModel.Mail,
                    UserName = userSignUpViewModel.UserName,
                    NameSurname = userSignUpViewModel.NameSurname
                };
                var result = await _userManager.CreateAsync(user, userSignUpViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(userSignUpViewModel);
        }
    }
}
