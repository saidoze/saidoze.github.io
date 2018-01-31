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
    public class PlayerService : ServiceBase<Player>
    {
        //https://github.com/rlamasb/Firebase.Xamarin
        private FirebaseClient _firebase;

        public PlayerService() : base(Constants.FirebasePlayerDbName)
        {
        }

        public override List<Player> CreateObjects(IReadOnlyCollection<FirebaseObject<Player>> items)
        {
            return items.Select(i => new Player() { Key = i.Key, Name = i.Object.Name }).ToList();
        }

        public async Task<Player> SavePlayer(Player player)
        {
            try
            {
                var p = await SaveItem(player);

                return p;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
