using KidsToyHive.Core.Enums;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Services;

public interface IEmailBuilder
{
    Task<SendGridMessage> Build(EmailTemplateName template, Dictionary<string, string> items);
}
