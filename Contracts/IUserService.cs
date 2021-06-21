using System.Threading.Tasks;
using Entities.Auth;

namespace Contracts
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
    }
}
