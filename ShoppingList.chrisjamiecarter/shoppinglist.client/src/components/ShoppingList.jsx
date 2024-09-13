const ShoppingList = ({ list, handleCheck, handleDelete }) => {
    return (
        <div className="shopping-list">
            {list.length ? (
                <ul className="p-0">
                    {list.map((item) => {
                        return (
                            <li className="d-flex justify-content-between my-1" key={item.id}>
                                <input
                                    onChange={() => handleCheck(item.id)}
                                    type="checkbox"
                                    checked={item.isPickedUp}
                                />

                                <label
                                    onDoubleClick={() => handleCheck(item.id)}>
                                    <span>{item.name}</span>
                                </label>

                                <button
                                    type="button"
                                    onClick={() => handleDelete(item.id)}
                                    aria-label="Delete Item"
                                >
                                    <span className="btn-text">x</span>
                                </button>
                            </li>
                        );
                    })}
                </ul>
            ) : (
                <h3>Empty List</h3>
            )}
        </div>
    );
};

export default ShoppingList;
