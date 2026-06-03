namespace ShoppingList.NathanJenner.Server.Models;

public record CreateItemDto(string Name, int Quantity);
public record UpdateItemDto(string Name, int Quantity);
public record ReorderItemDto(int Id, int SortOrder);