using MediatR;

namespace MediatrCleanArchitecture.Application.Commands.CreateEmployee;

public class CreateEmployeeNotification : INotification
{
    public required string Id { get; set; }
}
