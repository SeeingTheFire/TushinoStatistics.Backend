using DataBase.Statistics.Repositories.Interfaces;
using Domain.Statistics.Entities;

namespace DataBase.Statistics.Repositories;

public class AttendancesRepository(ApplicationContext context) : IAttendancesRepository
{
    public void AddRange(List<Attendance> attendances)
    {
        context.Attendance.AddRange(attendances);
    }
}