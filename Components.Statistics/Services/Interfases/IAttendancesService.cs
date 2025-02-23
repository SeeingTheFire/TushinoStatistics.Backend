using Domain.Statistics.Entities;

namespace Components.Statistics.Services.Interfases;

public interface IAttendancesService
{
    Task AddAttendances(List<Attendance> attendances);
}