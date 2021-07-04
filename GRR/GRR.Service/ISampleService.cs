
using GRR.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GRR.Service
{
  public  interface ISampleService
    {

        public DbUser saveuser(DbUser dbUser);

        public DbUser Updateuser(DbUser dbUser);

        public void deleteUser(int id);

        public object GetUser(int id);
    }

}
