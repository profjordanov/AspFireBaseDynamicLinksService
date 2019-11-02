using System;
using System.Diagnostics;
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


            var requestModel = new CreateShortDynamicLinkRequest
            {
                DynamicLinkInfo = new DynamicLinkInfo
                {
                    AndroidInfo = new AndroidInfo
                    {
                        AndroidPackageName = "AndroidPackageName",
                        //ETag = "ETagAndroid"
                    },
                    IosInfo = new IosInfo
                    {
                        IosAppStoreId = "IosAppStoreId",
                        IosBundleId = "AndroidPackageName",
                        //ETag = "ETagIos"
                    },
                    AnalyticsInfo = new AnalyticsInfo
                    {
                        //ETag = "ETagAnalytics"
                    },
                    DesktopInfo = new DesktopInfo
                    {
                        //ETag = "ETagDesktop"
                    },
                    DomainUriPrefix = "soge.page.link",
                    Link = linkAppOpen,
                    NavigationInfo = new NavigationInfo
                    {
                       // ETag = "ETagNavigation"
                    },
                    SocialMetaTagInfo = new SocialMetaTagInfo
                    {
                       // ETag = "ETagSocialMeta"
                    }
                },
                Suffix = new Suffix
                {
                    Option = "SHORT",
                    //ETag = "ETagSuffix"
                },
                //ETag = "ETag"
            };

            var request = _fireBaseDynamicLinksService.ShortLinks.Create(requestModel);

            //var jsonRequest = JsonConvert.SerializeObject(
            //    requestModel,
            //    Formatting.Indented,
            //    new CreateShortDynamicLinkRequestConverter());

            //request.ModifyRequest = message =>
            //    message.Content = new StringContent(
            //        jsonRequest,
            //        Encoding.UTF8,
            //        "application/json");
            try
            {
                var shortDynamicLinkResponse = await request.ExecuteAsync();

                return shortDynamicLinkResponse.ShortLink;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }
    }
}