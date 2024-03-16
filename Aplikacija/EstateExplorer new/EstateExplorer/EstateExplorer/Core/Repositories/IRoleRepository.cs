using EstateExplorer.Models;
using Microsoft.AspNetCore.Identity;

namespace EstateExplorer.Core.Repositories
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
