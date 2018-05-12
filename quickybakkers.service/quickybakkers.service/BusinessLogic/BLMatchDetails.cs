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
    public class BLMatchDetails
    {
        public readonly DALMatchDetails _dal;
        public readonly BusinessLogicContext _context;

        public BLMatchDetails(BusinessLogicContext context)
        {
            this._context = context;
            this._dal = new DALMatchDetails(context?.DataAccessContext);
        }

        public virtual List<MatchDetail> GetAll(int? speeldagId)
        {
            return _dal.EnumerateAll(speeldagId).ToList();
        }
    }
}
