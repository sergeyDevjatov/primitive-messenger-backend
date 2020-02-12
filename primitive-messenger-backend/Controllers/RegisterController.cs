using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using primitive_messenger_backend.Cryptography;
using primitive_messenger_backend.Database;
using primitive_messenger_backend.Firebase;


namespace primitive_messenger_backend.Controllers
{
    public class RegisterData
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FirebaseIdToken { get; set; }
        public string Password { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IPasswordHasher _hasher = new MD5PasswordHasher();

        private readonly ILogger<RegisterController> _logger;

        public RegisterController(ILogger<RegisterController> logger)
        {
            _logger = logger;
        }

        public static IFirebaseVerifier GetFirebaseVerification(RegisterData data)
        {
            if(data.Phone != null)
            {
                return new FirebasePhoneVerification(data.Phone);
            }
            if(data.Email != null)
            {
                return new FirebaseEmailVerification(data.Email);
            }
            return null;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post(RegisterData data)
        {
            using DatabaseContext db = new DatabaseContext();

            var verification = GetFirebaseVerification(data);

            var isVerified = data.FirebaseIdToken != null && await verification.Verify(data.FirebaseIdToken);

            if(!isVerified)
            {
                return Unauthorized(new ClientErrorData
                {
                    Title = "Your credentials are not verified",
                });
            }

            var alreadyExists = db.Users
                .Where(u => u.Email == data.Email || u.Phone == data.Phone)
                .Count() <= 0;

            if(alreadyExists)
            {
                return Conflict(new ClientErrorData
                {
                    Title = "User with such email or phone already exists. Try other credentials"
                });
            }

            var dbUser = new Database.Models.User
            {
                Email = data.Email,
                Phone = data.Phone,
                Password = _hasher.HashPassword(data.Password),
            };

            var createdUser = db.Users.Add(dbUser).Entity;

            db.SaveChanges();

            return Ok(new Model.User
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                Nickname = createdUser.Nickname,
                Phone = createdUser.Phone,
            });
        }
    }
}
