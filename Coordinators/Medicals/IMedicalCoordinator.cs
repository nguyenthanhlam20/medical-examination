using ViewModels;

namespace Coordinators.Medicals;
public interface IMedicalCoordinator
{
    Task<List<ServiceModel>> ListAsync();
}
