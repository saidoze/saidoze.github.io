using Actemium.BusinessLogic;
using Actemium.DataAccess;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quickybakkers.Service.Extensions
{
    public static class IConfigurationExtensions
    {
        public static BusinessLogicContext GetBusinessLogicContext(this IConfiguration configuration, string name = "DefaultConnection")
        {
            var connectionString = configuration.GetConnectionString(name);
            return new BusinessLogicContext()
            {
                DataAccessContext = new DataAccessContext(connectionString)
            };
        }
    }
}
