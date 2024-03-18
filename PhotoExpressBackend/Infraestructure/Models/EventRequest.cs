using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    public class EventRequest
    {
        
        public Guid? InstitutionId { get; set; }

        public int NumberOfStudents { get; set; }

        public DateTime StartTime { get; set; }

        public bool CapAndGown { get; set; }

        public decimal ServiceCost { get; set; }
        public string? InstitutionName { get; set; }

        public string? InstitutionAddress { get; set; }
    }
}
