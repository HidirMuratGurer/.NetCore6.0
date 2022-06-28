namespace WebApplication2.Models
{
    public class UserUpdateViewModel
    {
        public string namesurname { get; set; }

        public string password { get; set; }
        public IFormFile imageurl { get; set; }
        public string stringimageurl { get; set; }
    }
}
