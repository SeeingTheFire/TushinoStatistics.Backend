using Components.Statistics.Services.Interfaces;
using DataBase.Statistics.Repositories.Interfaces;
using Domain.Statistics.Entities;

namespace Components.Statistics.Services.DomainServices;

public class VehicleService(IVehicleRepository vehicleRepository) : IVehicleService
{
    public Task UpdateVehicles(List<Vehicle> vehicles)
    {
        //TODO: Доделать что-то пока нету идеи
        return Task.CompletedTask;
    }
}