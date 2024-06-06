using System.ComponentModel;

namespace ShoppingList.StevieTV.Models;

public class ShoppingListItem
{
    public int Id { get; set; }
    public string ItemName { get; set; }
    [DefaultValue(false)]
    public bool IsPickedUp { get; set; }
}