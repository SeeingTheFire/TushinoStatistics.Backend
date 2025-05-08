using Domain.Statistics.Entities;

namespace Components.Statistics.Services.Interfaces;

public interface IMedicalService
{
    Task AddMedicalInfos(List<MedicalInfo> medicalInfos);
}