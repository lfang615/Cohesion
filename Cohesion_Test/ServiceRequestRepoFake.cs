using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Contracts;
using Entities.Models;

namespace Cohesion_Test
{
    public class ServiceRequestRepoFake : IServiceRequest
    {
        private readonly List<ServiceRequest> _serviceRequests;

        public ServiceRequestRepoFake()
        {
            _serviceRequests = new List<ServiceRequest>()
            {
               new ServiceRequest
                    {
                        id = new Guid("3bec7cd9-035c-4728-ab7d-a467f87fde67"),
                        buildingCode = "1",
                        description = "test1",
                        currentStatus = CurrentStatus.InProgress,
                        createdBy = "Larry",
                        lastModifiedBy = "Tom",
                        lastUpdatedBy = new DateTime(2019, 1, 23),
                        createdDate = new DateTime(2019, 1, 1)
                    },
                    new ServiceRequest
                    {
                        id = new Guid("02018b48-1265-4ec7-8381-13fb2ed209d4"),
                        buildingCode = "2",
                        description = "test2",
                        currentStatus = CurrentStatus.InProgress,
                        createdBy = "Mindy",
                        lastModifiedBy = "Mark",
                        lastUpdatedBy = new DateTime(2019, 5, 16),
                        createdDate = new DateTime(2019, 5, 1)
                    },
                    new ServiceRequest
                    {
                        id = new Guid("7d963041-05b9-47eb-a0b5-018e2fe78d07"),
                        buildingCode = "3",
                        description = "test3",
                        currentStatus = CurrentStatus.InProgress,
                        createdBy = "Larry",
                        lastModifiedBy = "Tom",
                        lastUpdatedBy = new DateTime(2019, 1, 23),
                        createdDate = new DateTime(2019, 1, 1)
                    },
                    new ServiceRequest
                    {
                        id = new Guid("94762cc5-8ae8-4b64-b5d7-806c0d12ec68"),
                        buildingCode = "4",
                        description = "test4",
                        currentStatus = CurrentStatus.InProgress,
                        createdBy = "Tom",
                        lastModifiedBy = "Sarah",
                        lastUpdatedBy = new DateTime(2019, 3, 23),
                        createdDate = new DateTime(2019, 3, 1)
                    }
            };
        }

        public IEnumerable<ServiceRequest> FindAll()
        {
            return _serviceRequests;
        }

        public IEnumerable<ServiceRequest> FindByCondition(Expression<Func<ServiceRequest, bool>> expression)
        {
            return _serviceRequests.AsQueryable().Where(expression);
        }

        public void Create(ServiceRequest entity)
        {
            entity.id = Guid.NewGuid();
            _serviceRequests.Add(entity);
        }

        public void Update(ServiceRequest entity)
        {
            // new values assigned to properties

        }

        public void Delete(ServiceRequest entity)
        {
            _serviceRequests.Remove(entity);
        }
    }
}
