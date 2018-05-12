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
    [Export(typeof(IMatchDetailService))]
    public class MatchDetailService : IMatchDetailService
    {
        private readonly IConfiguration _settings;
        private readonly ILogger<MatchDetailService> _logger;

        [ImportingConstructor]
        public MatchDetailService(IAspNetService<IConfiguration> settings, IAspNetService<ILogger<MatchDetailService>> logger)
        {
            _settings = settings.Value;
            _logger = logger.Value;
        }
        
        public Task<List<MatchDetail>> GetMatchDetails(int? speeldagId)
        {
            var context = _settings.GetBusinessLogicContext();

            var bl = new BLMatchDetails(context);
            var matchDetails = bl.GetAll(speeldagId);

            return Task.FromResult(matchDetails);
        }
    }
}
