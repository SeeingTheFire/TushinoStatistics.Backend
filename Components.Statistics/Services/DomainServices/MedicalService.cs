using Components.Statistics.Services.Interfases;
using DataBase.Statistics.Repositories.Interfaces;
using Domain.Statistics.Entities;

namespace Components.Statistics.Services.DomainServices;

public class MedicalService(IMedicalRepository medicalRepository) : IMedicalService
{
    public Task AddMedicalInfos(List<MedicalInfo> medicalInfos)
    {
        medicalRepository.AddRange(medicalInfos);
        return Task.CompletedTask;
    }
}