using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace BitcoinLoggerServer.Framework.Interfaces
{
    public interface IBaseService<T>
    {
        IBaseRepository<T> EntityRepository { get; set; }

        IEnumerable<T> Get();
        T GetById(int id);
        void Insert(T entity);
        IEnumerable<T> Filter(Expression<Func<T, bool>> expression);
        void Update(T entity);

    }
}
