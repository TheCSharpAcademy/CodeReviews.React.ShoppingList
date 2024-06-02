import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrash } from '@fortawesome/free-solid-svg-icons';

function ShoppingListItem({ item, deleteItem, toggleIsPickedUp }) {
    function handleChange() {
        toggleIsPickedUp(item.id);
    }

    return (
        <>
        <div className="shopping-list-item">
            <input 
                type="checkbox" 
                className="shopping-list-item-input"
                checked={item.isPickedUp}
                onChange={handleChange}
            />
            <p className={item.isPickedUp ? "completed" : ""}>{item.itemName}</p>
            <button onClick={() => deleteItem(item.id)}>
                <FontAwesomeIcon icon={faTrash} />
            </button>
                        
        </div>
        </>
    );
}
export default ShoppingListItem;
