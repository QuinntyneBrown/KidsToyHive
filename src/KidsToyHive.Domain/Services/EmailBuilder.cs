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

        public async Task<SendGridMessage> Build(EmailTemplateName template, User user)
        {
            var sendGridMessage = new SendGridMessage();
            
            switch (template)
            {
                case EmailTemplateName.NewCustomer:
                    break;

                case EmailTemplateName.BookingConfirmation:
                    var emailTemplate = await _context.EmailTemplates.SingleAsync(x => x.Name == nameof(EmailTemplateName.BookingConfirmation));
                    string result = default;

                    sendGridMessage.HtmlContent = result;
                    break;

                default:
                    throw new NotSupportedException("");
            }

            

            return sendGridMessage;
        }
    }
}
