using System.Threading.Tasks;


namespace primitive_messenger_backend.Firebase
{
    public class FirebaseEmailVerification : IFirebaseVerifier
    {
        protected string _email;
        public FirebaseEmailVerification (string email)
        {
            _email = email;
        }
        public async Task<bool> Verify(string firebaseIdToken)
        {
            var foundUser = await FirebaseAuthentication.auth.GetUserByEmailAsync(_email);
            var isTokenVerified = await new FirebaseUidVerification(foundUser?.Uid).Verify(firebaseIdToken);

            return foundUser != null && isTokenVerified && (foundUser?.EmailVerified ?? false);
        }
    }
}
