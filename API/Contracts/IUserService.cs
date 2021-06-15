using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities.Auth;

namespace API.Contracts
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);

    }
}
