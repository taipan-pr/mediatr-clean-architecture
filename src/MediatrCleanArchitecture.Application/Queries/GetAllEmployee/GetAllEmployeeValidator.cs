namespace MediatrCleanArchitecture.Application.Queries.GetAllEmployee;

internal class GetAllEmployeeValidator : AbstractValidator<GetAllEmployeeRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<GetAllEmployeeRequest> context, CancellationToken cancellation = new CancellationToken())
    {
        return base.ValidateAsync(context, cancellation);
    }
}
