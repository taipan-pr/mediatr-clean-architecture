using MediatrCleanArchitecture.Application.DataTransferObjects;
using MediatrCleanArchitecture.Application.Interfaces;

namespace MediatrCleanArchitecture.Infrastructure.Services;

public class SendGridEmailService : IEmailService
{
    public async Task<SendEmailResult> Send()
    {
        await Task.CompletedTask;
        return new()
        {
            Message = "Email was sent by SendGrid"
        };
    }
}
