using EXM.Common.Models.Mails.Requests;

namespace EXM.Services.Contracts
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}
