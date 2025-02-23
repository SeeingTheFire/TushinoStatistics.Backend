namespace Domain.Statistics.Entities;

public class Version
{
    public Version(DateTime utcNow)
    {
        UpdateDate = utcNow;
    }
    private Version()
    {
        
    }
    
    public long VersionNumber { get; set; }
    public DateTime UpdateDate { get; set; }
}