using System.Threading.Tasks;
using HexMaster.IdentityServer.Stores.Models;

namespace HexMaster.IdentityServer.Stores.Abstractions
{
    public interface IUsersStore
    {
        Task<User> Validate(string username, string password);
    }
}