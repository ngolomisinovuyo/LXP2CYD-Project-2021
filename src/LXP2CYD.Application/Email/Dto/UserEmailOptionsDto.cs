using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Email.Dto
{
    public class UserEmailOptionsDto
    {
        public UserEmailOptionsDto()
        {
            CcEmails = new List<string>();
        }
        public List<string> ToEmails { get; set; }
        public List<string> CcEmails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public List<KeyValuePair<string, string>> PlaceHolders { get; set; }
    }
}
