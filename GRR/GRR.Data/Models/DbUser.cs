using System;
using System.Collections.Generic;

#nullable disable

namespace GRR.Data.Models
{
    public partial class DbUser : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public DateTime? Dob { get; set; }
  

        public virtual UserAdditionalDetail UserAdditionalDetail { get; set; }
    }
}
