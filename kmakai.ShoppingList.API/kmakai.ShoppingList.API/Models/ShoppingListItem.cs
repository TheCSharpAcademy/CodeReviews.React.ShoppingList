namespace kmakai.ShoppingList.API.Models;

public class ShoppingListItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsChecked { get; set; } = false;
}
