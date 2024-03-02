namespace KlidecekIS.DAL.DalOptions;


public record DalOptions
{
    public required string DatabaseDirectory { get; init; }
    private string DatabaseName { get; init; } = null!;
    public string DatabaseFilePath => Path.Combine(DatabaseDirectory, DatabaseName!);
    public bool RecreateDatabaseEachTime { get; init; } = false;

    
}