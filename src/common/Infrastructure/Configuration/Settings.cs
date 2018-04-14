namespace Infrastructure.Configuration
{
    public class AuthenticationSettings
    {
        public string TokenPath { get; set; }
        public int ExpirationMinutes { get; set; }
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public string AuthType { get; set; }
    }

    public class StripeSettings
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }

    public class ClusterSettings
    {
        public string AccountServiceBaseUrl { get; set; }
        public string ChatServiceBaseUrl { get; set; }
        public string ContactServiceBaseUrl { get; set; }
        public string ContentServiceBaseUrl { get; set; }
        public string DashboardServiceBaseUrl { get; set; }
        public string DigitalAssetServiceBaseUrl { get; set; }
        public string IdentityServiceBaseUrl { get; set; }
        public string PaymentServiceBaseUrl { get; set; }
        public string ProductServiceBaseUrl { get; set; }
        public string ShoopingServiceBaseUrl { get; set; }
        public string SubscriptionServiceBaseUrl { get; set; }
    }
}
