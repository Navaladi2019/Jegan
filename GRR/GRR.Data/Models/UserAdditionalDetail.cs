using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace GRR.Data.Models
{
    public partial class UserAdditionalDetail : BaseEntity
    {
        public UserAdditionalDetail()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Groups { get; set; }
        public int? UserId { get; set; }
      
        [JsonIgnore]
        public virtual DbUser User { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
