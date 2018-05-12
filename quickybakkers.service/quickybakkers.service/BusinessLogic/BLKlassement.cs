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
    public class BLKlassement
    {
        public readonly DALKlassement _dal;
        public readonly BusinessLogicContext _context;

        public BLKlassement(BusinessLogicContext context)
        {
            this._context = context;
            this._dal = new DALKlassement(context?.DataAccessContext);
        }

        public virtual List<Klassement> GetAll()
        {
            return _dal.EnumerateAll().ToList();
        }
    }
}
