using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Quicky.SportsApp.Models;

[assembly: Xamarin.Forms.Dependency(typeof(Quicky.SportsApp.Services.MockDataStore))]
namespace Quicky.SportsApp.Services
{
    public class MockDataStore : IDataStore
    {
        List<Player> players;

        public MockDataStore()
        {
            players = new List<Player>();
            var mockPlayers = new List<Player>
            {
                new Player { Name = "Quicky" },
                new Player { Name = "Tobias" },
                new Player { Name = "PJ" },
                new Player { Name = "Mike" }
            };

            foreach (var p in mockPlayers)
            {
                players.Add(p);
            }
        }

        public async Task<List<Player>> GetPlayersAsync()
        {
            return await Task.FromResult(players);
        }
    }
}
