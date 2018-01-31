using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Quicky.SportsApp.Models;
using Quicky.SportsApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicky.SportsApp.Test.Firebase
{
    public class FirebaseXamarin
    {
        private FirebaseClient _firebase;
        private FirebaseAuthLink _auth;
        private string _firebaseAuthorizationToken = Constants.FirebaseAuthorizationToken;

        public FirebaseXamarin()
        {
            _firebase = new FirebaseClient("https://sport-league-creator.firebaseio.com");
            //var authProvider = new FirebaseAuthProvider(new FirebaseConfig("krmBS3n6U5ITvODSXQWrNbbnJlGBwrpKNuJKvDpa"));
            //test@test.com = "krmBS3n6U5ITvODSXQWrNbbnJlGBwrpKNuJKvDpa",
            //_auth = authProvider.CreateUserWithEmailAndPasswordAsync("test@test.com", "123456").Result;
        }

        public async Task Start()
        {
            // add new item to list of data 
            var item = await _firebase
              .Child("players")
              .WithAuth("krmBS3n6U5ITvODSXQWrNbbnJlGBwrpKNuJKvDpa") // <-- Add Auth token if required. Auth instructions further down in readme.
              .PostAsync(new Player() { Name = "ik" }, false);

            var newkey = item.Key;

            await _firebase
              .Child("players")
              .Child(newkey)
              //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
              .PutAsync(new Player() { Name = "ik22" });

            var items2 = await _firebase
              .Child("players")
              .WithAuth("krmBS3n6U5ITvODSXQWrNbbnJlGBwrpKNuJKvDpa") // <-- Add Auth token if required. Auth instructions further down in readme.
                                                                    //.OrderByKey()
                                                                    //.LimitToFirst(2)
              .OnceAsync<Player>();
        }

    }
}
