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
    [Export(typeof(IMatchService))]
    public class MatchService : IMatchService
    {
        private BusinessLogicContext _context;
        private IAppConfiguration _config;
        private const string LOG_SERVICE_CALL = "Call to MatchService {0}, Params {1}";

        [ImportingConstructor]
        public MatchService(IAppConfiguration config)
        {
            _config = config;
            _context = new BusinessLogicContext()
            {
                DataAccessContext = new DataAccessContext(_config.ConnectionString)
            };

            TraceUtils.TraceFilePath = @"C:\temp\";
            TraceUtils.DefaultSwitch = new System.Diagnostics.TraceSwitch("logger1", "logger1") { Level = TraceLevel.Verbose };
        }
        
        public Task<int> SaveMatchAsync(Match match)
        {
            TraceUtils.WriteVerbose(string.Format(LOG_SERVICE_CALL, "SaveMatchAsync", ""));

            var bl = new BLMatchen(_context);
            var rowsAffected = 0;
            /*if (match.Id > 0)
                rowsAffected = bl.UpdateSpeler(match);
            else*/
                rowsAffected = bl.CreateMatch(match);

            return Task.FromResult(rowsAffected);
        }
    }
}
