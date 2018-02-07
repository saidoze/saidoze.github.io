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
    public class MatchService : ServiceBase<Match>
    {
        //https://github.com/rlamasb/Firebase.Xamarin

        public MatchService() : base(Constants.FirebaseMatchDbName)
        {
        }

        public override List<Match> CreateObjects(IReadOnlyCollection<FirebaseObject<Match>> items)
        {
            return items.Select(i => new Match()
            {
                Key = i.Key,
                MatchdayKey = i.Object.MatchdayKey,
                Team1Key = i.Object.Team1Key,
                Team2Key = i.Object.Team2Key,
                ScoreTeam1 = i.Object.ScoreTeam1,
                ScoreTeam2 = i.Object.ScoreTeam2
            }).ToList();
        }

        public async Task<Match> SaveMatch(Match match)
        {
            try
            {
                var p = await SaveItem(match);
                return p;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
