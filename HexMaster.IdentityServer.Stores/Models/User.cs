using System;

namespace HexMaster.IdentityServer.Stores.Models
{
    public sealed class User
    {


        public Guid Id { get; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string ProfilePicture { get; set; }
        public string IsEmailVerified { get; set; }


        public User(string id, string username, string emailAddress, string isEmailVerified, string profilePicture)
        {
            Id = Guid.Parse(id);
            Username = username;
            EmailAddress = emailAddress;
            IsEmailVerified = isEmailVerified;
            ProfilePicture = profilePicture;
        }

    }
}
