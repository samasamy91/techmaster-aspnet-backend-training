//using AuthRefactorPack.DTOs.BadAuth;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using TrainingCenter.Api.Data;
//using TrainingCenterAuthTask01.Entities;

//namespace AuthRefactorPack.Controllers
//{
//    [Route("api/bad-auth")]
//    [ApiController]
//    public class BadAuthController : ControllerBase
//    {
//        private readonly AppDbContext db;
//        public BadAuthController(AppDbContext db) { this.db = db; }
//        [HttpPost("login")]
//        public IActionResult Login(LoginRequest request)
//        {
//            var user = db.Users.FirstOrDefault(x => x.Email == request.Email);
//            if (user == null) return Ok("wrong email");
//            if (user.HashPassword != request.Password) return Ok("wrong password");
//            var token = "fake-token-" + user.Id;
//            return Ok(new { token = token, user = user });
//        }
//        [HttpPost("register")]
//        public IActionResult Register(RegisterRequest request)
//        {
//            var user = new User();
//            user.FullName = request.FullName;
//            user.Email = request.Email;
//            user.HashPassword = request.Password;
//            user.Role = request.Role;
//            db.Users.Add(user);
//            db.SaveChanges();
//            return Ok(user);
//        }
//    }
//}
