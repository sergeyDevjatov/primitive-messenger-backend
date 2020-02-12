using System.Threading.Tasks;


namespace primitive_messenger_backend.Firebase
{
    public class FirebasePhoneVerification : IFirebaseVerifier
    {
        protected string _phone;
        public FirebasePhoneVerification(string phone)
        {
            _phone = phone;
        }

        public async Task<bool> Verify(string firebaseIdToken)
        {
            var foundUser = await FirebaseAuthentication.auth.GetUserByPhoneNumberAsync(_phone);
            var isTokenVerified = await new FirebaseUidVerification(foundUser?.Uid).Verify(firebaseIdToken);

            return foundUser != null && isTokenVerified && foundUser?.PhoneNumber != null;
        }
    }
}
