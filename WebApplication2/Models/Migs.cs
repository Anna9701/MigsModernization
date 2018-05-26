using System;
using System.Collections.Generic;

namespace MigsModernization.Models
{
    public partial class Mig
    {
        public Mig()
        {
            Modernizations = new HashSet<Modernization>();
        }

        public long SideNumber { get; set; }
        public string Airplane { get; set; }
        public double Version { get; set; }
        public string Type { get; set; }
        public string Unit { get; set; }
        public string StagingArea { get; set; }
        public string Notes { get; set; }

        public ICollection<Modernization> Modernizations { get; set; }
    }
}
