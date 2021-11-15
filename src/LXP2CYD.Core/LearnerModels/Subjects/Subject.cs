using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace LXP2CYD.LearnerModels.Subjects
{
    [Table("AppSubjects")]
    public class Subject: Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
