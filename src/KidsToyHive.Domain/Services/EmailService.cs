using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.Models;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Services
{
    public class EmailService: IEmailService
    {
        private readonly IEmailBuilder _emailBuilder;
        private readonly IEmailDistributionService _emailDistributionService;
        private readonly IEmailDeliveryService _emailDeliveryService;

        public EmailService(
            IEmailBuilder emailBuilder, 
            IEmailDistributionService emailDistributionService,
            IEmailDeliveryService emailDeliveryService)
        {
            _emailBuilder = emailBuilder;
            _emailDistributionService = emailDistributionService;
            _emailDeliveryService = emailDeliveryService;
        }
        public async Task SendNewCustomerEmail(Customer customer, User user)
        {
            var mailMessage = await _emailBuilder.Build(EmailTemplateName.NewCustomer, user);
            _emailDistributionService.SetDistributionList(ref mailMessage);
            _emailDeliveryService.Send(mailMessage);

        }

        public async Task SendNewDriverEmail(Driver driver, User user)
        {
            var mailMessage = await _emailBuilder.Build(EmailTemplateName.NewDriver, user);
            _emailDistributionService.SetDistributionList(ref mailMessage);
            _emailDeliveryService.Send(mailMessage);
        }
    }
}
