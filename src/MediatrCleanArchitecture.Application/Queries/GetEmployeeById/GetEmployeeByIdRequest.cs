using MediatR;

namespace MediatrCleanArchitecture.Application.Queries.GetEmployeeById;

public record GetEmployeeByIdRequest : IRequest<GetEmployeeByIdResponse?>
{
    public required string Id { get; init; }
}
