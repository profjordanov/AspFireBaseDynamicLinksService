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
            var repositoryTypes = Assembly
                .GetAssembly(typeof(IDynamicLinksService))
                .GetTypes()
                .Where(t => t.Name.EndsWith("Repository") &&
                            t.IsInterface)
                .ToArray();

            var repositoryImplementationTypes = Assembly
                .GetAssembly(typeof(DynamicLinksService))
                .GetTypes()
                .Where(t => t.Name.EndsWith("Repository") &&
                            t.IsClass)
                .ToDictionary(t => t.Name, t => t);

            foreach (var repositoryType in repositoryTypes)
            {
                var expectedImplementationName = repositoryType
                    .Name
                    .Substring(1);

                if (!repositoryImplementationTypes.ContainsKey(expectedImplementationName))
                {
                    throw new InvalidOperationException($"Could not find implementation for {repositoryType.FullName}.");
                }

                var implementation = repositoryImplementationTypes[expectedImplementationName];

                if (!repositoryType.IsAssignableFrom(implementation))
                {
                    throw new InvalidOperationException($"For repository {repositoryType.Name} found matching type {implementation.Name}, but it does not implement it.");
                }

                services.AddTransient(repositoryType, implementation);
            }

            return services;
        }
    }
}