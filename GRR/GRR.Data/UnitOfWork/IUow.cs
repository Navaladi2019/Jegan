
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GRR.Data.UnitOfWork
{
  
    public  interface IUow
    {
       




     



        /// <summary>
        /// Opens a new transaction instantly when being called.
        /// If a transaction is already open, it won't do anything.
        /// Generally, you shouldn't call this method unless you need
        /// to control the exact moment of opening a transaction.
        /// Unit of Work automatically handles opening transactions
        /// in a convenient time.        
        /// </summary>
        void ForceBeginTransaction();

        /// <summary>
        /// Commits the current transaction (does nothing when none exists).
        /// </summary>
        void CommitInternalTransaction();

        /// <summary>
        /// Rolls back the current transaction (does nothing when none exists).
        /// </summary>
        void RollbackInternalTransaction();

        /// <summary>
        /// Saves changes to database, previously opening a transaction
        /// only when none exists. The transaction is opened with isolation
        /// level set in Unit of Work before calling this method.
        /// </summary>
       public int SaveChanges();

        /// <summary>
        /// Sets the isolation level for new transactions.
        /// </summary>
        /// <param name="isolationLevel"></param>
        public void SetIsolationLevel(IsolationLevel isolationLevel);


        /// <summary>
        /// Clears the isolation Level
        /// </summary>
        public void ClearIsolationLevel();


        /// <summary>
        /// Creates Unit Transaction
        /// </summary>

        public void BeginTransaction();


        /// <summary>
        /// Commits Unit Transaction
        /// </summary>

        public void CommitTransaction();



        /// <summary>
        /// Rollback Unit Transaction
        /// </summary>
        public void RollbackTransaction();


        internal DbContext GetDbContext();




    }
}
