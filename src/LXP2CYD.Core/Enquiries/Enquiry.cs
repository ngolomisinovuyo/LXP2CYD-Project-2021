using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace LXP2CYD.Enquiries
{
    [Table("AppEnquiries")]
    public class Enquiry: FullAuditedEntity<int>, IMayHaveTenant
    {
        public long? UserId { get; set; }
        public DateTime Date { get; set; }
        public EnquiryStatus Status { get; set; }
        public EnquiryType Type { get; set; }
        public int? TenantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string EmailAddress { get; set; }
        public string PatronType { get; set; }
        public string Message { get; set; }
    }
}
