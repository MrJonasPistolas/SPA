using EXM.Base.Requests.Mail;

namespace EXM.Base.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}
