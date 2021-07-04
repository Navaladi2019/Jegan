using System;
using System.Collections.Generic;
using System.Text;

namespace GRR.Data.Repository.Abstract
{
    public interface ICrudRepository<TEntity> : ICudRepository<TEntity>,IReadRepository<TEntity> where TEntity: class
    {

    }
}
