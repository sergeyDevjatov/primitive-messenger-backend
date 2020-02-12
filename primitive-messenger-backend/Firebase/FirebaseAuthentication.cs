
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;


namespace primitive_messenger_backend.Firebase
{
    public static class FirebaseAuthentication
    {
        static public FirebaseAuth auth { get { return _auth; } }

        static private FirebaseApp _app;
        static private FirebaseAuth _auth;
        static FirebaseAuthentication()
        {
            _app = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("./primitive-messenger-firebase-adminsdk-dtwj4-b2248cebe5.json"),
            });

            _auth = FirebaseAuth.DefaultInstance;
        }
    }
}
