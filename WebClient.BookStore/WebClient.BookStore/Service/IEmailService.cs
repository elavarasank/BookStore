using System.Threading.Tasks;
using WebClient.BookStore.Models;

namespace WebClient.BookStore.Service
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);
        Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions);
        Task SendEmailForgotPassword(UserEmailOptions userEmailOptions);
    }
}