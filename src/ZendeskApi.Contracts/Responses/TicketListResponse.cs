using System.Collections.Generic;
using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class TicketListResponse : ListResponse<Ticket>
    {
        [DataMember(Name = "tickets")]
        public override IEnumerable<Ticket> Results { get; set; }
    }
}
