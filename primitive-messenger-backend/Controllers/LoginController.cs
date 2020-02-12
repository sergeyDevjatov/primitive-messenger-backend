using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using primitive_messenger_backend.Database;
using primitive_messenger_backend.Cryptography;


namespace primitive_messenger_backend.Controllers
{
    public class LoginData
    {
        public string Phone { get; set; }
        public string Password { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IPasswordHasher _hasher = new MD5PasswordHasher();

        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Post(LoginData data)
        {
            using DatabaseContext db = new DatabaseContext();

            var hashedPassword = _hasher.HashPassword(data.Password);

            var foundUser = db.Users
                            .Where(u => u.Phone.Trim() == data.Phone.Trim())
                            .Where(u => u.Password == hashedPassword)
                            .FirstOrDefault();

            if (foundUser != null)
            {
                var responseUser = new Model.User
                {
                    Id = foundUser.Id,
                    Nickname = foundUser.Nickname,
                    Phone = foundUser.Phone,
                };
                return Ok(responseUser);
            }
            return Unauthorized(new ClientErrorData {
                Title = "Login or password is incorrect",
            });
        }
    }
}
