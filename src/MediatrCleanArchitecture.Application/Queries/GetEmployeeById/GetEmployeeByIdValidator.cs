using MediatrCleanArchitecture.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediatrCleanArchitecture.Application.Queries.GetEmployeeById;

internal class GetEmployeeByIdValidator : AbstractValidator<GetEmployeeByIdRequest>
{
    private readonly IDbContext _dbContext;

    public GetEmployeeByIdValidator(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override Task<ValidationResult> ValidateAsync(ValidationContext<GetEmployeeByIdRequest> context, CancellationToken cancellation = new CancellationToken())
    {
        RuleFor(e => e.Id)
            .NotEmpty()
            .MustAsync(async (id, cancellationToken) =>
            {
                var isExists = await _dbContext.Employees.AnyAsync(e => e.Id == id, cancellationToken);
                return isExists;
            })
            .WithMessage("Employee does not exists");

        return base.ValidateAsync(context, cancellation);
    }
}
