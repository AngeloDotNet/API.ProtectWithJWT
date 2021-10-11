using System.ComponentModel.DataAnnotations;

namespace API_Protect_With_JWT.Models.InputModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]  
        public string Username { get; set; }  
  
        [Required(ErrorMessage = "Password is required")]  
        public string Password { get; set; }
    }
}