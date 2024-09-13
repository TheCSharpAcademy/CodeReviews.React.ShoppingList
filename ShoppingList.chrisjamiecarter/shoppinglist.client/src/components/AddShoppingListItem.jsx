import { useRef } from "react";

const AddShoppingListItem = ({ newItem, setNewItem, handleSubmit }) => {
    const inputRef = useRef();

    return (
        <form className="d-flex justify-content-between my-3" onSubmit={handleSubmit}>
            <label className="d-flex align-items-center" htmlFor="addItem">Add:</label>

            <input
                className="mx-3"
                type="text"
                id="addItem"
                autoFocus
                ref={inputRef}
                placeholder="Add Item"
                required
                value={newItem}
                onChange={(e) => setNewItem(e.target.value)}
            />

            <button
                type="submit"
                aria-label="Add Item"
                onClick={() => inputRef.current.focus()}
            >
                <span className="btn-text">+</span>
            </button>
        </form>
    );
};

export default AddShoppingListItem;
