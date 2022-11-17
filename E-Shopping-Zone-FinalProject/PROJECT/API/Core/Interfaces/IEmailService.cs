using Core.Entities;

namespace Core.Interfaces
{
    public interface IEmailService
    {
        Task SendTestEmail(EmailServiceOptions emailService);
    }
}