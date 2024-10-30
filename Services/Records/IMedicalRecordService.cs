using ViewModels;

namespace Services.Records;
public interface IMedicalRecordService
{
    Task<MedicalRecordModel?> GetAsync(int id); 
    Task<List<MedicalRecordModel>> ListAsync(); 
}
