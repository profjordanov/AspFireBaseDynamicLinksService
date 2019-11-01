using System.Threading.Tasks;
using FireBaseDynamicLinksService.Services.Core;
using Google.Apis.FirebaseDynamicLinks.v1;
using Google.Apis.FirebaseDynamicLinks.v1.Data;

namespace FireBaseDynamicLinksService.Services.Business
{
    public class DynamicLinksService : IDynamicLinksService
    {
        private readonly FirebaseDynamicLinksService _fireBaseDynamicLinksService;

        public DynamicLinksService(FirebaseDynamicLinksService fireBaseDynamicLinksService)
        {
            _fireBaseDynamicLinksService = fireBaseDynamicLinksService;
        }

        public async Task<string> CreateRoleRequestDynamicLinkAsync(string roleRequestId)
        {
            var request = _fireBaseDynamicLinksService.ShortLinks.Create(new CreateShortDynamicLinkRequest());
        }
    }
}