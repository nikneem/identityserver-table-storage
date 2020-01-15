using System.Runtime.CompilerServices;
using Microsoft.Azure.Cosmos.Table;

namespace HexMaster.IdentityServer.Stores.Entities
{
    public sealed class ClientEntity : TableEntity
    {
        public string CorsOrigins { get; set; }
        public string GrantTypes { get; set; }
        public string Scopes { get; set; }
        public string ClientName { get; set; }
        public string ClientUri { get; set; }
        public string Description { get; set; }
        public bool EnableLocalLogin { get; set; }
        public bool Enabled { get; set; }
        public string LogoutRedirectUris { get; set; }
        public string RedirectUris { get; set; }
        public bool RequireClientSecret { get; set; }
        public bool RequireConsent { get; set; }
        public bool RequirePkce { get; set; }
    }
}
