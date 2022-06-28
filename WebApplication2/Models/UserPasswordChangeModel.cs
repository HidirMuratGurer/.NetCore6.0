using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class UserPasswordChangeModel
    {
        public string oldpassword { get; set; }

        public string newpassword { get; set; }
        [Compare("newpassword",ErrorMessage ="Şifreler Aynı Olmalıdır.")]
        public string newpasswordagain { get; set; }
    }
}
