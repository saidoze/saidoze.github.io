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
    [Export(typeof(ISpeeldagService))]
    public class SpeeldagService : ISpeeldagService
    {
        private BusinessLogicContext _context;
        private IAppConfiguration _config;
        private const string LOG_SERVICE_CALL = "Call to SpelerService {0}, Params {1}";

        [ImportingConstructor]
        public SpeeldagService(IAppConfiguration config)
        {
            _config = config;
            _context = new BusinessLogicContext()
            {
                DataAccessContext = new DataAccessContext(_config.ConnectionString)
            };

            TraceUtils.TraceFilePath = @"C:\temp\";
            TraceUtils.DefaultSwitch = new System.Diagnostics.TraceSwitch("logger1", "logger1") { Level = TraceLevel.Verbose };
        }

        public Task<List<Speeldag>> GetSpeeldagenAsync()
        {
            TraceUtils.WriteVerbose(string.Format(LOG_SERVICE_CALL, "GetSpeeldagenAsync", ""));

            var bl = new BLSpeeldagen(_context);
            var speeldagen = bl.GetAll();

            return Task.FromResult(speeldagen);
        }

        public Task<Speeldag> GetSpeeldagByIdAsync(int id)
        {
            TraceUtils.WriteVerbose(string.Format(LOG_SERVICE_CALL, "GetSpeeldagByIdAsync", id));
            return Task.FromResult(new Speeldag());
        }

        public Task<int> UpdateSpeeldagAsync(int id, Speeldag speeldag)
        {
            TraceUtils.WriteVerbose(string.Format(LOG_SERVICE_CALL, "UpdateSpeeldagAsync", ""));

            var bl = new BLSpeeldagen(_context);
            var rowsAffected = bl.UpdateSpeeldag(speeldag);

            return Task.FromResult(rowsAffected);
        }

        public Task<int> SaveSpeeldagAsync(Speeldag speeldag)
        {
            TraceUtils.WriteVerbose(string.Format(LOG_SERVICE_CALL, "SaveSpeeldagAsync", ""));

            var bl = new BLSpeeldagen(_context);
            var rowsAffected = bl.CreateSpeeldag(speeldag);

            return Task.FromResult(rowsAffected);
        }

        public Task<int> DeleteSpeeldagAsync(int id)
        {
            TraceUtils.WriteVerbose(string.Format(LOG_SERVICE_CALL, "DeleteSpeeldagAsync", ""));

            var bl = new BLSpeeldagen(_context);
            var rowsAffected = bl.DeleteSpeeldag(id);

            return Task.FromResult(rowsAffected);
        }
    }
}
