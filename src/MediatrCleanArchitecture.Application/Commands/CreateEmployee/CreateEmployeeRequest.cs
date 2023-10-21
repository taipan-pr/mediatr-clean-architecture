using MediatR;

namespace MediatrCleanArchitecture.Application.Commands.CreateEmployee;

public record CreateEmployeeRequest : IRequest<CreateEmployeeResponse>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
