using KidsToyHive.Domain.Models;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Services
{
    public interface IEmailService
    {
        Task SendNewCustomer(Customer customer, User user);
        Task SendNewDriver(Driver driver, User user);
        Task SendBookingConfirmation(Booking booking, Customer customer);
    }
}
