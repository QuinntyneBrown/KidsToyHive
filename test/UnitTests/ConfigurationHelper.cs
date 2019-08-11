using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace UnitTests
{
    public static class ConfigurationHelper
    {
        public static IConfiguration Seed => new ConfigurationBuilder()
                .AddInMemoryCollection(new List<KeyValuePair<string, string>>() {
                                new KeyValuePair<string, string>("Seed:DefaultProfile:Name", "Name"),
                                new KeyValuePair<string, string>("Seed:DefaultUser:Password", ""),
                                new KeyValuePair<string, string>("Seed:DefaultUser:Username", "Username")
                })
                .Build();

        public static IConfiguration OAuth2 => new ConfigurationBuilder()
                .AddInMemoryCollection(new List<KeyValuePair<string, string>>() {
                                new KeyValuePair<string, string>("Authentication:ExpirationMinutes", "1"),
                                new KeyValuePair<string, string>("Authentication:JwtKey", "6h0bqMhnKqvA/dk5j/iXTbK7vevpTPuV")
                })
                .Build();
    }
}
