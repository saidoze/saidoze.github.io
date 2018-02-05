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
    public class TeamService : ServiceBase<Team>
    {
        //https://github.com/rlamasb/Firebase.Xamarin
        private FirebaseClient _firebase;

        public TeamService() : base(Constants.FirebaseTeamDbName)
        {
        }

        public override List<Team> CreateObjects(IReadOnlyCollection<FirebaseObject<Team>> items)
        {
            return items.Select(i => new Team()
            {
                Key = i.Key,
                Player1Key = i.Object.Player1Key,
                Player2Key = i.Object.Player2Key,
                Player1AllowPoints = i.Object.Player1AllowPoints,
                Player2AllowPoints = i.Object.Player2AllowPoints
            }).ToList();
        }

        public async Task<Team> SaveTeam(Team team)
        {
            try
            {
                var p = await SaveItem(team);

                return p;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
