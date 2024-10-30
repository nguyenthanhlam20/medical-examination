using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Doctors;
public class DoctorAvailableService(OmsContext context) : IDoctorAvailableService
{
    private readonly OmsContext _context = context;

    public async Task<int?> CheckDoctorAvailableAsync(DateTime registerDate)
    {
		try
		{
            var availableDoctors = await _context.Availables
               .Join(_context.Doctors,
                   available => available.DoctorId,
                   doctor => doctor.Id,
                   (available, doctor) => new { doctorId = doctor.Id, availableTime = available.Datetime })
               .Where(x => x.availableTime == registerDate)
               .Select(x => x.doctorId).ToListAsync();

            var busyDoctors = await _context.Examinations.Select(x => x.DoctorId).ToListAsync();
            return availableDoctors.Except(busyDoctors).FirstOrDefault();
        }
		catch (Exception)
		{
            return null;
		}
    }
}
