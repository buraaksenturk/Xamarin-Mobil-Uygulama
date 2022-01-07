using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinProject
{
    public class UserData
    {
        static string webAPIKey = "AIzaSyDIOcRrmSuAkQGyZ7JBVEqjeuybnSYtvw8";
        FirebaseAuthProvider authProvider;

        public UserData()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIKey));
        }

        public async Task<bool> Register(string email, string password)
        {
            var token = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);

            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return true;
            }
            return false;
        }

        public async Task<string>SignIn(string email, string password)
        {
            var token = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return token.FirebaseToken;
            }
            return "";
        }

        public async Task<bool> RefreshPassword(string token, string password)
        {
            await authProvider.ChangeUserPassword(token, password);
            return true;
        }

        [Obsolete]
        public async Task<bool> Delete(string id)
        {
            await authProvider.DeleteUser(id);
            return true;
        }

    }
}
