using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace KidsToyHive.SPA.Clients
{
    public class BaseClient<T>
    {
        protected readonly ILogger<T> _logger;
        protected readonly HttpClient _client;
        
        public BaseClient(HttpClient client, ILogger<T> logger)
        {
            _client = client;
            _logger = logger;
            _client.DefaultRequestHeaders.Add("TenantId", "619D303D-9CC4-486A-BAB9-486FAADCC056");            
        }
    }
}
