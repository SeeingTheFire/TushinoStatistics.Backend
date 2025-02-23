using Domain.Statistics.Entities;

namespace Components.Statistics.Services.Interfases;

public interface IMedicalService
{
    Task AddMedicalInfos(List<MedicalInfo> medicalInfos);
}