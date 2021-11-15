using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Configuration;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Net.Mail;

namespace LXP2CYD.EntityFrameworkCore.Seed.Host
{
    public class DefaultSettingsCreator
    {
        private readonly LXP2CYDDbContext _context;

        public DefaultSettingsCreator(LXP2CYDDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            int? tenantId = null;

            if (LXP2CYDConsts.MultiTenancyEnabled == false)
            {
                tenantId = MultiTenancyConsts.DefaultTenantId;
            }

            // Emailing
            //AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, "admin@mydomain.com", tenantId);
            //AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, "mydomain.com mailer", tenantId);
            AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, "no-reply@LPX2.com");
            AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, "Center management team");
            AddSettingIfNotExists(EmailSettingNames.Smtp.Host, "smtp.mailtrap.io");
            AddSettingIfNotExists(EmailSettingNames.Smtp.Port, "587");
            AddSettingIfNotExists(EmailSettingNames.Smtp.UserName, "f4e7b6f9b00c43");
            AddSettingIfNotExists(EmailSettingNames.Smtp.Password, "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");
            AddSettingIfNotExists(EmailSettingNames.Smtp.Domain, "smtp.mailtrap.io");
            AddSettingIfNotExists(EmailSettingNames.Smtp.EnableSsl, "false");
            AddSettingIfNotExists(EmailSettingNames.Smtp.UseDefaultCredentials, "false");

            // Languages
            AddSettingIfNotExists(LocalizationSettingNames.DefaultLanguage, "en", tenantId);
        }

        private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
        {
            if (_context.Settings.IgnoreQueryFilters().Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
            {
                return;
            }

            _context.Settings.Add(new Setting(tenantId, null, name, value));
            _context.SaveChanges();
        }
    }
}
