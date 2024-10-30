using Services.Doctors;
using Services.Medicals;
using Services.Records;
using ViewModels;

namespace Coordinators.Patients;
public class PatientCoordinator(
    IMedicalService medicalService, 
    IDoctorAvailableService doctorAvailableService,
    IMedicalRecordService medicalRecordService) : IPatientCoordinator
{
    private readonly IMedicalService _medicalService = medicalService;
    private readonly IDoctorAvailableService _doctorAvailableService = doctorAvailableService;
    private readonly IMedicalRecordService _medicalRecordService = medicalRecordService;

    public async Task<MedicalRecordModel?> GetMedicalRecordAsync(int id)
        => await _medicalRecordService.GetAsync(id);

    public async Task<List<MedicalRecordModel>> ListAsync()
        => await _medicalRecordService.ListAsync();

    public async Task<ResponseModel> RegisterAsync(RegisterExaminationModel request)
    {
        try
        {
            var availableDoctor = await _doctorAvailableService.CheckDoctorAvailableAsync(request.RegisterDate);
            if (availableDoctor is null || availableDoctor == 0)
                return new ResponseModel(false, $"There are no doctors available at {request.RegisterDate}.");

            request.DoctorId = availableDoctor;
            request.Status = true;
            var verifyService = await _medicalService.VerifyServiceAsync(request);
            if (verifyService is false) throw new Exception();

            return new ResponseModel(true, "Register successfully.");
        }
        catch (Exception)
        {
            return new ResponseModel(false, "Incomplete, re-enter info.");
        }

    }
}
