using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitcoinLoggerServer.Framework.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BitcoinLoggerServer.Framework.BaseEntities
{
    //[Authorize]
    [Route("[controller]")]
    public abstract class BaseController<T> : ControllerBase, IBaseController<T> where T : BaseEntity, new()
    {
        public IBaseService<T> BaseService { get; set; }
        public BaseController(IBaseService<T> baseService)
        {
            BaseService = baseService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<T>> Get()
        {
            return BaseService.Get().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<T> Get(int id)
        {
            return BaseService.GetById(id);
        }

        [HttpPost]
        public ActionResult<T> Post([FromBody] T entity)
        {
            BaseService.Insert(entity);
            return entity;
        }

        [HttpPut("{id}")]
        public ActionResult<T> Put(int id, [FromBody] T entity)
        {
            entity.Id = id;
            BaseService.Update(entity);
            return entity;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}