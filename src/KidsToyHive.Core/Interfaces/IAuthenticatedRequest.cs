using System;
using System.Collections.Generic;
using System.Text;

using System;

namespace KidsToyHive.Core.Interfaces
{
    public interface IAuthenticatedRequest<TResponse>
    {
        int CurrentUserId { get; set; }
        string PartitionKey { get; set; }
        string CurrentUsername { get; set; }
    }
}

