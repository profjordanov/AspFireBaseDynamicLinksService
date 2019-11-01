using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FireBaseDynamicLinksService.JsonConverters;
using FireBaseDynamicLinksService.Services.Core;
using Google.Apis.FirebaseDynamicLinks.v1;
using Google.Apis.FirebaseDynamicLinks.v1.Data;
using Newtonsoft.Json;

namespace FireBaseDynamicLinksService.Services.Business
{
    public class DynamicLinksService : IDynamicLinksService
    {
        private readonly FirebaseDynamicLinksService _fireBaseDynamicLinksService;

        public DynamicLinksService(FirebaseDynamicLinksService fireBaseDynamicLinksService)
        {
            _fireBaseDynamicLinksService = fireBaseDynamicLinksService;
        }

        public async Task<string> CreateRoleRequestFireBaseDynamicLinkAsync(string roleRequestId)
        {
            var linkAppOpen = "https://api-dev.hconnect.heidelbergcement.com/applyrolerequest/" + roleRequestId;

            var request = _fireBaseDynamicLinksService.ShortLinks.Create(new CreateShortDynamicLinkRequest());

            var requestModel = new CreateShortDynamicLinkRequest
            {
                DynamicLinkInfo = new DynamicLinkInfo
                {
                    AndroidInfo = new AndroidInfo(),
                    IosInfo = new IosInfo(),
                    AnalyticsInfo = new AnalyticsInfo(),
                    DesktopInfo = new DesktopInfo(),
                    DomainUriPrefix = string.Empty,
                    DynamicLinkDomain = string.Empty,
                    ETag = string.Empty,
                    Link = linkAppOpen,
                    NavigationInfo = new NavigationInfo(),
                    SocialMetaTagInfo = new SocialMetaTagInfo()
                },
                ETag = string.Empty,
                LongDynamicLink = string.Empty,
                SdkVersion = string.Empty,
                Suffix = new Suffix()
            };

            var jsonRequest = JsonConvert.SerializeObject(
                requestModel,
                Formatting.Indented,
                new CreateShortDynamicLinkRequestConverter());

            request.ModifyRequest = message =>
                message.Content = new StringContent(
                    jsonRequest,
                    Encoding.UTF8,
                    "application/json");

            var shortDynamicLinkResponse = await request.ExecuteAsync();

            return shortDynamicLinkResponse.ShortLink;
        }
    }
}