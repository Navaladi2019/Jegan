
using GRR.Data.BaseContext;
using GRR.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace GRR.Data.UnitOfWork
{
    public class Uow : IUow
    {



        internal readonly DbContext Context;
        private IDbContextTransaction _transaction;
        public IDbContextTransaction transaction;
        private IsolationLevel? _isolationLevel;

        public readonly UserInfo userInfo;



        /// <summary>
        /// Returns DbContext to Internal
        /// </summary>

        DbContext IUow.GetDbContext()
        {
            return Context;
        }



        public Uow(GRRContext dbContext, UserInfo userInfo)
        {
            Context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.userInfo = userInfo;


        }





        private void StartNewTransactionIfNeeded()
        {
            if (transaction == null)
            {
                if (_transaction == null)
                {
                    if (_isolationLevel.HasValue)
                        _transaction = Context.Database.BeginTransaction(_isolationLevel.GetValueOrDefault());

                    else
                        _transaction = Context.Database.BeginTransaction();
                }
            }

        }


        public void BeginTransaction()
        {
            if (transaction == null)
            {
                if (_isolationLevel.HasValue)
                    transaction = Context.Database.BeginTransaction(_isolationLevel.GetValueOrDefault());
                else
                    transaction = Context.Database.BeginTransaction();
            }
        }

        public void CommitTransaction()
        {
            //do not open transaction here, because if during the request
            //nothing was changed (only select queries were run), we don't
            //want to open and commit an empty transaction - calling SaveChanges()
            //on _transactionProvider will not send any sql to database in such case
            Context.SaveChanges();

            if (transaction != null)
            {
                transaction.Commit();

                transaction.Dispose();
                transaction = null;
            }
        }

        public void RollbackTransaction()
        {

            if (transaction == null) return;

            transaction.Rollback();

            transaction.Dispose();
            transaction = null;


        }



        public void ForceBeginTransaction()
        {
            StartNewTransactionIfNeeded();
        }

        void IUow.CommitInternalTransaction()
        {
            //do not open transaction here, because if during the request
            //nothing was changed (only select queries were run), we don't
            //want to open and commit an empty transaction - calling SaveChanges()
            //on _transactionProvider will not send any sql to database in such case
            //  Context.SaveChanges();

            if (_transaction != null)
            {
                _transaction.Commit();

                _transaction.Dispose();
                _transaction = null;
            }
        }

        void IUow.RollbackInternalTransaction()
        {
            if (transaction != null)
            {
                if (_transaction == null) return;

                _transaction.Rollback();

                _transaction.Dispose();
                _transaction = null;
            }

        }

       public int SaveChanges()
        {



            return Context.SaveChanges();
        }

        public void SetIsolationLevel(IsolationLevel isolationLevel)
        {
            _isolationLevel = isolationLevel;
        }

        public void ClearIsolationLevel()
        {
            _isolationLevel = null;
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();

            _transaction = null;
        }

          private  T newInstance<T>() where T: class{
           return (T)Activator.CreateInstance(typeof(T),Context, userInfo);
            }

        private DbUserRepository _DbUserRepository;
        public DbUserRepository DbUser => _DbUserRepository ??= newInstance<DbUserRepository>();

        private UserAdditionalDetailRepository _UserAdditionalDetailRepository;
        public UserAdditionalDetailRepository UserAdditionalDetail => _UserAdditionalDetailRepository ??= newInstance<UserAdditionalDetailRepository>();


        private UserRoleRepository _userRoleRepository;
        public UserRoleRepository userRole => _userRoleRepository ??= newInstance<UserRoleRepository>();
    }
}
