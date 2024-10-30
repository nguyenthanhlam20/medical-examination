using ViewModels;

namespace Coordinators.Patients;
public interface IPatientCoordinator
{
    Task<ResponseModel> RegisterAsync(RegisterExaminationModel request);

    Task<MedicalRecordModel?> GetMedicalRecordAsync(int id);

    Task<List<MedicalRecordModel>> ListAsync();
}
