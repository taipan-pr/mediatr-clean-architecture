using MediatR;
using MediatrCleanArchitecture.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediatrCleanArchitecture.Application.Queries.GetEmployeeById;

internal class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdRequest, GetEmployeeByIdResponse?>
{
    private readonly ILogger _logger;
    private readonly IDbContext _dbContext;

    public GetEmployeeByIdHandler(ILogger logger, IDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<GetEmployeeByIdResponse?> Handle(GetEmployeeByIdRequest request, CancellationToken cancellationToken)
    {
        _logger.Debug("Calling from - {Action}", nameof(GetEmployeeByIdHandler));
        var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        return employee is null
            ? default
            : new GetEmployeeByIdResponse
            {
                Id = employee.Id,
                FullName = $"{employee.FirstName} {employee.LastName}"
            };
    }
}
