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
        public long AirplaneId { get; set; }
        public string Unit { get; set; }
        public string StagingArea { get; set; }
        public string Notes { get; set; }

        public Airplane AirplaneNavigation { get; set; }
        public ICollection<Modernization> Modernizations { get; set; }
    }
}
