
using GRR.Data.Models;
using GRR.Data.UnitOfWork;
using GRR.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GRR.Service
{
    public class SampleService : ISampleService
    {
        private readonly Uow _uow;
        public SampleService(IUow uow)
        {
            _uow = (Uow)uow;
        }

        public DbUser saveuser(DbUser dbUser)
        {
            _uow.DbUser.Add(dbUser);
            _uow.SaveChanges();
            return dbUser;

            //if there are any child it will be inserted also
        }

        public DbUser Updateuser(DbUser dbUser)
        {
            _uow.DbUser.Update(dbUser);

            UserAdditionalDetail userAdditionalDetail = dbUser.UserAdditionalDetail;
            _uow.UserAdditionalDetail.Update(userAdditionalDetail);
            _uow.SaveChanges();
            return dbUser;
        }


        public void deleteUser(int id)
        {
            _uow.DbUser.Delete(id);
            _uow.SaveChanges();
        }


        public object GetUser(int id)
        {
            Expression<Func<DbUser, TestProjection>> select = x =>  new TestProjection { Id = x.Id, Name = x.UserAdditionalDetail.Groups };
            var obj =   _uow.DbUser.GetListAsync<DbUser, TestProjection>(selectExpression: select, filter: (x => x.Name == "string"),
                includeProperties: x => x.Include(o => o.UserAdditionalDetail)).Result;
            return obj;

        }

       
    }

    public class TestProjection
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
