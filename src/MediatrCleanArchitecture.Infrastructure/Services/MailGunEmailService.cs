using MediatrCleanArchitecture.Application.DataTransferObjects;
using MediatrCleanArchitecture.Application.Interfaces;

namespace MediatrCleanArchitecture.Infrastructure.Services;

public class MailGunEmailService : IEmailService
{
    public async Task<SendEmailResult> Send()
    {
        await Task.CompletedTask;
        return new()
        {
            Message = "Email was sent by MailGun"
        };
    }
}
