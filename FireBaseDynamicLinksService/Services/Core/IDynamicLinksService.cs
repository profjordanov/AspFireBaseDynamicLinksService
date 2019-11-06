using System.Threading.Tasks;
using Google.Apis.FirebaseDynamicLinks.v1.Data;

namespace FireBaseDynamicLinksService.Services.Core
{
    public interface IDynamicLinksService
    {
        Task<CreateShortDynamicLinkResponse> CreateFirebaseDynamicLinkByIdAsync(string roleRequestId);
    }
}