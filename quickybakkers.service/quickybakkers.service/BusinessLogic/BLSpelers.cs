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
    public class BLSpelers
    {
        public readonly DALSpelers _dal;
        public readonly BusinessLogicContext _context;

        public BLSpelers(BusinessLogicContext context)
        {
            this._context = context;
            this._dal = new DALSpelers(context?.DataAccessContext);
        }

        public virtual List<Speler> GetAll()
        {
            return _dal.EnumerateAll().ToList();
        }

        public virtual int CreateSpeler(Speler speler)
        {
            return _dal.Create(speler);
        }

        public virtual int UpdateSpeler(Speler speler)
        {
            return _dal.Update(speler);
        }

        public virtual int DeleteSpeler(int id)
        {
            return _dal.Delete(id);
        }
    }
}
