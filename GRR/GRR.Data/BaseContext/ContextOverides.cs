
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GRR.Data.BaseContext
{
    public partial class GRRContext : DbContext
    {
        private readonly UserInfo userInfo; 
        public GRRContext(DbContextOptions<GRRContext> options, UserInfo userInfo)
            : base(options)
        {
            this.userInfo = userInfo;
        }

     
        public override  int SaveChanges()
        {
            var entries = ChangeTracker.Entries();
            if (entries == null || !entries.Any(x => x.State == EntityState.Added
            || x.State == EntityState.Deleted || x.State == EntityState.Modified))
                return 0;

         
            var UtcNow = DateTime.UtcNow;
            var userId = userInfo.UserId;
            foreach (var entry in entries)
            {

                if (entry.Entity is BaseEntity)
                {
                    var entity = (BaseEntity)entry.Entity ;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entity.CreatedBy = userId;
                            entity.CreatedOn = UtcNow;
                            entity.ModifiedBy = userId;
                            entity.ModifiedOn = UtcNow;
                            entity.IsDeleted = false;


                            break;

                        case EntityState.Modified:
                            entity.ModifiedBy = userId;
                            entity.ModifiedOn = UtcNow;

                            break;
                    }
                }
            }

            return base.SaveChanges();
        }
    }
}
