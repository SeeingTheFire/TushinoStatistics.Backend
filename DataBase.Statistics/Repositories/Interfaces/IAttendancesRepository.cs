using Domain.Statistics.Entities;

namespace DataBase.Statistics.Repositories.Interfaces;

public interface IAttendancesRepository
{
    void AddRange(List<Attendance> attendances);
}