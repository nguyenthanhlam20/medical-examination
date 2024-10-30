using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Doctor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public int YearOfExperience { get; set; }

    public virtual ICollection<Available> Availables { get; set; } = new List<Available>();

    public virtual ICollection<Examination> Examinations { get; set; } = new List<Examination>();

    public virtual ICollection<MedicalService> Services { get; set; } = new List<MedicalService>();
}
