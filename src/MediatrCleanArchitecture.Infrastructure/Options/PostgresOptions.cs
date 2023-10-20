namespace MediatrCleanArchitecture.Infrastructure.Options;

internal class PostgresOptions
{
    public const string ConfigurationName = "Postgres";
    public string? ConnectionString { get; set; }
    public bool IsSeedingRequired { get; set; }
}
