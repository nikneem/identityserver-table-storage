using System.Linq;
using System.Threading.Tasks;
using HexMaster.IdentityServer.Stores.Abstractions;
using HexMaster.IdentityServer.Stores.Common;
using HexMaster.IdentityServer.Stores.Configuration;
using HexMaster.IdentityServer.Stores.Entities;
using HexMaster.IdentityServer.Stores.ExtensionMethods;
using HexMaster.IdentityServer.Stores.Models;
using IdentityServer4.Models;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;

namespace HexMaster.IdentityServer.Stores.Stores
{
   public sealed class TableStorageUsersStore : IUsersStore
    {

        private readonly CloudTable _table;

        public async Task<User> Validate(string username, string password)
        {
            await _table.CreateIfNotExistsAsync();

            var usernameFilter =
                TableQuery.GenerateFilterCondition(nameof(UserEntity.Username), QueryComparisons.Equal, username);
            var partitionKeyFilter = TableQuery.GenerateFilterCondition(nameof(UserEntity.PartitionKey),
                QueryComparisons.Equal, PartitionKeys.User);
            var filter = TableQuery.CombineFilters(usernameFilter, TableOperators.And, partitionKeyFilter);
            var query = new TableQuery<UserEntity>().Where(filter);

            var continuationToken = new TableContinuationToken();
            var fetchedUser = await _table.ExecuteQuerySegmentedAsync(query, continuationToken);
            var userEntity = fetchedUser.Results.FirstOrDefault();
            if (userEntity != null)
            {
                if (password.ValidatePassword(userEntity.Secret, userEntity.Password))
                {
                    return new User(
                        userEntity.RowKey,
                        userEntity.Username,
                        userEntity.EmailAddress,
                        userEntity.IsEmailVerified,
                        userEntity.ProfilePicture
                    );
                }
            }
            return null;
        }



        public TableStorageUsersStore(IOptions<CloudConfiguration> cloudConfig)
        {
            var storageConnectionString = cloudConfig.Value.StorageConnectionString;
            var account = TableStorageHelper.CreateStorageAccountFromConnectionString(storageConnectionString);
            var tableStorageClient = account.CreateCloudTableClient();
            _table = tableStorageClient.GetTableReference(TableNames.Clients);
        }
    }
}
