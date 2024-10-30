using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Available
{
    public int Id { get; set; }

    public DateTime Datetime { get; set; }

    public int? DoctorId { get; set; }

    public virtual Doctor? Doctor { get; set; }
}
