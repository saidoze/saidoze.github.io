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
    public class BLSpeeldagenSpelers
    {
        public readonly DALSpeeldagenSpelers _dal;
        public readonly BusinessLogicContext _context;

        public BLSpeeldagenSpelers(BusinessLogicContext context)
        {
            this._context = context;
            this._dal = new DALSpeeldagenSpelers(context?.DataAccessContext);
        }
        
        public virtual int CreateSpeeldagenSpelers(int speeldagId, List<int> spelerIds)
        {
            return _dal.Create(speeldagId, spelerIds);
        }
        
    }
}
