using System.Collections.Generic;
using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class UserIdentityListResponse : ListResponse<UserIdentity>
    {
        [DataMember(Name = "identities")]
        public override IEnumerable<UserIdentity> Results { get; set; }
    }
}
