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
        public readonly DALSpeeldagen _dal;
        public readonly BusinessLogicContext _context;

        public BLSpeeldagen(BusinessLogicContext context)
        {
            this._context = context;
            this._dal = new DALSpeeldagen(context?.DataAccessContext);
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
    }
}
