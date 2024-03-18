using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Infraestructure.Models;

public partial class Event
{
    public Guid EventId { get; set; }

    public Guid? InstitutionId { get; set; }

    public int NumberOfStudents { get; set; }

    public DateTime StartTime { get; set; }

    public decimal ServiceCost { get; set; }

    public bool? CapAndGown { get; set; }
    [JsonIgnore]
    public virtual ICollection<EventModificationLog> EventModificationLogs { get; set; } = new List<EventModificationLog>();

    public virtual HigherEducationInstitution? Institution { get; set; }
}
