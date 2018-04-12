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
    public class DALSpelers
    {
        public readonly DataAccessContext context;
        public Func<Where, IWhereExpression> Where { get; set; }

        public DALSpelers(DataAccessContext context = null)
        {
            this.context = context;
        }

        public virtual IEnumerable<Speler> EnumerateAll()
        {
            SQLHelper s = new SQLHelper();
            var l = s.EnumerateAll<Speler>(context.ConnectionString, TableMetadata.TBL_SPELERS);
            return l;
        }
        
        public virtual int Create(Speler speler)
        {
            SQLHelper s = new SQLHelper();
            var mySqlParameters = new List<MySqlParameter>() {
                new MySqlParameter("@Id", speler.Id),
                new MySqlParameter("@Naam", speler.Naam)
            };
            var l = s.Create(context.ConnectionString, TableMetadata.TBL_SPELERS, mySqlParameters);
            return l;
        }

        public virtual int Update(Speler speler)
        {
            SQLHelper s = new SQLHelper();
            var mySqlParameters = new List<MySqlParameter>() {
                new MySqlParameter("@Naam", speler.Naam)
            };
            var l = s.Update(context.ConnectionString, TableMetadata.TBL_SPELERS, speler.Id, mySqlParameters);
            return l;
        }

        public virtual int Delete(int id)
        {
            SQLHelper s = new SQLHelper();
            var ids = new List<int>() {
                id
            };
            var l = s.Delete(context.ConnectionString, TableMetadata.TBL_SPELERS, ids);
            return l;
        }
    }
}
