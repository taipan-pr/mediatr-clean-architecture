using MediatrCleanArchitecture.Application.DataTransferObjects;

namespace MediatrCleanArchitecture.Application.Interfaces;

public interface IEmailService
{
    Task<SendEmailResult> Send();
}
