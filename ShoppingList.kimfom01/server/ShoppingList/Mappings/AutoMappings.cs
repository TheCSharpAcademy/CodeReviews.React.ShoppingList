using AutoMapper;
using ShoppingList.Dtos;
using ShoppingList.Entities;

namespace ShoppingList.Mappings;

public class AutoMappings : Profile
{
    public AutoMappings()
    {
        CreateMap<ShoppingItemResponse, ShoppingListItem>()
            .ReverseMap();
        CreateMap<CreateShoppingItem, ShoppingListItem>()
            .ReverseMap();
    }
}