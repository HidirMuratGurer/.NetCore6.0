using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Lütfen bir rol adı giriniz...")]
        public string name { get; set; }
    }
}
