using Microsoft.Azure.Cosmos.Table;

namespace HexMaster.IdentityServer.Stores.Entities
{
    public class UserEntity : TableEntity
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public string Secret { get; set; }
        public string EmailAddress { get; set; }
        public string ProfilePicture { get; set; }
        public string IsEmailVerified { get; set; }

        public UserEntity(string rowKey)
        {
            PartitionKey = PartitionKeys.User;
            RowKey = rowKey;
        }
        public UserEntity()
        {
            PartitionKey = PartitionKeys.User;
        }
    }
}
