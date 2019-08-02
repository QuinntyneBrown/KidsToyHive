using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.Models;
using SendGrid.Helpers.Mail;

namespace KidsToyHive.Domain.Services
{
    public interface IEmailBuilder
    {
        SendGridMessage Build(EmailTemplateName template, User user);
    }
}
