namespace Backend.Statistics.Interfaces;

public interface IAuthService
{
    string GenerateJwtToken(string username, string password);

    bool CheckUserCredantionals(string? email, string? password);
}