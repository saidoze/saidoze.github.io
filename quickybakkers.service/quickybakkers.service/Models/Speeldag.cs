using System;

namespace Quickybakkers.Service.Models
{
    public class Speeldag
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public bool Gesloten { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
