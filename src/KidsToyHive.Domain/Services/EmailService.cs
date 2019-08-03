using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.Models;
using System.Collections.Generic;
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
        public async Task SendNewCustomer(Customer customer, User user)
        {
            var mailMessage = await _emailBuilder.Build(EmailTemplateName.NewCustomer, new Dictionary<string, string>());
            _emailDistributionService.SetDistributionList(ref mailMessage);
            _emailDeliveryService.Send(mailMessage);

        }

        public async Task SendNewDriver(Driver driver, User user)
        {
            var mailMessage = await _emailBuilder.Build(EmailTemplateName.NewDriver, new Dictionary<string, string>());
            _emailDistributionService.SetDistributionList(ref mailMessage);
            _emailDeliveryService.Send(mailMessage);
        }


        public async Task SendBookingConfirmation(Booking booking)
        {
            var mailMessage = await _emailBuilder.Build(EmailTemplateName.NewCustomer, new Dictionary<string, string>() {
                {
                    "{{ bookingDate }}", booking.Date.ToLongDateString()
                }
            });
            _emailDistributionService.SetDistributionList(ref mailMessage);
            _emailDeliveryService.Send(mailMessage);
        }
    }
}
