using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using ViewModels;

namespace Services.Medicals;
public class MedicalService(OmsContext context) : IMedicalService
{
    private readonly OmsContext _context = context;

    public async Task<bool> VerifyServiceAsync(RegisterExaminationModel request)
    {
        try
        {
            var examination = MapToExamination(request);
            _context.Examinations.Add(examination);
            return await _context.SaveChangesAsync() > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static Examination MapToExamination(RegisterExaminationModel request)
        => new()
        {
            RegisterDate = request.RegisterDate,
            ServiceId = request.ServiceId,
            PatientName = request.PatientName,
            Email = request.Email,
            Phone = request.Phone,
            Age = request.Age,
            Status = request.Status,
            Symptoms = request.Symptoms,
            Reasons = request.Reasons,
            DoctorId = request.DoctorId ?? 0,
            Diagnose = "The patient presents with symptoms consistent with a mild respiratory infection, likely viral in origin.",
            Results = "Physical examination shows clear lungs with no wheezing or crackling sounds. Mild nasal congestion and throat redness observed.",
            Instructions = "Take the prescribed medication (500 mg acetaminophen) every 8 hours as needed for pain relief. Increase fluid intake, avoid cold drinks, and rest as much as possible. Avoid strenuous physical activities and try to stay indoors. Use a humidifier to ease breathing if needed.",
            Assessments = "Symptoms align with a mild viral respiratory infection. The patient is advised to monitor symptoms, and if they worsen or if there is no improvement within 3-5 days, a follow-up consultation is recommended to reassess for possible bacterial involvement or further complications."
        };

    public async Task<List<ServiceModel>> ListAsync()
        => await _context.MedicalServices.Select(x => new ServiceModel(x.Id, x.Name)).ToListAsync();
}
