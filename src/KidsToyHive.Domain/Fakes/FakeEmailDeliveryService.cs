using KidsToyHive.Domain.Services;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Fakes
{
    public class FakeEmailDeliveryService : IEmailDeliveryService
    {
        public Task Send(SendGridMessage message)
        {
            return Task.CompletedTask;
        }
    }
}
