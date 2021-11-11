using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using LXP2CYD.Configuration.Dto;

namespace LXP2CYD.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : LXP2CYDAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
