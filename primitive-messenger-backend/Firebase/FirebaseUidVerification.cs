using System.Threading.Tasks;


namespace primitive_messenger_backend.Firebase
{
    public class FirebaseUidVerification : IFirebaseVerifier
    {
        protected string _uid;
        public FirebaseUidVerification(string uid)
        {
            _uid = uid;
        }

        public async Task<bool> Verify(string firebaseIdToken)
        {
            var token = await FirebaseAuthentication.auth.VerifyIdTokenAsync(firebaseIdToken);

            return token != null && _uid != null && token.Uid == _uid;
        }
    }
}
