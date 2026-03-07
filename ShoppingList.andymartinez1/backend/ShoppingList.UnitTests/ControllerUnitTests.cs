using Microsoft.AspNetCore.Mvc;
using Moq;
using ShoppingList.Controllers;
using ShoppingList.ServiceContracts;
using ShoppingList.ServiceContracts.DTO;
using ShoppingList.UnitTests.UnitTestHelpers;

namespace ShoppingList.UnitTests;

public class ControllerUnitTests
{
    [Fact]
    public async Task Details_ReturnsOk_WhenItemExists()
    {
        // Arrange
        var serviceMock = ControllerTestHelpers.GetByIdMock();

        var controller = new ShoppingListController(serviceMock.Object);

        // Act
        ActionResult<GroceryItemResponse> actionResult = await controller.Details(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var dto = Assert.IsType<GroceryItemResponse>(okResult.Value);

        Assert.Equal(1, dto.Id);
        Assert.Equal("Milk", dto.Name);

        serviceMock.Verify(s => s.GetByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task Details_ReturnsNotFound_WhenItemDoesNotExist()
    {
        // Arrange
        var serviceMock = ControllerTestHelpers.GetByIdMock();

        var controller = new ShoppingListController(serviceMock.Object);

        // Act
        ActionResult<GroceryItemResponse> actionResult = await controller.Details(999);

        // Assert
        Assert.IsType<NotFoundResult>(actionResult.Result);
        serviceMock.Verify(s => s.GetByIdAsync(999), Times.Once);
    }

    [Fact]
    public async Task Index_ReturnsOk_WithListOfItems()
    {
        // Arrange
        var serviceMock = ControllerTestHelpers.GetAllMock();
        var controller = new ShoppingListController(serviceMock.Object);

        // Act
        var actionResult = await controller.Index();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        var items = Assert.IsType<List<GroceryItemResponse>>(okResult.Value);

        Assert.Equal(2, items.Count);
        Assert.Equal("Milk", items[0].Name);
        Assert.Equal("Bread", items[1].Name);

        serviceMock.Verify(s => s.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Index_ReturnsOk_WithEmptyList()
    {
        // Arrange
        var serviceMock = new Mock<IGroceryService>();
        serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<GroceryItemResponse>());

        var controller = new ShoppingListController(serviceMock.Object);

        // Act
        var actionResult = await controller.Index();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        var items = Assert.IsType<List<GroceryItemResponse>>(okResult.Value);

        Assert.Empty(items);
        serviceMock.Verify(s => s.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction_WhenRequestIsValid()
    {
        // Arrange
        var serviceMock = ControllerTestHelpers.AddMock();
        var controller = new ShoppingListController(serviceMock.Object);

        var request = new GroceryItemAddRequest
        {
            Name = "Eggs",
            Quantity = 12,
            DateAdded = DateTime.Now,
            IsPurchased = false,
        };

        // Act
        var actionResult = await controller.Create(request);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(actionResult);
        Assert.Equal(nameof(controller.Details), createdResult.ActionName);
        Assert.Equal(3, ((GroceryItemResponse)createdResult.Value!).Id);

        serviceMock.Verify(s => s.AddAsync(request), Times.Once);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtActionWithCorrectId_WhenItemIsAdded()
    {
        // Arrange
        var serviceMock = ControllerTestHelpers.AddMock();
        var controller = new ShoppingListController(serviceMock.Object);

        var request = new GroceryItemAddRequest
        {
            Name = "Eggs",
            Quantity = 12,
            DateAdded = DateTime.Now,
            IsPurchased = false,
        };

        // Act
        var actionResult = await controller.Create(request);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(actionResult);
        var routeValues = createdResult.RouteValues as IDictionary<string, object>;

        Assert.NotNull(routeValues);
        Assert.Equal(3, routeValues["id"]);
    }

    [Fact]
    public async Task Edit_ReturnsOk_WhenItemIsUpdatedSuccessfully()
    {
        // Arrange
        var serviceMock = ControllerTestHelpers.UpdateMock();
        var controller = new ShoppingListController(serviceMock.Object);

        var request = new GroceryItemUpdateRequest
        {
            Id = 1,
            Name = "Milk",
            Quantity = 5,
            DateAdded = DateTime.Now,
            IsPurchased = true,
        };

        // Act
        var actionResult = await controller.Edit(1, request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        var dto = Assert.IsType<GroceryItemResponse>(okResult.Value);

        Assert.Equal(1, dto.Id);
        Assert.Equal("Milk", dto.Name);
        Assert.Equal(5, dto.Quantity);
        Assert.True(dto.IsPurchased);

        serviceMock.Verify(s => s.UpdateAsync(request), Times.Once);
    }

    [Fact]
    public async Task Edit_ReturnsNotFound_WhenItemDoesNotExist()
    {
        // Arrange
        var serviceMock = new Mock<IGroceryService>();
        serviceMock
            .Setup(s => s.UpdateAsync(It.IsAny<GroceryItemUpdateRequest>()))
            .ReturnsAsync((GroceryItemResponse?)null);

        var controller = new ShoppingListController(serviceMock.Object);

        var request = new GroceryItemUpdateRequest
        {
            Id = 999,
            Name = "Nonexistent",
            Quantity = 1,
            DateAdded = DateTime.Now,
            IsPurchased = false,
        };

        // Act
        var actionResult = await controller.Edit(999, request);

        // Assert
        Assert.IsType<NotFoundResult>(actionResult);
        serviceMock.Verify(s => s.UpdateAsync(request), Times.Once);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenItemIsDeletedSuccessfully()
    {
        // Arrange
        var serviceMock = ControllerTestHelpers.DeleteMock();
        var controller = new ShoppingListController(serviceMock.Object);

        // Act
        var actionResult = await controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(actionResult);

        serviceMock.Verify(s => s.GetByIdAsync(1), Times.Once);
        serviceMock.Verify(s => s.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenItemDoesNotExist()
    {
        // Arrange
        var serviceMock = new Mock<IGroceryService>();
        serviceMock
            .Setup(s => s.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((GroceryItemResponse?)null);

        var controller = new ShoppingListController(serviceMock.Object);

        // Act
        var actionResult = await controller.Delete(999);

        // Assert
        Assert.IsType<NotFoundResult>(actionResult);

        serviceMock.Verify(s => s.GetByIdAsync(999), Times.Once);
        serviceMock.Verify(s => s.DeleteAsync(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task Details_ReturnsOkStatusCode_WhenItemExists()
    {
        // Arrange
        var serviceMock = ControllerTestHelpers.GetByIdMock();
        var controller = new ShoppingListController(serviceMock.Object);

        // Act
        ActionResult<GroceryItemResponse> actionResult = await controller.Details(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task Details_ReturnsCorrectDataType_WhenItemExists()
    {
        // Arrange
        var serviceMock = ControllerTestHelpers.GetByIdMock();
        var controller = new ShoppingListController(serviceMock.Object);

        // Act
        ActionResult<GroceryItemResponse> actionResult = await controller.Details(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var item = Assert.IsType<GroceryItemResponse>(okResult.Value);
        Assert.NotNull(item.Name);
        Assert.True(item.Quantity >= 0);
    }

    [Fact]
    public async Task Index_ReturnsOkStatusCode()
    {
        // Arrange
        var serviceMock = ControllerTestHelpers.GetAllMock();
        var controller = new ShoppingListController(serviceMock.Object);

        // Act
        var actionResult = await controller.Index();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task Index_ReturnsAllItemsWithCorrectProperties()
    {
        // Arrange
        var serviceMock = ControllerTestHelpers.GetAllMock();
        var controller = new ShoppingListController(serviceMock.Object);

        // Act
        var actionResult = await controller.Index();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        var items = Assert.IsType<List<GroceryItemResponse>>(okResult.Value);

        // Verify first item properties
        Assert.Equal(1, items[0].Id);
        Assert.Equal("Milk", items[0].Name);
        Assert.Equal(3, items[0].Quantity);
        Assert.False(items[0].IsPurchased);

        // Verify second item properties
        Assert.Equal(2, items[1].Id);
        Assert.Equal("Bread", items[1].Name);
        Assert.Equal(2, items[1].Quantity);
        Assert.True(items[1].IsPurchased);
    }

    [Fact]
    public async Task Create_CallsServiceOnce_WhenRequestIsValid()
    {
        // Arrange
        var serviceMock = ControllerTestHelpers.AddMock();
        var controller = new ShoppingListController(serviceMock.Object);

        var request = new GroceryItemAddRequest
        {
            Name = "Eggs",
            Quantity = 12,
            DateAdded = DateTime.Now,
            IsPurchased = false,
        };

        // Act
        await controller.Create(request);

        // Assert
        serviceMock.Verify(s => s.AddAsync(request), Times.Once);
    }

    [Fact]
    public async Task Create_ReturnsCreatedStatusCode_WhenItemIsAdded()
    {
        // Arrange
        var serviceMock = ControllerTestHelpers.AddMock();
        var controller = new ShoppingListController(serviceMock.Object);

        var request = new GroceryItemAddRequest
        {
            Name = "Eggs",
            Quantity = 12,
            DateAdded = DateTime.Now,
            IsPurchased = false,
        };

        // Act
        var actionResult = await controller.Create(request);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(actionResult);
        Assert.Equal(201, createdResult.StatusCode);
    }

    [Fact]
    public async Task Create_ReturnsCorrectResponseData_WhenItemIsAdded()
    {
        // Arrange
        var serviceMock = ControllerTestHelpers.AddMock();
        var controller = new ShoppingListController(serviceMock.Object);

        var request = new GroceryItemAddRequest
        {
            Name = "Eggs",
            Quantity = 12,
            DateAdded = DateTime.Now,
            IsPurchased = false,
        };

        // Act
        var actionResult = await controller.Create(request);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(actionResult);
        var response = Assert.IsType<GroceryItemResponse>(createdResult.Value);

        Assert.Equal(3, response.Id);
        Assert.Equal("Eggs", response.Name);
        Assert.Equal(12, response.Quantity);
        Assert.False(response.IsPurchased);
    }

    [Fact]
    public async Task Edit_ReturnsOkStatusCode_WhenItemIsUpdatedSuccessfully()
    {
        // Arrange
        var serviceMock = ControllerTestHelpers.UpdateMock();
        var controller = new ShoppingListController(serviceMock.Object);

        var request = new GroceryItemUpdateRequest
        {
            Id = 1,
            Name = "Milk",
            Quantity = 5,
            DateAdded = DateTime.Now,
            IsPurchased = true,
        };

        // Act
        var actionResult = await controller.Edit(1, request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task Edit_CallsServiceOnce_WhenItemIsUpdated()
    {
        // Arrange
        var serviceMock = ControllerTestHelpers.UpdateMock();
        var controller = new ShoppingListController(serviceMock.Object);

        var request = new GroceryItemUpdateRequest
        {
            Id = 1,
            Name = "Milk",
            Quantity = 5,
            DateAdded = DateTime.Now,
            IsPurchased = true,
        };

        // Act
        await controller.Edit(1, request);

        // Assert
        serviceMock.Verify(s => s.UpdateAsync(request), Times.Once);
    }

    [Fact]
    public async Task Edit_ReturnsNotFoundStatusCode_WhenItemDoesNotExist()
    {
        // Arrange
        var serviceMock = new Mock<IGroceryService>();
        serviceMock
            .Setup(s => s.UpdateAsync(It.IsAny<GroceryItemUpdateRequest>()))
            .ReturnsAsync((GroceryItemResponse?)null);

        var controller = new ShoppingListController(serviceMock.Object);

        var request = new GroceryItemUpdateRequest
        {
            Id = 999,
            Name = "Nonexistent",
            Quantity = 1,
            DateAdded = DateTime.Now,
            IsPurchased = false,
        };

        // Act
        var actionResult = await controller.Edit(999, request);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(actionResult);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsNoContentStatusCode_WhenItemIsDeletedSuccessfully()
    {
        // Arrange
        var serviceMock = ControllerTestHelpers.DeleteMock();
        var controller = new ShoppingListController(serviceMock.Object);

        // Act
        var actionResult = await controller.Delete(1);

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(actionResult);
        Assert.Equal(204, noContentResult.StatusCode);
    }

    [Fact]
    public async Task Delete_CallsBothGetByIdAndDeleteAsync_WhenItemExists()
    {
        // Arrange
        var serviceMock = ControllerTestHelpers.DeleteMock();
        var controller = new ShoppingListController(serviceMock.Object);

        // Act
        await controller.Delete(1);

        // Assert
        serviceMock.Verify(s => s.GetByIdAsync(1), Times.Once);
        serviceMock.Verify(s => s.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task Delete_DoesNotCallDeleteAsync_WhenItemDoesNotExist()
    {
        // Arrange
        var serviceMock = new Mock<IGroceryService>();
        serviceMock
            .Setup(s => s.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((GroceryItemResponse?)null);

        var controller = new ShoppingListController(serviceMock.Object);

        // Act
        await controller.Delete(999);

        // Assert
        serviceMock.Verify(s => s.DeleteAsync(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task Delete_ReturnsNotFoundStatusCode_WhenItemDoesNotExist()
    {
        // Arrange
        var serviceMock = new Mock<IGroceryService>();
        serviceMock
            .Setup(s => s.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((GroceryItemResponse?)null);

        var controller = new ShoppingListController(serviceMock.Object);

        // Act
        var actionResult = await controller.Delete(999);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(actionResult);
        Assert.Equal(404, notFoundResult.StatusCode);
    }
}
