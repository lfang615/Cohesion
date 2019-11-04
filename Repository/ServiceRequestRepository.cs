using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class ServiceRequestRepository : RepositoryBase<ServiceRequest>, IServiceRequest
    {
        public ServiceRequestRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
           
        }

        public IEnumerable<ServiceRequest> GetAllRequests()
        {
            return FindAll();
        }

        public ServiceRequest GetRequestById(Guid id)
        {
            return (ServiceRequest)FindByCondition(x => x.id == id);
        }
        public void CreateRequest(ServiceRequest request)
        {
            Create(request);
        }

        public void UpdateRequest(ServiceRequest request)
        {
            Update(request);
        }

        public void DeleteRequest(ServiceRequest request)
        {
            Delete(request);
        }
    }
}
