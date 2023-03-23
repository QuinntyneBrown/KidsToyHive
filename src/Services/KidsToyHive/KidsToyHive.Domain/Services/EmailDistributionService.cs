using System;
using System.Collections.Generic;
using System.Text;
using SendGrid.Helpers.Mail;

namespace KidsToyHive.Domain.Services;

public class EmailDistributionService : IEmailDistributionService
{
    public void SetDistributionList(ref SendGridMessage message)
    {
    }
}
