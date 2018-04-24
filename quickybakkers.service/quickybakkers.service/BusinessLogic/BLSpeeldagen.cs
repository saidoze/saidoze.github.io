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
    public class BLSpeeldagen
    {
        public readonly BLSpeeldagenSpelers _blSpeeldagenSpelers;
        public readonly DALSpeeldagen _dal;
        public readonly BusinessLogicContext _context;

        public BLSpeeldagen(BusinessLogicContext context)
        {
            this._context = context;
            this._dal = new DALSpeeldagen(context?.DataAccessContext);
            this._blSpeeldagenSpelers = new BLSpeeldagenSpelers(context);
        }

        public virtual List<Speeldag> GetAll()
        {
            return _dal.EnumerateAll().ToList();
        }

        public virtual int CreateSpeeldag(Speeldag speeldag)
        {
            return _dal.Create(speeldag);
        }

        public virtual int UpdateSpeeldag(Speeldag speeldag)
        {
            return _dal.Update(speeldag);
        }

        public virtual int DeleteSpeeldag(int id)
        {
            return _dal.Delete(id);
        }

        public virtual bool SluitSpeeldag(int speeldagId, List<int> spelerIds)
        {
            //save speler ids
            var rowsAffected = this._blSpeeldagenSpelers.CreateSpeeldagenSpelers(speeldagId, spelerIds);

            //update speeldag
            return _dal.SluitSpeeldag(speeldagId);
        }
    }
}
