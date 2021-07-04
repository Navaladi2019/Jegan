using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace GRR.Data.Models
{
    public partial class UserRole : BaseEntity
    {
        public int Id { get; set; }
        public string Roles { get; set; }
        public int? UserAdditionalDetailId { get; set; }

        [JsonIgnore]
        public virtual UserAdditionalDetail UserAdditionalDetail { get; set; }
    }
}
