using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace BitcoinLoggerServer.Framework.Interfaces
{
    public interface IBaseRepository<T>
    {
                
        IEnumerable<T> GetAll();
        T GetById(int id);
        IEnumerable<T> Filter(Expression<Func<T, bool>> expression);
        void Insert(T entity);
        void Update(T entity);

    }
}
