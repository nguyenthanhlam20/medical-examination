using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using ViewModels;

namespace Services.Records;
public class MedicalRecordService(OmsContext context) : IMedicalRecordService
{
    private readonly OmsContext _context = context;
    public async Task<MedicalRecordModel?> GetAsync(int id)
    {
        var history = await _context.Examinations
                    .Include(x => x.Doctor)
                    .Include(x => x.Service)
                    .FirstOrDefaultAsync(x => x.Id == id);
        return history is null ? null : MapToMedicalRecordModel(history);
    }

    private static MedicalRecordModel MapToMedicalRecordModel(Examination model)
        => new()
        {
            Id = model.Id,
            Age = model.Age,
            Email = model.Email,
            Assessments = model.Assessments,
            Doctor = new DoctorModel(model.Doctor.Name, model.Doctor.Age, model.Doctor.YearOfExperience),
            Service = new ServiceModel(model.Service.Id, model.Service.Name),
            Instructions = model.Instructions,
            PatientName = model.PatientName,
            Phone = model.Phone,
            RegisterDate = model.RegisterDate,
            Reasons = model.Reasons,
            Symptoms = model.Symptoms,
            Status = model.Status,
            Diagnose = model.Diagnose,
            Results = model.Results,
        };

    public async Task<List<MedicalRecordModel>> ListAsync()
        => await _context.Examinations
                    .Include(x => x.Doctor)
                    .Include(x => x.Service)
                    .Select(x => MapToMedicalRecordModel(x))
                    .ToListAsync();
}
