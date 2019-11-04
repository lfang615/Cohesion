using System;
using Xunit;
using Cohesion;
using Cohesion.Controllers;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Cohesion_Test
{
    public class ServiceRequestTest 
    {
        ServiceRequestController _controller;
        IServiceRequest _serviceRequest;

        public ServiceRequestTest()
        {
            _serviceRequest = new ServiceRequestRepoFake();
            _controller = new ServiceRequestController(_serviceRequest);
        }

        [Fact]
        public void Get_ReturnsOkResult()
        {
            // Act 
            var okResult = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get().Result as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<ServiceRequest>>(okResult.Value);
            Assert.Equal(4, items.Count);

        }

        [Fact]
        public void GetById_ExistingGuid_ReturnsOkResult()
        {
            //Arrange
            var testGuid = new Guid("3bec7cd9-035c-4728-ab7d-a467f87fde67");

            //Act
            var okResult = _controller.Get(testGuid);

            //Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
            
        }

        [Fact]
        public void GetById_ReturnsRightItem()
        {
            var testGuid = new Guid("3bec7cd9-035c-4728-ab7d-a467f87fde67");

            var okResult = _controller.Get(testGuid).Result as OkObjectResult;

            Assert.Equal(testGuid, (okResult.Value as ServiceRequest).id);
        }

        [Fact]
        public void GetById_UnknownGuid_ReturnsNotFound()
        {
            var notFound = _controller.Get(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(notFound.Result);
        }

        [Fact]
        public void Post_InvalidObject_ReturnsBadRequest()
        {
            ServiceRequest sq = new ServiceRequest()
            {
                createdBy = "Tom"
            };

            _controller.ModelState.AddModelError("buildingCode", "Required");
            _controller.ModelState.AddModelError("description", "Required");
            _controller.ModelState.AddModelError("currentStatus", "Required");
            _controller.ModelState.AddModelError("lastModifiedBy", "Required");
            _controller.ModelState.AddModelError("lastUpdatedBy", "Required");
            _controller.ModelState.AddModelError("createdDate", "Required");

            var badResponse = _controller.Post(sq);

            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public void Post_ValidObject_ReturnsCreatedResponse()
        {
            ServiceRequest sq = new ServiceRequest()
            {
                buildingCode = "5",
                description = "test5",
                currentStatus = CurrentStatus.InProgress,
                createdBy = "Tom",
                lastModifiedBy = "George",
                lastUpdatedBy = new DateTime(),
                createdDate = new DateTime()
            };

            var createdResponse = _controller.Post(sq);

            Assert.IsType<CreatedResult>(createdResponse);
        }

        [Fact]
        public void Post_ValidObject_ReturnsCreatedItemWithId()
        {
            ServiceRequest sq = new ServiceRequest()
            {
                buildingCode = "5",
                description = "test5",
                currentStatus = CurrentStatus.InProgress,
                createdBy = "Tom",
                lastModifiedBy = "George",
                lastUpdatedBy = new DateTime(),
                createdDate = new DateTime()
            };

            var createdResponse = _controller.Post(sq) as CreatedResult;
            var item = createdResponse.Value as ServiceRequest;

            Assert.IsType<ServiceRequest>(item);
            Assert.Equal("test5", item.description);
        }

        [Fact]
        public void Put_InvalidGuid_ReturnsNotFound()
        {
            ServiceRequest sq = new ServiceRequest()
            {
                buildingCode = "5",
                description = "test5",
                currentStatus = CurrentStatus.InProgress,
                createdBy = "Tom",
                lastModifiedBy = "George",
                lastUpdatedBy = new DateTime(),
                createdDate = new DateTime()
            };

            //Act
            var notFound = _controller.Put(Guid.NewGuid(), sq);

            //Assert
            Assert.IsType<NotFoundResult>(notFound);
        }

        [Fact]
        public void Put_InvalidObject_ReturnsBadRequest()
        {
            ServiceRequest sq = new ServiceRequest()
            {
                buildingCode = "9"
            };

            _controller.ModelState.AddModelError("description", "Required");
            _controller.ModelState.AddModelError("currentStatus", "Required");
            _controller.ModelState.AddModelError("lastModifiedBy", "Required");
            _controller.ModelState.AddModelError("lastUpdatedBy", "Required");
            _controller.ModelState.AddModelError("createdDate", "Required");

            var badRequest = _controller.Put(new Guid("02018b48-1265-4ec7-8381-13fb2ed209d4"), sq);

            Assert.IsType<BadRequestResult>(badRequest);
        }

        [Fact]
        public void Put_NullObject_ReturnsBadRequest()
        {
            var badRequest = _controller.Put(new Guid("02018b48-1265-4ec7-8381-13fb2ed209d4"), null);

            Assert.IsType<BadRequestResult>(badRequest);
        }

        [Fact]
        public void Put_ValidGuidAndObject_ReturnsOk()
        {
            ServiceRequest sq = new ServiceRequest()
            {
                buildingCode = "5",
                description = "test5",
                currentStatus = CurrentStatus.InProgress,
                createdBy = "Tom",
                lastModifiedBy = "George",
                lastUpdatedBy = new DateTime(),
                createdDate = new DateTime()
            };

            var okResult = _controller.Put(new Guid("7d963041-05b9-47eb-a0b5-018e2fe78d07"), sq);

            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void Delete_InvalidGuid_ReturnsNotFound()
        {
            var wrongGuid = Guid.NewGuid();

            var badResponse = _controller.Delete(wrongGuid);

            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Delete_ExistingGuid_Returns201Result()
        {
            var existingGuid = new Guid("7d963041-05b9-47eb-a0b5-018e2fe78d07");

            var okResponse = _controller.Delete(existingGuid);

            Assert.IsType<CreatedResult>(okResponse);
        }

        [Fact]
        public void Delete_ExistingGuid_RemovesOneItem()
        {
            var existingGuid = new Guid("02018b48-1265-4ec7-8381-13fb2ed209d4");

            var okResponse = _controller.Delete(existingGuid);

            Assert.Equal(3, _serviceRequest.FindAll().Count());
        }


    }
}
