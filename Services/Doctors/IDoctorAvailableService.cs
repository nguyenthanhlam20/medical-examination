namespace Services.Doctors;
public interface IDoctorAvailableService
{
    Task<int?> CheckDoctorAvailableAsync(DateTime registerDate);
}
