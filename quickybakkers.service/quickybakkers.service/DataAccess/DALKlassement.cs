using Actemium.DataAccess;
using Actemium.DataAccess.Queries;
using MySql.Data.MySqlClient;
using Quickybakkers.Service.Config;
using Quickybakkers.Service.Extensions;
using Quickybakkers.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quickybakkers.Service.DataAccess
{
    public class DALKlassement
    {
        public readonly DataAccessContext context;
        public Func<Where, IWhereExpression> Where { get; set; }

        public DALKlassement(DataAccessContext context = null)
        {
            this.context = context;
        }

        public virtual IEnumerable<Klassement> EnumerateAll()
        {
            SQLHelper s = new SQLHelper();
            var l = s.EnumerateAll<Klassement>(context.ConnectionString, TableMetadata.VW_KLASSEMENT);
            return l;
        }
    }
}
