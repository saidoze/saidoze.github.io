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
    public class DALTeams
    {
        public readonly DataAccessContext context;
        public Func<Where, IWhereExpression> Where { get; set; }

        public DALTeams(DataAccessContext context = null)
        {
            this.context = context;
        }

        public virtual IEnumerable<Team> EnumerateAll()
        {
            SQLHelper s = new SQLHelper();
            var l = s.EnumerateAll<Team>(context.ConnectionString, TableMetadata.TBL_TEAMS);
            return l;
        }
        
        public virtual int Create(Team team, bool returnLastInsertedRowId)
        {
            SQLHelper s = new SQLHelper();
            var mySqlParameters = new List<MySqlParameter>() {
                new MySqlParameter("@Speler1Id", team.Speler1Id),
                new MySqlParameter("@Speler1PuntenToelaten", team.Speler1PuntenToelaten),
                new MySqlParameter("@Speler2Id", team.Speler2Id),
                new MySqlParameter("@Speler2PuntenToelaten", team.Speler2PuntenToelaten)
            };
            var l = s.Create(context.ConnectionString, TableMetadata.TBL_TEAMS, mySqlParameters, returnLastInsertedRowId);
            return l;
        }

        //public virtual int Update(Speler speler)
        //{
        //    SQLHelper s = new SQLHelper();
        //    var mySqlParameters = new List<MySqlParameter>() {
        //        new MySqlParameter("@Naam", speler.Naam)
        //    };
        //    var l = s.Update(context.ConnectionString, TableMetadata.TBL_SPELERS, speler.Id, mySqlParameters);
        //    return l;
        //}

        //public virtual int Delete(int id)
        //{
        //    SQLHelper s = new SQLHelper();
        //    var ids = new List<int>() {
        //        id
        //    };
        //    var l = s.Delete(context.ConnectionString, TableMetadata.TBL_SPELERS, ids);
        //    return l;
        //}
    }
}
