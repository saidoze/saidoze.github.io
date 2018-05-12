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
    public class DALSpeeldagenSpelers
    {
        public readonly DataAccessContext context;
        public Func<Where, IWhereExpression> Where { get; set; }

        public DALSpeeldagenSpelers(DataAccessContext context = null)
        {
            this.context = context;
        }
        
        public virtual int Create(int speeldagId, List<int> spelerIds)
        {
            SQLHelper s = new SQLHelper();
            List<MySqlParameter> mySqlParameters;
            int rowsAffected = 0;

            foreach (int spelerId in spelerIds)
            {
                mySqlParameters = new List<MySqlParameter>() {
                    new MySqlParameter("@SpeeldagId", speeldagId),
                    new MySqlParameter("@SpelerId", spelerId)
                };
                var l = s.Create(context.ConnectionString, TableMetadata.TBL_SPEELDAGEN_SPELERS, mySqlParameters);
                rowsAffected += l;
            }
            
            return rowsAffected;
        }
        
        //public virtual int Delete(int speeldagId)
        //{
        //    SQLHelper s = new SQLHelper();
        //    var ids = new List<int>() {
        //        id
        //    };
        //    var l = s.Delete(context.ConnectionString, TableMetadata.TBL_SPEELDAGEN_SPELERS, ids);
        //    return l;
        //}
    }
}
