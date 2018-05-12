using System;

namespace Quickybakkers.Service.Models
{
    public class Klassement
    {
        public int SpelerId { get; set; }
        public string Naam { get; set; }
        public int Team1Speler1Ptn { get; set; }
        public int Team1Speler2Ptn { get; set; }
        public int Team2Speler1Ptn { get; set; }
        public int Team2Speler2Ptn { get; set; }
        public int SpelerWin { get; set; }
        public int SpelerGelijk { get; set; }
        public int SpelerVerloren { get; set; }
        public int TotaalPunten { get; set; }
        public int TeamScoreVoor { get; set; }
        public int TeamScoreTegen { get; set; }
        public int Aanwezigheidspunten { get; set; }

        public int PuntenMetAanwezigheid { get { return TotaalPunten + Aanwezigheidspunten; } }
    }
}
