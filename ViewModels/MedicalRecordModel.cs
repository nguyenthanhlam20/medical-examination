namespace ViewModels;
public class MedicalRecordModel
{
    public int Id { get; set; }
    public DateTime RegisterDate { get; set; }

    public int ServiceId { get; set; }

    public string PatientName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int Age { get; set; }

    public bool Status { get; set; }

    public string? Assessments { get; set; }

    public string? Instructions { get; set; }

    public string Symptoms { get; set; } = null!;

    public string Reasons { get; set; } = null!;

    public string? Diagnose { get; set; }

    public string? Results { get; set; }

    public int DoctorId { get; set; }

    public DoctorModel Doctor { get; set; } = null!;

    public ServiceModel Service { get; set; } = null!;
}
