using Moq;
using ShoppingList.ServiceContracts;
using ShoppingList.ServiceContracts.DTO;

namespace ShoppingList.UnitTests.UnitTestHelpers;

public static class ControllerTestHelpers
{
    public static Mock<IGroceryService> GetByIdMock()
    {
        var serviceMock = new Mock<IGroceryService>();
        serviceMock
            .Setup(s => s.GetByIdAsync(1))
            .ReturnsAsync(
                new GroceryItemResponse
                {
                    Id = 1,
                    Name = "Milk",
                    Quantity = 3,
                    DateAdded = DateTime.Now,
                    IsPurchased = false,
                }
            );

        return serviceMock;
    }

    public static Mock<IGroceryService> GetAllMock()
    {
        var serviceMock = new Mock<IGroceryService>();
        var items = new List<GroceryItemResponse>
        {
            new()
            {
                Id = 1,
                Name = "Milk",
                Quantity = 3,
                DateAdded = DateTime.Now,
                IsPurchased = false,
            },
            new()
            {
                Id = 2,
                Name = "Bread",
                Quantity = 2,
                DateAdded = DateTime.Now,
                IsPurchased = true,
            },
        };

        serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(items);

        return serviceMock;
    }

    public static Mock<IGroceryService> AddMock()
    {
        var serviceMock = new Mock<IGroceryService>();
        serviceMock
            .Setup(s => s.AddAsync(It.IsAny<GroceryItemAddRequest>()))
            .ReturnsAsync(
                new GroceryItemResponse
                {
                    Id = 3,
                    Name = "Eggs",
                    Quantity = 12,
                    DateAdded = DateTime.Now,
                    IsPurchased = false,
                }
            );

        return serviceMock;
    }

    public static Mock<IGroceryService> UpdateMock()
    {
        var serviceMock = new Mock<IGroceryService>();
        serviceMock
            .Setup(s => s.UpdateAsync(It.IsAny<GroceryItemUpdateRequest>()))
            .ReturnsAsync(
                new GroceryItemResponse
                {
                    Id = 1,
                    Name = "Milk",
                    Quantity = 5,
                    DateAdded = DateTime.Now,
                    IsPurchased = true,
                }
            );

        return serviceMock;
    }

    public static Mock<IGroceryService> DeleteMock()
    {
        var serviceMock = new Mock<IGroceryService>();
        serviceMock
            .Setup(s => s.GetByIdAsync(1))
            .ReturnsAsync(
                new GroceryItemResponse
                {
                    Id = 1,
                    Name = "Milk",
                    Quantity = 3,
                    DateAdded = DateTime.Now,
                    IsPurchased = false,
                }
            );
        serviceMock.Setup(s => s.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);

        return serviceMock;
    }
}
