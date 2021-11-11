using LXP2CYD.Debugging;

namespace LXP2CYD
{
    public class LXP2CYDConsts
    {
        public const string LocalizationSourceName = "LXP2CYD";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = false;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "2260c5081e954758aac5d69cfca360ab";
    }
}
