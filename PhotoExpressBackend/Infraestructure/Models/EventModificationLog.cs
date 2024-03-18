using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Infraestructure.Models;

public partial class EventModificationLog
{
    public Guid ModificationId { get; set; }

    public Guid? EventId { get; set; }

    public DateTime ModificationDate { get; set; }

    public string? EventBefore { get; set; }

    public string? EventAfter { get; set; }
    [JsonIgnore]
    public virtual Event? Event { get; set; }
}
