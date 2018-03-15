using Firebase.Xamarin.Database;
using quicky.bakkers.models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quicky.bakkers.services.Services
{
    public class SettingService : ServiceBase<Setting>
    {
        //https://github.com/rlamasb/Firebase.Xamarin
        private FirebaseClient _firebase;

        public SettingService() : base(Constants.FirebaseSettingsDbName)
        {
        }

        public override List<Setting> CreateObjects(IReadOnlyCollection<FirebaseObject<Setting>> items)
        {
            return items.Select(i => new Setting() { Key = i.Key, AssemblyVersionAndroid = i.Object.AssemblyVersionAndroid, AssemblyVersionIos = i.Object.AssemblyVersionIos }).ToList();
        }

        public async Task<Setting> SaveSettings(Setting settings)
        {
            try
            {
                var p = await SaveItem(settings);

                return p;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
