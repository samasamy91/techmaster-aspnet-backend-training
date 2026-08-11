using System.ComponentModel.DataAnnotations;

namespace AuthRefactorPack.DTOs.BadAuth
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    

}

