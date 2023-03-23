using Stripe.Issuing;

namespace KidsToyHive.Domain.Services;

public class DepositService : IDepositService
{
    public void Process()
    {
        //https://stripe.com/docs/api/issuing/authorizations/approve?lang=dotnet
        var options = new AuthorizationApproveOptions { };
        var service = new AuthorizationService();
        var authorization = service.Approve("iauth_1DPc772eZvKYlo2C6avLyZ25", options);
    }
}
