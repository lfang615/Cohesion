using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Cohesion
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RepositoryContext(
                serviceProvider.GetRequiredService<DbContextOptions<RepositoryContext>>()))
            {
                if (context.ServiceRequests.Any())
                {
                    return; // data exists
                }

                context.ServiceRequests.AddRange(
                    new ServiceRequest
                    {
                        id = new Guid(),
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
                        id = new Guid(),
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
                        id = new Guid(),
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
                        id = new Guid(),
                        buildingCode = "4",
                        description = "test4",
                        currentStatus = CurrentStatus.InProgress,
                        createdBy = "Tom",
                        lastModifiedBy = "Sarah",
                        lastUpdatedBy = new DateTime(2019, 3, 23),
                        createdDate = new DateTime(2019, 3, 1)
                    }
                    );

                context.SaveChanges();
            }
        }
    }
}
