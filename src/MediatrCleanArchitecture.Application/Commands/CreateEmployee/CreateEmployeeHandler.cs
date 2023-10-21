using MediatR;
using MediatrCleanArchitecture.Application.Interfaces;
using MediatrCleanArchitecture.Domain.Entities;

namespace MediatrCleanArchitecture.Application.Commands.CreateEmployee;

internal class CreateEmployeeHandler : IRequestHandler<CreateEmployeeRequest, CreateEmployeeResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IPublisher _publisher;

    public CreateEmployeeHandler(IDbContext dbContext, IPublisher publisher)
    {
        _dbContext = dbContext;
        _publisher = publisher;
    }

    public async Task<CreateEmployeeResponse> Handle(CreateEmployeeRequest request, CancellationToken cancellationToken)
    {
        var model = new Employee
        {
            Id = Guid.NewGuid().ToString("N"),
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        await _dbContext.Employees.AddAsync(model, cancellationToken);
        var isSuccess = await _dbContext.SaveChangesAsync() > 0;
        if(isSuccess)
        {
            await _publisher.Publish(new CreateEmployeeNotification
                {
                    Id = model.Id
                },
                cancellationToken);
        }

        return new()
        {
            IsSuccess = isSuccess
        };
    }
}
