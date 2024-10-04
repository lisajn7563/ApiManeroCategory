using ApiManeroCategory.Contexts;
using ApiManeroCategory.Controllers;
using ApiManeroCategory.Entites;
using ApiManeroCategory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCategoryApi;

public class CategoryControllerTests
{
    private DbContextOptions<DataContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task Create_ShouldAddCategory_IfModelStateIsValid()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using (var context = new DataContext(options))
        {
            var controller = new CategoryController(context);
            var model = new CategoryRegistration
            {
                id = "3",
                categoryTitle = "Electronics"
            };

            // Act
            var result = await controller.Create(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var createdCategory = Assert.IsType<CategoryEntity>(okResult.Value);
            Assert.Equal("Electronics", createdCategory.categoryTitle);

            // Verify that the category was added to the database
            var categoryInDb = await context.Category.FindAsync("3");
            Assert.NotNull(categoryInDb);
            Assert.Equal("Electronics", categoryInDb.categoryTitle);
        }
    }

    [Fact]
    public async Task Create_ShouldReturnBadRequest_IfModelStateIsInvalid()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using (var context = new DataContext(options))
        {
            var controller = new CategoryController(context);
            controller.ModelState.AddModelError("Error", "ModelState is invalid");

            var model = new CategoryRegistration
            {
                id = "3",
                categoryTitle = "Electronics"
            };

            // Act
            var result = await controller.Create(model);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
    [Fact]
    public async Task GetAll_ShouldReturnAllCategories()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using (var context = new DataContext(options))
        {
            context.Category.AddRange(new List<CategoryEntity>
                {
                    new CategoryEntity { id = "1", categoryTitle = "Books" },
                    new CategoryEntity { id = "2", categoryTitle = "Movies" }
                });
            context.SaveChanges();

            var controller = new CategoryController(context);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var categoryList = Assert.IsType<List<CategoryEntity>>(okResult.Value);
            Assert.Equal(2, categoryList.Count);
        }
    }
}
