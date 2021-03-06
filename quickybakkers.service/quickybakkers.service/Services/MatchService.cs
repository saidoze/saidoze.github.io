﻿using Quickybakkers.Service.Models;
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
    [Export(typeof(IMatchService))]
    public class MatchService : IMatchService
    {
        private readonly IConfiguration _settings;
        private readonly ILogger<MatchService> _logger;

        [ImportingConstructor]
        public MatchService(IAspNetService<IConfiguration> settings, ILogger<MatchService> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }
        
        public Task<int> SaveMatchAsync(Match match)
        {
            var context = _settings.GetBusinessLogicContext();

            var bl = new BLMatchen(context);
            var rowsAffected = 0;
            /*if (match.Id > 0)
                rowsAffected = bl.UpdateSpeler(match);
            else*/
                rowsAffected = bl.CreateMatch(match);

            return Task.FromResult(rowsAffected);
        }
    }
}
