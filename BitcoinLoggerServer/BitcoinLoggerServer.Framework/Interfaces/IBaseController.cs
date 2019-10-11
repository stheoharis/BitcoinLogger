using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinLoggerServer.Framework.Interfaces
{ 
    public interface IBaseController<T>
    {
        IBaseService<T> BaseService { get; set; }

        // GET api/Base
        [HttpGet]
        ActionResult<IEnumerable<T>> Get();

        // GET api/Base/5
        [HttpGet("{id}")]
        ActionResult<T> Get(int id);

        // POST api/Base
        [HttpPost]
        ActionResult<T> Post([FromBody] T value);

        // PUT api/Base/5
        [HttpPut("{id}")]
        ActionResult<T> Put(int id, [FromBody] T entity);

        // DELETE api/Base/5
        [HttpDelete("{id}")]
        void Delete(int id);

    }
}
