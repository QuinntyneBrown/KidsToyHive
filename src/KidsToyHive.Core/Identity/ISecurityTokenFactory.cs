using System.Collections.Generic;
using System.Security.Claims;

namespace KidsToyHive.Core.Identity;

public interface ISecurityTokenFactory
{
    string Create(string username, List<Claim> customClaims = null);
}
