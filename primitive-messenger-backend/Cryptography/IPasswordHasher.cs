using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace primitive_messenger_backend.Cryptography
{
    interface IPasswordHasher
    {
        string HashPassword(string rawPassword);
        string HashPassword(string rawPassword, string salt);
    }
}
