using System.Threading.Tasks;
using Abp.Application.Services;
using LXP2CYD.Authorization.Accounts.Dto;

namespace LXP2CYD.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
