using System;
using System.Diagnostics;
using System.Threading.Tasks;
using FireBaseDynamicLinksService.Services.Core;
using Google.Apis.FirebaseDynamicLinks.v1;
using Google.Apis.FirebaseDynamicLinks.v1.Data;
using Microsoft.AspNetCore.Http;

namespace FireBaseDynamicLinksService.Services.Business
{
    public class DynamicLinksService : IDynamicLinksService
    {
        private readonly FirebaseDynamicLinksService _fireBaseDynamicLinksService;
        private readonly IHttpContextAccessor _contextAccessor;

        public DynamicLinksService(FirebaseDynamicLinksService fireBaseDynamicLinksService, IHttpContextAccessor contextAccessor)
        {
            _fireBaseDynamicLinksService = fireBaseDynamicLinksService;
            _contextAccessor = contextAccessor;
        }

        public async Task<CreateShortDynamicLinkResponse> CreateRoleRequestFireBaseDynamicLinkAsync(
            string roleRequestId)
        {
            var linkAppOpen = GenerateRoleRequestLink(roleRequestId);

            var requestModel = new CreateShortDynamicLinkRequest
            {
                DynamicLinkInfo = new DynamicLinkInfo
                {
                    AndroidInfo = new AndroidInfo(),
                    IosInfo = new IosInfo(),
                    // Parameters used for tracking.
                    AnalyticsInfo = new AnalyticsInfo(),
                    DesktopInfo = new DesktopInfo(),
                    // E.g. https://maps.app.goo.gl, https://maps.page.link
                    // Will fallback to DynamicLinkDomain
                    DomainUriPrefix = "soge.page.link",
                    // <-- Can used only one of DomainUriPrefix / DynamicLinkDomain -->
                    // Dynamic Links domain that the project owns, e.g. abcd.app.goo.gl 
                    //  Required if missing DomainUriPrefix
                    // DynamicLinkDomain = "soge.domain.link",

                    // The link your app will open
                    // You can specify any URL your app can handle./
                    // must be a well-formatted URL
                    Link = linkAppOpen,
                    // Information of navigation behavior of a Firebase Dynamic Links.
                    NavigationInfo = new NavigationInfo(),
                    // Used to set meta tag data for link previews on social sites
                    SocialMetaTagInfo = new SocialMetaTagInfo()
                },
                Suffix = new Suffix
                {
                    Option = "SHORT",
                }
            };

            var request = _fireBaseDynamicLinksService.ShortLinks.Create(requestModel);

            try
            {
                var shortDynamicLinkResponse = await request.ExecuteAsync();

                var shortLink = shortDynamicLinkResponse.ShortLink;

                return shortDynamicLinkResponse;

            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
                return default(CreateShortDynamicLinkResponse);
            }
        }

        private string GenerateRoleRequestLink(string roleRequestId)
        {
            var request = _contextAccessor.HttpContext.Request;
            var uriBuilder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Path = "roleRequests/applyPermissions/" + roleRequestId
            };

            var x = uriBuilder.Uri.AbsoluteUri;
            var y = uriBuilder.Uri.AbsolutePath;
            var yz = uriBuilder.Uri.LocalPath;

            return uriBuilder.Uri.AbsoluteUri;
        }
    }
}