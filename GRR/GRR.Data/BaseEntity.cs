using System;
using System.Collections.Generic;
using System.Text;

namespace GRR.Data
{
    public abstract class BaseEntity
    {
        public int CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool Status { get; set; }

        public bool IsDeleted{get;set;}


    }
}
