using System.Threading.Tasks;
using LXP2CYD.Configuration.Dto;

namespace LXP2CYD.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
