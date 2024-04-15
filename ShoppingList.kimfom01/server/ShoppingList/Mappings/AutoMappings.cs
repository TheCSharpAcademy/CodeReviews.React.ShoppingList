using AutoMapper;
using ShoppingList.Dtos;
using ShoppingList.Entities;

namespace ShoppingList.Mappings;

public class AutoMappings : Profile
{
    public AutoMappings()
    {
        CreateMap<ShoppingListItemDto, ShoppingListItem>()
            .ReverseMap();
    }
}