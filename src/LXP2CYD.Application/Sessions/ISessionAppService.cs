using System.Threading.Tasks;
using Abp.Application.Services;
using LXP2CYD.Sessions.Dto;

namespace LXP2CYD.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
