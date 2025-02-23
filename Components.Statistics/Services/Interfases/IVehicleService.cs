using Domain.Statistics.Entities;

namespace Components.Statistics.Services.Interfases;

public interface IVehicleService
{
    Task UpdateVehicles(List<Vehicle> vehicles);
}