using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Services;

public interface IEmailDeliveryService
{
    Task Send(SendGridMessage message);
}
