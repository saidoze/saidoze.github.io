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
    public class BLMatchen
    {
        public readonly BLTeams _blTeams;
        public readonly DALMatchen _dal;
        public readonly BusinessLogicContext _context;

        public BLMatchen(BusinessLogicContext context)
        {
            this._context = context;
            this._blTeams = new BLTeams(context);
            this._dal = new DALMatchen(context?.DataAccessContext);
        }
        
        public virtual int CreateMatch(Match match)
        {
            //create teams
            match.Team1Id = _blTeams.CreateTeam(new Team()
            {
                Speler1Id = match.Team1Speler1Id,
                Speler2Id = match.Team1Speler2Id,
                Speler1PuntenToelaten = match.Team1Speler1PuntenToelaten,
                Speler2PuntenToelaten = match.Team1Speler2PuntenToelaten
            }, true);

            match.Team2Id = _blTeams.CreateTeam(new Team()
            {
                Speler1Id = match.Team2Speler1Id,
                Speler2Id = match.Team2Speler2Id,
                Speler1PuntenToelaten = match.Team2Speler1PuntenToelaten,
                Speler2PuntenToelaten = match.Team2Speler2PuntenToelaten
            }, true);

            //create match
            var rowsAffected = _dal.Create(match);

            return rowsAffected;
        }

        //public virtual int UpdateSpeler(Speler speler)
        //{
        //    return _dal.Update(speler);
        //}
    }
}
