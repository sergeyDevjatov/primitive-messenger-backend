using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace primitive_messenger_backend.Firebase
{
    public interface IFirebaseVerifier
    {
        Task<bool> Verify(string firebaseTokenId);
    }
}
