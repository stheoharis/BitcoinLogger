using BitcoinLoggerServer.Framework.Interfaces;
using BitcoinLoggerServer.Framework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace BitcoinLoggerServer.Framework.BaseEntities
{

    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        protected DbContext _DbContext;

        public BaseRepository(DbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public IEnumerable<T> GetAll()
        {
            return _DbContext.Set<T>();
        }

        public T GetById(int id)
        {
            return _DbContext.Set<T>().Where(entity => entity.Id == id).SingleOrDefault();
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> expression)
        {
            return _DbContext.Set<T>().Where(expression);
        }

        public void Insert(T entity)
        {
            _DbContext.Set<T>().Add(entity);
            _DbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            T dbEntity = _DbContext.Set<T>().SingleOrDefault(dbEntity => dbEntity.Id == entity.Id);
            if (dbEntity == null) throw new BusinessException("Entity with id: " + entity.Id + "does not exist.");
            var entityProperties = typeof(T).GetProperties().ToList();
            entityProperties.ForEach(prop => prop.SetValue(dbEntity, prop.GetValue(entity)));
            _DbContext.SaveChanges();
        }
    }

}
