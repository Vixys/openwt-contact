using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OpenWT.Contact.Application.Contract;
using OpenWT.Contact.Application.Data;

namespace OpenWT.Contact.Api.Controllers
{
    [ApiController]
    public abstract class ApiControllerBase<TService, TDto, TId> : ControllerBase
        where TService : IServiceBase<TDto, TId> 
        where TId : struct 
        where TDto : IDto<TId>
    {
        private readonly TService _service;

        public ApiControllerBase(TService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<TDto>> GetAll()
        {
            return _service.GetAll().ToList();
        }
        
        [HttpGet("{id}")]
        public ActionResult<TDto> Get([FromRoute] TId id)
        {
            return _service.GetById(id);
        }
        
        [HttpPost]
        public ActionResult<TDto> Create([FromBody] TDto createBody)
        {
            return _service.Create(createBody);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] TId id)
        {
            _service.DeleteById(id);
            return Ok();
        }
        
        [HttpPut("{id}")]
        public ActionResult<TDto> Update([FromRoute] TId id, [FromBody] TDto createBody)
        {
            return _service.Update(id, createBody);
        }
    }
}