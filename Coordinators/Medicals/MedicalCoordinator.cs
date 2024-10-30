using Services.Medicals;
using ViewModels;

namespace Coordinators.Medicals;
public class MedicalCoordinator(IMedicalService service) : IMedicalCoordinator
{
    private readonly IMedicalService _service =service;
    public async Task<List<ServiceModel>> ListAsync()
        => await _service.ListAsync();
}
