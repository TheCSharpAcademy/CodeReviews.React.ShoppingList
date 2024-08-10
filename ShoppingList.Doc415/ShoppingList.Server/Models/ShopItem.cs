namespace ShoppingList.Server.Models
{
    public class ShopItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPickedUp { get; set; } = false;
    }
}
