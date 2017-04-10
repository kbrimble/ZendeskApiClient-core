using System.Collections.Generic;
using System.Runtime.Serialization;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    [DataContract]
    public class TicketFormListResponse : ListResponse<TicketForm>
    {
        [DataMember(Name = "ticket_forms")]
        public override IEnumerable<TicketForm> Results { get; set; }
    }
}
