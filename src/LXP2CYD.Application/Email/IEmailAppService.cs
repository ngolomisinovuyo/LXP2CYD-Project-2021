using Abp.Domain.Services;
using LXP2CYD.Email.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Email
{
    public interface IEmailAppService: IDomainService
    {
        Task SendEmailConfirmation(UserEmailOptionsDto userEmailOptions);
        Task SendForgotPasswordEmail(UserEmailOptionsDto userEmailOptions);
        Task SendEqnuiryResponseEmail(UserEmailOptionsDto userEmailOptions);
        Task Send(UserEmailOptionsDto userEmailOptions);
    }
}
