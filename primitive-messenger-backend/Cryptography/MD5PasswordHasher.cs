using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace primitive_messenger_backend.Cryptography
{
    public class MD5PasswordHasher : IPasswordHasher
    {
        protected string _salt;
        public MD5PasswordHasher(): this("SECRET")
        {
        }
        public MD5PasswordHasher(string salt)
        {
            _salt = salt;
        }
        public string HashPassword(string rawPassword)
        {
            return this.HashPassword(rawPassword, _salt);
        }
        public string HashPassword(string rawPassword, string salt)
        {
            var md5 = MD5.Create();
            var preparedString = rawPassword + salt + rawPassword.Substring(0, rawPassword.Length / 2) + salt;

            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(preparedString));

            return Encoding.ASCII.GetString(bytes);
        }
    }
}
