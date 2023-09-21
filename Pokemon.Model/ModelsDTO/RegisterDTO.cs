using System.ComponentModel.DataAnnotations;

namespace Pokemon.Model.ModelsDTO
{
    public class RegisterDTO
    {
        [EmailAddress]
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
