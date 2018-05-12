using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quickybakkers.Service.Config
{
    public static class TableMetadata
    {
        public const string TBL_SPELERS = "spelers";
        public const string TBL_SPEELDAGEN = "speeldagen";
        public const string TBL_SPEELDAGEN_SPELERS = "speeldagen_spelers";
        public const string TBL_MATCHEN = "matchen";
        public const string TBL_TEAMS = "teams";

        public const string VW_MATCHDETAILS = "vwresultatenoverzicht";
        public const string VW_KLASSEMENT = "vwklassement";

        public const string COL_LASTUPDATEDATE = "LastUpdateDate";
    }
}
