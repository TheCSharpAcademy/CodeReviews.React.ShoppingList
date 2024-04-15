namespace ShoppingList.Dtos;

public class ShoppingItemResponse
{
    public Guid Id { get; set; }
    public bool IsPickedUp { get; set; }
    public string Item { get; set; }
}