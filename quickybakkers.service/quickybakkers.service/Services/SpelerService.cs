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

namespace Quickybakkers.Service.Services
{
    [Export(typeof(ISpelerService))]
    public class SpelerService : ISpelerService
    {
        private BusinessLogicContext _context;
        private IAppConfiguration _config;
        private const string LOG_SERVICE_CALL = "Call to SpelerService {0}, Params {1}";

        [ImportingConstructor]
        public SpelerService(IAppConfiguration config)
        {
            _config = config;
            _context = new BusinessLogicContext()
            {
                DataAccessContext = new DataAccessContext(_config.ConnectionString)
            };

            TraceUtils.TraceFilePath = @"C:\temp\";
            TraceUtils.DefaultSwitch = new System.Diagnostics.TraceSwitch("logger1", "logger1") { Level = TraceLevel.Verbose };
        }

        public Task<Speler> GetSpelerByIdAsync(int id)
        {
            TraceUtils.WriteVerbose(string.Format(LOG_SERVICE_CALL, "GetSpelerByIdAsync", id));
            return Task.FromResult(new Speler());
        }

        public Task<List<Speler>> GetSpelersAsync()
        {
            TraceUtils.WriteVerbose(string.Format(LOG_SERVICE_CALL, "GetSpelersAsync", ""));

            var bl = new BLSpelers(_context);
            var spelers = bl.GetAll();

            return Task.FromResult(spelers);
        }

        public Task<int> UpdateSpelerAsync(int id, Speler speler)
        {
            TraceUtils.WriteVerbose(string.Format(LOG_SERVICE_CALL, "UpdateSpelerAsync", ""));

            var bl = new BLSpelers(_context);
            var rowsAffected = bl.UpdateSpeler(speler);

            return Task.FromResult(rowsAffected);
        }

        public Task<int> SaveSpelerAsync(Speler speler)
        {
            TraceUtils.WriteVerbose(string.Format(LOG_SERVICE_CALL, "SaveSpelerAsync", ""));

            var bl = new BLSpelers(_context);
            var rowsAffected = bl.CreateSpeler(speler);

            return Task.FromResult(rowsAffected);
        }

        public Task<int> DeleteSpelerAsync(int id)
        {
            TraceUtils.WriteVerbose(string.Format(LOG_SERVICE_CALL, "DeleteSpelerAsync", ""));

            var bl = new BLSpelers(_context);
            var rowsAffected = bl.DeleteSpeler(id);

            return Task.FromResult(rowsAffected);
        }
    }
}
