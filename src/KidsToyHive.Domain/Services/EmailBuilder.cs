using System;
using System.Collections.Generic;
using System.Text;
using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
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

        public SendGridMessage Build(EmailTemplateName template, User user)
        {
            var sendGridMessage = new SendGridMessage();

            switch (template)
            {
                case EmailTemplateName.NewCustomer:
                    break;

                default:
                    throw new NotSupportedException("");
            }

            

            return sendGridMessage;
        }
    }
}
