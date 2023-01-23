using ESSModels.Model;

namespace ESSModels.Interface
{
    public interface IAuthenticateService
    {
        User Authenticate(string username, string password);
    }
}
