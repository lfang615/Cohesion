using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Contracts;
using Entities.Models;

namespace Cohesion.Controllers
{
    public class ServiceRequestController : ControllerBase
    {
        private IServiceRequest _repository;
        public ServiceRequestController(IServiceRequest repository)
        {
            _repository = repository;
        }
        // GET api/servicerequest
        [HttpGet("api/servicerequest")]
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

        // GET api/servicerequest/5
        [HttpGet("api/servicerequest/{id}")]
        public ActionResult<string> Get(Guid id)
        {
            try
            {
                var request = _repository.FindByCondition(x => x.id == id).FirstOrDefault();

                if(request != null)
                {
                    return Ok(request);
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

        // POST api/servicerequest
        [HttpPost("api/servicerequest")]
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

                request.id = new Guid();
                _repository.Create(request);

                return Created("POST/api/servicerequest", request);
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
            
        }

        // PUT api/servicerequest/5
        [HttpPut("api/servicerequest/{id}")]
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

                var foundRequest = _repository.FindByCondition(x => x.id == id).FirstOrDefault();
                if(foundRequest == null)
                {
                    return NotFound();
                }

                foundRequest.lastModifiedBy = request.lastModifiedBy;
                foundRequest.lastUpdatedBy = request.lastUpdatedBy;
                foundRequest.description = request.description;
                foundRequest.currentStatus = request.currentStatus;
                foundRequest.createdDate = request.createdDate;
                foundRequest.createdBy = request.createdBy;
                foundRequest.buildingCode = request.buildingCode;

                _repository.Update(foundRequest);

                return Ok("updated service request");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/servicerequest/5
        [HttpDelete("api/servicerequest/{id}")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                var request = _repository.FindByCondition(x => x.id == id).FirstOrDefault();

                if(request != null)
                {
                    _repository.Delete((ServiceRequest)request);
                    
                    return Created("Successful", request);
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
