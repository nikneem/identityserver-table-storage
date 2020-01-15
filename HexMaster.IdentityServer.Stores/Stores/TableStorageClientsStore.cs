using System;
using System.Threading.Tasks;
using HexMaster.IdentityServer.Stores.Common;
using HexMaster.IdentityServer.Stores.Configuration;
using HexMaster.IdentityServer.Stores.Entities;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;

namespace HexMaster.IdentityServer.Stores.Stores
{
    public sealed class TableStorageClientsStore : IClientStore
    {
        private readonly CloudTable _table;

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var operation = TableOperation.Retrieve<ClientEntity>(PartitionKeys.Clients, clientId);
            var operationResult = await _table.ExecuteAsync(operation);

            if (operationResult.Result is ClientEntity clientEntity)
            {
                var client = new Client
                {
                    ClientId = clientEntity.RowKey,
                    AllowedCorsOrigins = clientEntity.CorsOrigins.Split(Constants.SplitCharacter),
                    AllowedGrantTypes = clientEntity.GrantTypes.Split(Constants.SplitCharacter),
                    AllowedScopes = clientEntity.Scopes.Split(Constants.SplitCharacter),
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowRememberConsent = true,
                    ClientClaimsPrefix = string.Empty,
                    ClientName = clientEntity.ClientName,
                    ClientUri = clientEntity.ClientUri,
                    Description = clientEntity.Description,
                    EnableLocalLogin = clientEntity.EnableLocalLogin,
                    Enabled = clientEntity.Enabled,
                    PostLogoutRedirectUris = clientEntity.LogoutRedirectUris.Split(Constants.SplitCharacter),
                    RedirectUris= clientEntity.RedirectUris.Split(Constants.SplitCharacter),
                    RequireClientSecret = clientEntity.RequireClientSecret,
                    RequireConsent = clientEntity.RequireConsent,
                    RequirePkce = clientEntity.RequirePkce,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                };
                return client;
            }

            return null;
        }

        public TableStorageClientsStore(IOptions<CloudConfiguration> cloudConfig)
        {
            var storageConnectionString = cloudConfig.Value.StorageConnectionString;
            var account = TableStorageHelper.CreateStorageAccountFromConnectionString(storageConnectionString);
            var tableStorageClient = account.CreateCloudTableClient();
            _table = tableStorageClient.GetTableReference(TableNames.Clients);
        }
    }
}