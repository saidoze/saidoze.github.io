using Quickybakkers.Service.Models;
using RA.Core.Services.Hosting;
using RA.Services.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quickybakkers.Service.Interfaces
{
    [ServiceRoute("Quickybakkers")]
    [ServiceName("Spelers")]
    public interface ISpelerService : IRestService
    {
        [ServiceMethod("GET", "GetSpelers")]
        Task<List<Speler>> GetSpelersAsync();

        [ServiceMethod("GET", "GetSpeler/{id}")]
        Task<Speler> GetSpelerByIdAsync(int id);
        
        [ServiceMethod("PUT", "UpdateSpeler/{id}")]
        Task<int> UpdateSpelerAsync(int id, Speler speler);

        [ServiceMethod("POST", "SaveSpeler")]
        Task<int> SaveSpelerAsync(Speler speler);

        [ServiceMethod("DELETE", "DeleteSpeler/{id}")]
        Task<int> DeleteSpelerAsync(int id);
    }
}
