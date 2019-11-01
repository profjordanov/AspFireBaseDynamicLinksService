using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using FireBaseDynamicLinksService.Services.Business;
using FireBaseDynamicLinksService.Services.Core;
using Google.Apis.Auth.OAuth2;
using Google.Apis.FirebaseDynamicLinks.v1;
using Google.Apis.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FireBaseDynamicLinksService.Configuration
{
    internal static class DependenciesConfiguration
    {
        internal static IServiceCollection AddFireBaseDynamicLinksService(this IServiceCollection serviceCollection)
        {
            var serviceAccountCredentialFilePath = Path.Combine(AppContext.BaseDirectory, "credentials.json");

            if (!File.Exists(serviceAccountCredentialFilePath))
            {
                throw new Exception("The service account credentials file does not exist at: " + serviceAccountCredentialFilePath);
            }

            try
            {
                var googleCredential = GoogleCredential
                    .FromFile(serviceAccountCredentialFilePath)
                    .CreateScoped(FirebaseDynamicLinksService.Scope.Firebase);

                serviceCollection.AddScoped(provider => new FirebaseDynamicLinksService(
                    new BaseClientService.Initializer
                    {
                        HttpClientInitializer = googleCredential
                    }));
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
                throw;
            }

            return serviceCollection;
        }

        internal static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IDynamicLinksService, DynamicLinksService>();

            return services;
        }
    }
}