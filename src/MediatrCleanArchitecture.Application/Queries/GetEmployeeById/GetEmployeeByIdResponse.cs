namespace MediatrCleanArchitecture.Application.Queries.GetEmployeeById;

public record GetEmployeeByIdResponse
{
    public required string Id { get; set; }
    public required string FullName { get; set; }
}
