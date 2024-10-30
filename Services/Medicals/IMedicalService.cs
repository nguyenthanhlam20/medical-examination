using ViewModels;

namespace Services.Medicals;
public interface IMedicalService
{
    Task<List<ServiceModel>> ListAsync();
    Task<bool> VerifyServiceAsync(RegisterExaminationModel request);
}
