namespace MediatrCleanArchitecture.Application.Commands.CreateEmployee;

internal class CreateEmployeeValidator : AbstractValidator<CreateEmployeeRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<CreateEmployeeRequest> context, CancellationToken cancellation = new CancellationToken())
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(e => e.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(e => e.LastName)
            .NotEmpty()
            .MaximumLength(100);

        return base.ValidateAsync(context, cancellation);
    }
}
