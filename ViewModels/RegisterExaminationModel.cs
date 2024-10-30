using System.ComponentModel.DataAnnotations;

namespace ViewModels;
public class RegisterExaminationModel
{
    [Required(ErrorMessage = "Please select a register date and time.")]
    [FutureDate(ErrorMessage = "Register date must be in the future.")]
    public DateTime RegisterDate { get; set; }

    [Required(ErrorMessage = "Please select a service.")]
    public int ServiceId { get; set; }

    [Required(ErrorMessage = "Patient name is required.")]
    public string PatientName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Age is required.")]
    [Range(0, 120, ErrorMessage = "Please enter a valid age.")]
    public int Age { get; set; }

    public bool Status { get; set; }

    [Required(ErrorMessage = "Please provide symptoms.")]
    public string Symptoms { get; set; } = null!;

    [Required(ErrorMessage = "Please provide the reason for examination.")]
    public string Reasons { get; set; } = null!;

    public int? DoctorId { get; set; }


    public override string ToString()
    {
        return
            $"You have successfully register medical examination.\n" +
            $"Register Date: {RegisterDate:yyyy-MM-dd hh:mm:ss tt}\n" +
               $"Patient Name: {PatientName}\n" +
               $"Email: {Email}\n" +
               $"Phone: {Phone}\n" +
               $"Age: {Age}\n" +
               $"Symptoms: {Symptoms}\n" +
               $"Reasons: {Reasons}";
    }
}

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is DateTime dateTime)
        {
            return dateTime > DateTime.Now;
        }
        return false;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"{name} must be in the future.";
    }
}