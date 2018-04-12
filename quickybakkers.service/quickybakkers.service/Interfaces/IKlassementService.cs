using Quickybakkers.Service.Models;
using RA.Core.Services.Hosting;
using RA.Services.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quickybakkers.Service.Interfaces
{
    [ServiceRoute("Quickybakkers")]
    [ServiceName("Klassement")]
    public interface IKlassementService : IRestService
    {
        [ServiceMethod("GET", "GetKlassement")]
        Task<Klassement> GetKlassementAsync();
    }
}
