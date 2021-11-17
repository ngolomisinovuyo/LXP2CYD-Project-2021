
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LXP2CYD.Web.Models.Home
{
    public class ContactUsModel
    {
        public ContactUsFormModel ContactUsFormModel { get; set; }
        public  CenterDetails SystemDetailsModel { get; set; }

        public IEnumerable<ContactUsFormModel> contactUs { get; set; }
        public IEnumerable<CenterDetails> systemDetails { get; set; }
    }
}
