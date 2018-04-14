using Quickybakkers.Service.Models;
using RA.Core.Services.Hosting;
using RA.Services.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quickybakkers.Service.Interfaces
{
    [ServiceRoute("Quickybakkers")]
    [ServiceName("Speeldag")]
    public interface ISpeeldagService : IRestService
    {
        [ServiceMethod("GET", "GetSpeeldagen")]
        Task<List<Speeldag>> GetSpeeldagenAsync();

        [ServiceMethod("GET", "GetSpeeldag/{id}")]
        Task<Speeldag> GetSpeeldagByIdAsync(int id);
        
        //[ServiceMethod("PUT", "UpdateSpeeldag/{id}")]
        //Task<int> UpdateSpeeldagAsync(int id, Speeldag speeldag);

        [ServiceMethod("POST", "SaveSpeeldag")]
        Task<int> SaveSpeeldagAsync(Speeldag speeldag);

        [ServiceMethod("DELETE", "DeleteSpeeldag/{id}")]
        Task<int> DeleteSpeeldagAsync(int id);
    }
}
