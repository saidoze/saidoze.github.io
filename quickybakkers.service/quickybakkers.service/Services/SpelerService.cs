using Quickybakkers.Service.Models;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Actemium.Tracing;
using System.Diagnostics;
using Actemium.BusinessLogic;
using Quickybakkers.Service.Config;
using Actemium.DataAccess;
using Quickybakkers.Service.BusinessLogic;
using Quickybakkers.Service.Interfaces;
using Microsoft.Extensions.Logging;
using RA.Services.Composition;
using Microsoft.Extensions.Configuration;
using Quickybakkers.Service.Extensions;

namespace Quickybakkers.Service.Services
{
    [Export(typeof(ISpelerService))]
    public class SpelerService : ISpelerService
    {
        private readonly IConfiguration _settings;
        private readonly ILogger<SpelerService> _logger;

        [ImportingConstructor]
        public SpelerService(IAspNetService<IConfiguration> settings, IAspNetService<ILogger<SpelerService>> logger)
        {
            _settings = settings.Value;
            _logger = logger.Value;
        }

        public Task<Speler> GetSpelerByIdAsync(int id)
        {
            var context = _settings.GetBusinessLogicContext();

            return Task.FromResult(new Speler());
        }

        public Task<List<Speler>> GetSpelersAsync()
        {
            var context = _settings.GetBusinessLogicContext();

            var bl = new BLSpelers(context);
            var spelers = bl.GetAll();

            return Task.FromResult(spelers);
        }

        //public Task<int> UpdateSpelerAsync(int id, Speler speler)
        //{

        //    var bl = new BLSpelers(_context);
        //    var rowsAffected = bl.UpdateSpeler(speler);

        //    return Task.FromResult(rowsAffected);
        //}

        public Task<int> SaveSpelerAsync(Speler speler)
        {
            var context = _settings.GetBusinessLogicContext();
            
            var bl = new BLSpelers(context);
            var rowsAffected = 0;
            if (speler.Id > 0)
                rowsAffected = bl.UpdateSpeler(speler);
            else
                rowsAffected = bl.CreateSpeler(speler);

            return Task.FromResult(rowsAffected);
        }

        public Task<int> DeleteSpelerAsync(int id)
        {
            var context = _settings.GetBusinessLogicContext();

            var bl = new BLSpelers(context);
            var rowsAffected = bl.DeleteSpeler(id);

            return Task.FromResult(rowsAffected);
        }
    }
}
