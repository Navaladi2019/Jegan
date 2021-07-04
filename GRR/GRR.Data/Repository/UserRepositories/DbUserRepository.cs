using GRR.Data.Models;
using GRR.Data.Repository.Abstract;
using GRR.Data.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GRR.Data.Repository
{
  public  class DbUserRepository : CrudRepository<DbUser>
    {
        public DbUserRepository(DbContext dbContext,UserInfo userInfo) : base(dbContext, userInfo)
        {

        }



    }
}
