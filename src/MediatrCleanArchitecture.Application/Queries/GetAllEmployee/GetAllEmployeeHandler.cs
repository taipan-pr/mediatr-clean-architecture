using MediatR;
using MediatrCleanArchitecture.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediatrCleanArchitecture.Application.Queries.GetAllEmployee;

internal class GetAllEmployeeHandler : IRequestHandler<GetAllEmployeeRequest, IEnumerable<GetAllEmployeeResponse>>
{
    private readonly ILogger _logger;
    private readonly IDbContext _dbContext;

    public GetAllEmployeeHandler(ILogger logger, IDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GetAllEmployeeResponse>> Handle(GetAllEmployeeRequest request, CancellationToken cancellationToken)
    {
        _logger.Debug("Calling from - {Action}", nameof(GetAllEmployeeHandler));
        var employees = await _dbContext.Employees.ToArrayAsync(cancellationToken: cancellationToken);
        var response = employees.Select(e => new GetAllEmployeeResponse
        {
            Id = e.Id,
            FullName = $"{e.FirstName} {e.LastName}"
        });

        return response;
    }
}
