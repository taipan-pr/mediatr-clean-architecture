namespace MediatrCleanArchitecture.Application.Queries.GetAllEmployee;

public record GetAllEmployeeResponse
{
    public required string Id { get; set; }
    public required string FullName { get; set; }
}
