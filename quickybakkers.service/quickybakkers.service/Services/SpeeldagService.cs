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
using RA.Services.Composition;
using Microsoft.Extensions.Configuration;
using Quickybakkers.Service.Extensions;

namespace Quickybakkers.Service.Services
{
    [Export(typeof(ISpeeldagService))]
    public class SpeeldagService : ISpeeldagService
    {
        private readonly IConfiguration _settings;

        [ImportingConstructor]
        public SpeeldagService(IAspNetService<IConfiguration> settings)
        {
            _settings = settings.Value;
        }

        public Task<List<Speeldag>> GetSpeeldagenAsync()
        {
            var context = _settings.GetBusinessLogicContext();

            var bl = new BLSpeeldagen(context);
            var speeldagen = bl.GetAll();

            return Task.FromResult(speeldagen);
        }

        public Task<Speeldag> GetSpeeldagByIdAsync(int id)
        {
            var context = _settings.GetBusinessLogicContext();

            return Task.FromResult(new Speeldag());
        }

        //public Task<int> UpdateSpeeldagAsync(int id, Speeldag speeldag)
        //{
        //    TraceUtils.WriteVerbose(string.Format(LOG_SERVICE_CALL, "UpdateSpeeldagAsync", ""));

        //    var bl = new BLSpeeldagen(_context);
        //    var rowsAffected = bl.UpdateSpeeldag(speeldag);

        //    return Task.FromResult(rowsAffected);
        //}

        public Task<int> SaveSpeeldagAsync(Speeldag speeldag)
        {
            var context = _settings.GetBusinessLogicContext();

            var bl = new BLSpeeldagen(context);
            var rowsAffected = 0;
            if (speeldag.Id > 0)
                rowsAffected = bl.UpdateSpeeldag(speeldag);
            else
                rowsAffected = bl.CreateSpeeldag(speeldag);

            return Task.FromResult(rowsAffected);
        }

        public Task<int> DeleteSpeeldagAsync(int id)
        {
            var context = _settings.GetBusinessLogicContext();
            
            var bl = new BLSpeeldagen(context);
            var rowsAffected = bl.DeleteSpeeldag(id);

            return Task.FromResult(rowsAffected);
        }

        public Task<bool> SluitSpeeldagAsync(int speeldagId, List<int> spelerIds)
        {
            var context = _settings.GetBusinessLogicContext();

            var bl = new BLSpeeldagen(context);
            var succes = bl.SluitSpeeldag(speeldagId, spelerIds);

            return Task.FromResult(succes);
        }
    }
}
