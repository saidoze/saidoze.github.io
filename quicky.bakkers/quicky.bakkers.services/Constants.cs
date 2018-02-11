using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quicky.bakkers.services
{
    public static class Constants
    {
        public static string DatabaseFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "QuickySportsApp.db");
        public static string FirebaseBaseUrl = "https://sport-league-creator.firebaseio.com";
        public static string FirebaseAuthorizationToken = "krmBS3n6U5ITvODSXQWrNbbnJlGBwrpKNuJKvDpa";

        public static string FirebasePlayerDbName = "players";
        public static string FirebaseMatchdayDbName = "matchdays";
        public static string FirebaseMatchDbName = "matches";
        public static string FirebaseTeamDbName = "teams";
        public static string FirebaseSettingsDbName = "settings";
    }
}
