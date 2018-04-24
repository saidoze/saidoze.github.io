using Actemium.BusinessLogic;
using Quickybakkers.Service.DataAccess;
using Quickybakkers.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quickybakkers.Service.BusinessLogic
{
    public class BLTeams
    {
        public readonly DALTeams _dal;
        public readonly BusinessLogicContext _context;

        public BLTeams(BusinessLogicContext context)
        {
            this._context = context;
            this._dal = new DALTeams(context?.DataAccessContext);
        }

        public virtual List<Team> GetAll()
        {
            return _dal.EnumerateAll().ToList();
        }

        public virtual int CreateTeam(Team team, bool returnLastInsertedRowId)
        {
            return _dal.Create(team, returnLastInsertedRowId);
        }

        //public virtual int UpdateSpeler(Speler speler)
        //{
        //    return _dal.Update(speler);
        //}

        //public virtual int DeleteSpeler(int id)
        //{
        //    return _dal.Delete(id);
        //}
    }
}
