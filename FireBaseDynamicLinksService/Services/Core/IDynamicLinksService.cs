using System.Threading.Tasks;

namespace FireBaseDynamicLinksService.Services.Core
{
    public interface IDynamicLinksService
    {
        Task<string> CreateRoleRequestFireBaseDynamicLinkAsync(string roleRequestId);
    }
}