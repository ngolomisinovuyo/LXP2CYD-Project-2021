using System.ComponentModel.DataAnnotations;

namespace LXP2CYD.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}