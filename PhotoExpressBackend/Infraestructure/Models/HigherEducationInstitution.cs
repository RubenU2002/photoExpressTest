using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Infraestructure.Models;

public partial class HigherEducationInstitution
{
    public Guid InstitutionId { get; set; }

    public string InstitutionName { get; set; } = null!;

    public string InstitutionAddress { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
