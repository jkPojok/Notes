using IdentityServer4;
using IdentityServer4.Models;
using System.Collections;

namespace NotesIdentity
{
    public static class IdentityServerConfiguration
    {
        public static IEnumerable<Client> AddClients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "notes-app",
                    ClientName = "Notes App",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "NotesApp"
                    },
                    RedirectUris =
                    {

                    },
                    PostLogoutRedirectUris =
                    {

                    },
                    AllowedCorsOrigins =
                    {
                        "http://localhost:5101"
                    },
                    AllowAccessTokensViaBrowser = true
                }
            };

        public static IEnumerable<ApiScope> AddScopes() =>
            new List<ApiScope>
            {
                new ApiScope("NotesApp")
            };

        public static IEnumerable<ApiResource> AddApiResources() =>
            new List<ApiResource>
            {
                new ApiResource("NotesAppApiResources") { Scopes = { "BookReaderUserApi" } }
            };

        public static IEnumerable<IdentityResource> AddIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
    }
}
