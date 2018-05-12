using Quickybakkers.Service.Models;
using RA.Core.Services.Hosting;
using RA.Services.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quickybakkers.Service.Interfaces
{
    [ServiceRoute("Quickybakkers")]
    [ServiceName("MatchDetail")]
    public interface IMatchDetailService : IRestService
    {
        [ServiceMethod("GET", "GetMatchDetails/{SpeeldagId}")]
        Task<List<MatchDetail>> GetMatchDetails(int? SpeeldagId);
    }
}
