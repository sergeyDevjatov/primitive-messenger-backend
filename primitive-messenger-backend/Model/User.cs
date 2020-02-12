using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace primitive_messenger_backend.Model
{
    public class User
    {
        public long Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
    }
}
