using System;

namespace MigsModernization.Models
{
    public partial class Modernization
    {
        public long Id { get; set; }
        public string ModernizationName { get; set; }
        public Boolean Performed { get; set; }
        public DateTime? PlannedBy { get; set; }
        public long MigSideNumber { get; set; }

        public Mig MigSideNumberNavigation { get; set; }
    }
}
