using System;
using System.Collections.Generic;
using MigsModernization.Models;

namespace MigsModernization.Models
{
    public partial class Modernization
    {
        public long Id { get; set; }
        public string ModernizationName { get; set; }
        public sbyte Performed { get; set; }
        public DateTime PlannedBy { get; set; }
        public long MigSideNumber { get; set; }

        public Mig MigSideNumberNavigation { get; set; }
    }
}
