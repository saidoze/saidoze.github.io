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
    [Export(typeof(IKlassementService))]
    public class KlassementService : IKlassementService
    {
        private readonly IConfiguration _settings;
        private readonly ILogger<KlassementService> _logger;

        [ImportingConstructor]
        public KlassementService(IAspNetService<IConfiguration> settings, IAspNetService<ILogger<KlassementService>> logger)
        {
            _settings = settings.Value;
            _logger = logger.Value;
        }

        public Task<List<Klassement>> GetKlassementAsync()
        {
            var context = _settings.GetBusinessLogicContext();

            var bl = new BLKlassement(context);
            var klassement = bl.GetAll();

            return Task.FromResult(klassement);
        }
    }
}
