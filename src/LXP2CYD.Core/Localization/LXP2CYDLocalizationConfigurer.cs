using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace LXP2CYD.Localization
{
    public static class LXP2CYDLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(LXP2CYDConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(LXP2CYDLocalizationConfigurer).GetAssembly(),
                        "LXP2CYD.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
