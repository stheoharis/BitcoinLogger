using BitcoinLoggerServer.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace BitcoinLoggerServer.Framework.BaseEntities
{
    public abstract class BaseService<T> : IBaseService<T> where T : BaseEntity, new()
    {
        public IBaseRepository<T> EntityRepository { get; set; }

        public BaseService(IBaseRepository<T> baseRepository)
        {
            EntityRepository = baseRepository;
        }

        public IEnumerable<T> Get()
        {
            return EntityRepository.GetAll();
        }

        public T GetById(int id)
        {
            return EntityRepository.GetById(id);
        }

        public void Insert(T entity)
        {
            EntityRepository.Insert(entity);
        }

        public void Update(T entity)
        {
            EntityRepository.Update(entity);
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> expression)
        {
            return EntityRepository.Filter(expression);
        }
    }
}
