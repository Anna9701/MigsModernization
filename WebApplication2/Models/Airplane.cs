using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MigsModernization.Models
{
    public class Airplane
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Version { get; set; }
        public String Type { get; set; }

        public override string ToString()
        {
            return String.Format("{0}, {1}", Name, Version);
        }

        public ICollection<Mig> Migs { get; set; }
    }
}
