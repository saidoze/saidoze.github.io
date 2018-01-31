using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Quicky.SportsApp.Models;
using Quicky.SportsApp.Models.Interfaces;
using Quicky.SportsApp.Services.Providers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicky.SportsApp.Services.Service
{
    public class MatchdayService : ServiceBase<Matchday>
    {
        //https://github.com/rlamasb/Firebase.Xamarin

        public MatchdayService() : base(Constants.FirebaseMatchdayDbName)
        {
        }

        public override List<Matchday> CreateObjects(IReadOnlyCollection<FirebaseObject<Matchday>> items)
        {
            return items.Select(i => new Matchday()
            {
                Key = i.Key,
                Closed = i.Object.Closed,
                Date = i.Object.Date,
                Number = i.Object.Number,
                PresencePlayerKeys = i.Object.PresencePlayerKeys
            }).ToList();
        }

        public async Task<Matchday> SaveMatchday(Matchday matchday)
        {
            try
            {
                var p = await SaveItem(matchday);

                return p;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
