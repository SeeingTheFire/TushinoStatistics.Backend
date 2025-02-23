using DataBase.Statistics.Repositories.Interfaces;
using Domain.Statistics.Entities;

namespace DataBase.Statistics.Repositories;

public class MedicalRepository(ApplicationContext context) : IMedicalRepository
{
    public void AddRange(List<MedicalInfo> medicalInfos)
    {
        context.MedicineInfo.AddRange(medicalInfos);
    }
}