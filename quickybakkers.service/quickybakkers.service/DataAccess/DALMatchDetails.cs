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
    public class DALMatchDetails
    {
        public readonly DataAccessContext context;
        public Func<Where, IWhereExpression> Where { get; set; }

        public DALMatchDetails(DataAccessContext context = null)
        {
            this.context = context;
        }

        public virtual IEnumerable<MatchDetail> EnumerateAll(int? speeldagId)
        {
            SQLHelper s = new SQLHelper();
            var l = s.EnumerateAllBy<MatchDetail>(context.ConnectionString, TableMetadata.VW_MATCHDETAILS, "SpeeldagId", speeldagId);
            return l;
        }
    }
}
