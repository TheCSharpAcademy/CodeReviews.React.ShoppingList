using System.ComponentModel.DataAnnotations;

namespace ShoppingList.StanimalTheMan.Server.Models;

public class ShoppingListItem
{
	public int Id { get; set; }

	[Required(ErrorMessage = "Item name is required")]
	[StringLength(50)]
	public string Name { get; set; }

	public bool IsPickedUp { get; set; }
}
