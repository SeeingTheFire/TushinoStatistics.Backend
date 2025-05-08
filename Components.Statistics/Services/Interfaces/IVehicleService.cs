using Domain.Statistics.Entities;

namespace Components.Statistics.Services.Interfaces;

public interface IVehicleService
{
    Task UpdateVehicles(List<Vehicle> vehicles);
}