using IdentityServer4;
using IdentityServer4.Models;

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
                        "http://localhost/signin"
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://localhost/signout"
                    },
                    AllowedCorsOrigins =
                    {
                        "http://localhost:5101",
                        "http://localhost:5103",
                        "http://localhost:5000"
                    },
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 3600
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
                new ApiResource("NotesAppApiResources") { Scopes = { "NotesApp" } }
            };

        public static IEnumerable<IdentityResource> AddIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
    }
}
