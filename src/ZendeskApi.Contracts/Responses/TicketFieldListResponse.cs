using System.Collections.Generic;
using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class TicketFieldListResponse : ListResponse<TicketField>
    {
        [DataMember(Name = "ticket_fields")]
        public override IEnumerable<TicketField> Results { get; set; }
    }
}
