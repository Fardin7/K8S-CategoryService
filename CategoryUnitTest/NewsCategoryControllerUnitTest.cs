using AutoMapper;
using CategoryService.AsyncConnection;
using CategoryService.Contract;
using CategoryService.Controllers;
using CategoryService.Data;
using CategoryService.NewsClient;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CategoryUnitTest
{
    public class NewsCategoryControllerUnitTest
    {
        private Mock<IRepository> _repository;
        private Mock<IClientUpdate> _httpClient;
        private Mock<IMapper> _mapper;
        private Mock<INotification> _asyncNotification;
        private NewsCategoryController _newsCategoryController;

        public NewsCategoryControllerUnitTest()
        {
            _repository = new Mock<IRepository>();
            _httpClient = new Mock<IClientUpdate>();
            _mapper = new Mock<IMapper>();
            _asyncNotification = new Mock<INotification>();




        }
        [Fact]
        public async Task Get_WithExistingItems_ReturnAllExistingElement()
        {
            //Arrange
            IEnumerable<NewsCategoryRead> categoryReads = new List<NewsCategoryRead>()
            {
                new NewsCategoryRead()
                {
                    Id=1,
                    Description=Guid.NewGuid().ToString(),
                    Name=Guid.NewGuid().ToString()
                },
                new NewsCategoryRead()
                 {
                      Id = 1,
                      Description = Guid.NewGuid().ToString(),
                      Name = Guid.NewGuid().ToString()
                 },
                 new NewsCategoryRead()
                  {
                        Id = 1,
                        Description = Guid.NewGuid().ToString(),
                        Name = Guid.NewGuid().ToString()
                  },
            };
            _repository.Setup(x => x.Get()).ReturnsAsync(categoryReads);

            _newsCategoryController = new NewsCategoryController(_repository.Object
                , _httpClient.Object
                , _mapper.Object
                , _asyncNotification.Object);

            //Act
            var result = (_newsCategoryController.Get().Result as OkObjectResult).Value;

            //Assert
            result.Should().BeEquivalentTo(categoryReads);
        }

        [Fact]
        public async Task Get_WithExistingItem_ReturnExpectedItem()
        {
            //Arrange
            var category = new NewsCategoryRead()
            {
                Id = 1,
                Description = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString()
            };

            _repository.Setup(x => x.GetById(category.Id)).ReturnsAsync(category);

            _newsCategoryController = new NewsCategoryController(_repository.Object
                , _httpClient.Object
                , _mapper.Object
                , _asyncNotification.Object);

            //Act
            var result = _newsCategoryController.Get(category.Id).Result;

            var value = (result as OkObjectResult).Value;

            //Assert

            result.Should().BeOfType<OkObjectResult>();
            value.Should().BeEquivalentTo(category);

        }

        [Fact]
        public async Task Get_WithUnExistingItem_ReturnNotFound()
        {
            //Arrange
            _repository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((NewsCategoryRead)null);

            _newsCategoryController = new NewsCategoryController(_repository.Object
                , _httpClient.Object
                , _mapper.Object
                , _asyncNotification.Object);

            //Act

            var result = await _newsCategoryController.Get(1);

            //Assert

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Add_WithCtegoryToCreate_ReturnCreatedCategory()
        {
            //Arrange
            var category = new NewsCategoryCreate()
            {
                Name = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            };

            _repository.Setup(x => x.Add(category)).ReturnsAsync(new NewsCategoryRead()
            {
                Name = category.Name,
                Description = category.Description
            });

            _newsCategoryController = new NewsCategoryController(_repository.Object
               , _httpClient.Object
               , _mapper.Object
               , _asyncNotification.Object);

            //Act
            var result = await _newsCategoryController.Add(category);

            //Assert

            var value = (result.Result as CreatedAtActionResult).Value as NewsCategoryRead;

            value.Should().BeEquivalentTo(category);
        }

        [Fact]
        public async Task Update_WithExistingCategory_RetuenUpdatedCategory()
        {
            //Arrange
            var existingCategory = new NewsCategoryRead()
            {
                Description = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
                Id = 1
            };
            _repository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(existingCategory);
            var categoryToUpdate = new NewsCategoryCreate()
            {
                Description = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
                Id = 1
            };

            _repository.Setup(x => x.Update(categoryToUpdate)).ReturnsAsync(new NewsCategoryRead()
            {
                Name = categoryToUpdate.Name,
                Description = categoryToUpdate.Description,
                Id = categoryToUpdate.Id
            });
            _newsCategoryController = new NewsCategoryController(_repository.Object
             , _httpClient.Object
             , _mapper.Object
             , _asyncNotification.Object);

            //Act
            var result = await _newsCategoryController.Update(categoryToUpdate);

            var value = (result.Result as CreatedAtActionResult).Value as NewsCategoryRead;

            //Assert

            value.Should().BeEquivalentTo(categoryToUpdate);


        }

        [Fact]
        public async Task Delete_WithUnExistingCategory_RetuenNotFound()
        {
            //Arrange
            _repository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((NewsCategoryRead)null);

            var existingCategory = new NewsCategoryRead()
            {
                Description = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
                Id = 1
            };
            var existingCategoryId = 1;
            _newsCategoryController = new NewsCategoryController(_repository.Object
          , _httpClient.Object
          , _mapper.Object
          , _asyncNotification.Object);

            //Act
            var result = await _newsCategoryController.Delete(existingCategoryId);

            //Assert

            result.Should().BeOfType<NotFoundResult>();

        }
        [Fact]
        public async Task Delete_WithExistingCategory_RetuenNoContent()
        {
            //Arrange
          
            var existingCategory = new NewsCategoryRead()
            {
                Description = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
                Id = 1
            };
            _repository.Setup(x => x.GetById(existingCategory.Id)).ReturnsAsync(existingCategory);

            var existingCategoryId = 1;
            _newsCategoryController = new NewsCategoryController(_repository.Object
          , _httpClient.Object
          , _mapper.Object
          , _asyncNotification.Object);

            //Act
            var result = await _newsCategoryController.Delete(existingCategoryId);

            //Assert

            result.Should().BeOfType<NoContentResult>();

        }

    }
}
