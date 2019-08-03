using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.Models;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Services
{
    public interface IEmailBuilder
    {
        Task<SendGridMessage> Build(EmailTemplateName template, User user);
    }
}
