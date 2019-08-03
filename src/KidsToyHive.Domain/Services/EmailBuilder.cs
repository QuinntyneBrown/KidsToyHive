using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;

namespace KidsToyHive.Domain.Services
{
    public class EmailBuilder : IEmailBuilder
    {
        private readonly IAppDbContext _context;

        public EmailBuilder(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<SendGridMessage> Build(EmailTemplateName template, Dictionary<string, string> items)
        {
            var sendGridMessage = new SendGridMessage();
            EmailTemplate emailTemplate;

            switch (template)
            {
                case EmailTemplateName.BookingConfirmation:
                    emailTemplate = await _context.EmailTemplates.SingleAsync(x => x.Name == nameof(EmailTemplateName.BookingConfirmation));
                    break;

                case EmailTemplateName.NewCustomer:
                    emailTemplate = await _context.EmailTemplates.SingleAsync(x => x.Name == nameof(EmailTemplateName.NewCustomer));
                    break;

                default:
                    throw new NotSupportedException("");
            }

            string result = emailTemplate.Value;

            foreach (var item in items)
            {
                result = result.Replace(item.Key, item.Value);
            }
            sendGridMessage.HtmlContent = result;


            return sendGridMessage;
        }
    }
}
