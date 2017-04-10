using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IUserIdentityResource
    {
        Task<IListResponse<UserIdentity>> GetAllAsync(long id);
        Task<IResponse<UserIdentity>> PostAsync(UserIdentityRequest request);
        Task<IResponse<UserIdentity>> PutAsync(UserIdentityRequest request);
        Task DeleteAsync(long id, long parentId);
    }
}