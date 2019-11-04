using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Contracts;
using Entities.Models;

namespace Cohesion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestController : ControllerBase
    {
        private IServiceRequest _repository;
        public ServiceRequestController(IServiceRequest repository)
        {
            _repository = repository;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            try
            {
                var requests = _repository.FindAll();

                if(requests != null)
                {
                    return Ok(requests);
                }
                else
                {
                    return NoContent();
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                var request = _repository.FindByCondition(x => Convert.ToInt32(x.id) == id);

                if(request != null)
                {
                    return Ok(request);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] ServiceRequest request)
        {
            try
            {
                if(request == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                _repository.Create(request);

                return Ok("updated service request");
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
            
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] ServiceRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var foundRequest = _repository.FindByCondition(x => x.id == id);
                if(foundRequest == null)
                {
                    return NotFound();
                }

                _repository.Update((ServiceRequest)foundRequest);

                return Ok("updated service request");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var request = _repository.FindByCondition(x => Convert.ToInt32(x.id) == id);

                if(request != null)
                {
                    _repository.Delete((ServiceRequest)request);
                    
                    return StatusCode(201);
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
