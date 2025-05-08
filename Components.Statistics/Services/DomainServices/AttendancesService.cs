using Components.Statistics.Services.Interfaces;
using DataBase.Statistics.Repositories.Interfaces;
using Domain.Statistics.Entities;

namespace Components.Statistics.Services.DomainServices;

public class AttendancesService(IAttendancesRepository attendancesRepository) : IAttendancesService
{
    public Task AddAttendances(List<Attendance> attendances)
    {
        attendancesRepository.AddRange(attendances);
        return Task.CompletedTask;
    }
}