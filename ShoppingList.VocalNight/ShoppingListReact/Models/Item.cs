namespace ShoppingListReact.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean IsPickedUp { get; set; } = false;
    }
}
