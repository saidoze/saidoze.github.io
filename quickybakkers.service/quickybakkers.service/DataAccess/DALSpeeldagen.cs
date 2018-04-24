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
    public class DALSpeeldagen
    {
        public readonly DataAccessContext context;
        public Func<Where, IWhereExpression> Where { get; set; }

        public DALSpeeldagen(DataAccessContext context = null)
        {
            this.context = context;
        }

        public virtual IEnumerable<Speeldag> EnumerateAll()
        {
            SQLHelper s = new SQLHelper();
            var l = s.EnumerateAll<Speeldag>(context.ConnectionString, TableMetadata.TBL_SPEELDAGEN);
            return l;
        }
        
        public virtual int Create(Speeldag speeldag)
        {
            SQLHelper s = new SQLHelper();
            var mySqlParameters = new List<MySqlParameter>() {
                new MySqlParameter("@Datum", speeldag.Datum),
                new MySqlParameter("@Gesloten", speeldag.Gesloten)
            };
            var l = s.Create(context.ConnectionString, TableMetadata.TBL_SPEELDAGEN, mySqlParameters);
            return l;
        }

        public virtual int Update(Speeldag speeldag)
        {
            SQLHelper s = new SQLHelper();
            var mySqlParameters = new List<MySqlParameter>() {
                new MySqlParameter("@Datum", speeldag.Datum),
                new MySqlParameter("@Gesloten", speeldag.Gesloten)
            };
            var l = s.Update(context.ConnectionString, TableMetadata.TBL_SPEELDAGEN, speeldag.Id, mySqlParameters);
            return l;
        }

        public virtual bool SluitSpeeldag(int speeldagId)
        {
            SQLHelper s = new SQLHelper();
            var mySqlParameters = new List<MySqlParameter>() {
                new MySqlParameter("@Gesloten", true)
            };
            var l = s.Update(context.ConnectionString, TableMetadata.TBL_SPEELDAGEN, speeldagId, mySqlParameters);
            return l > 0;
        }

        public virtual int Delete(int id)
        {
            SQLHelper s = new SQLHelper();
            var ids = new List<int>() {
                id
            };
            var l = s.Delete(context.ConnectionString, TableMetadata.TBL_SPEELDAGEN, ids);
            return l;
        }
    }
}
