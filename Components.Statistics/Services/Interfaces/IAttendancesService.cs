using Domain.Statistics.Entities;

namespace Components.Statistics.Services.Interfaces;

public interface IAttendancesService
{
    Task AddAttendances(List<Attendance> attendances);
}