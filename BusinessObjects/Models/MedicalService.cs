using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class MedicalService
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Examination> Examinations { get; set; } = new List<Examination>();

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
