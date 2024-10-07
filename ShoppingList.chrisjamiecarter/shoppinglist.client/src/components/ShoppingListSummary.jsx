const ShoppingListSummary = ({ list }) => {
    return (
        <div className="shopping-list-summary my-3">
            {list.length} Shopping List {list.length === 1 ? "Item" : "Items"}{" "}
        </div>
    );
};

export default ShoppingListSummary;
