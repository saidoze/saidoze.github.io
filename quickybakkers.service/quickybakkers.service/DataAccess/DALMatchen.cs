using Actemium.DataAccess;
using Actemium.DataAccess.Queries;
using MySql.Data.MySqlClient;
using Quickybakkers.Service.Config;
using Quickybakkers.Service.Extensions;
using Quickybakkers.Service.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quickybakkers.Service.DataAccess
{
    public class DALMatchen
    {
        public readonly DataAccessContext context;
        public Func<Where, IWhereExpression> Where { get; set; }

        public DALMatchen(DataAccessContext context = null)
        {
            this.context = context;
        }

        //public virtual IEnumerable<Speeldag> EnumerateAll()
        //{
        //    SQLHelper s = new SQLHelper();
        //    var l = s.EnumerateAll<Speeldag>(context.ConnectionString, TableMetadata.TBL_SPEELDAG);
        //    return l;
        //}
        
        public virtual int Create(Match match)
        {
            SQLHelper s = new SQLHelper();
            var mySqlParameters = new List<MySqlParameter>() {
                new MySqlParameter("@Team1Id", match.Team1Id),
                new MySqlParameter("@Team2Id", match.Team2Id),
                new MySqlParameter("@Team1Score", match.Team1Score),
                new MySqlParameter("@Team2Score", match.Team2Score),
                new MySqlParameter("@SpeeldagId", match.SpeeldagId)
            };
            var l = s.Create(context.ConnectionString, TableMetadata.TBL_MATCHEN, mySqlParameters);
            return l;
        }

        //public virtual int Update(Speeldag speeldag)
        //{
        //    SQLHelper s = new SQLHelper();
        //    var mySqlParameters = new List<MySqlParameter>() {
        //        new MySqlParameter("@Datum", speeldag.Datum),
        //        new MySqlParameter("@Gesloten", speeldag.Gesloten)
        //    };
        //    var l = s.Update(context.ConnectionString, TableMetadata.TBL_SPEELDAG, speeldag.Id, mySqlParameters);
        //    return l;
        //}

        //public virtual int Delete(int id)
        //{
        //    SQLHelper s = new SQLHelper();
        //    var ids = new List<int>() {
        //        id
        //    };
        //    var l = s.Delete(context.ConnectionString, TableMetadata.TBL_SPEELDAG, ids);
        //    return l;
        //}
    }
}
