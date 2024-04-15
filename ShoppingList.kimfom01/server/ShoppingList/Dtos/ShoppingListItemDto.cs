namespace ShoppingList.Dtos;

public class ShoppingListItemDto
{
    public Guid Id { get; set; }
    public bool IsPickedUp { get; set; }
    public string Item { get; set; }
}