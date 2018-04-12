using MySql.Data.MySqlClient;
using Quickybakkers.Service.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quickybakkers.Service.Extensions
{
    public static class GeneralExtensions
    {
        public static IEnumerable<MySqlParameter> AddFixedColumns(this IEnumerable<MySqlParameter> parameters)
        {
            foreach (var value in parameters)
                yield return value;

            yield return new MySqlParameter(string.Format("@{0}", TableMetadata.COL_LASTUPDATEDATE), DateTime.Now);
        }
    }
}
