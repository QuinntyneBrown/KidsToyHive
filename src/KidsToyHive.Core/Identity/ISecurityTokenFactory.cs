namespace KidsToyHive.Core.Identity
{
    public interface ISecurityTokenFactory
    {
        string Create(string username, int userId = 0, string customerKey = "");
    }
}
