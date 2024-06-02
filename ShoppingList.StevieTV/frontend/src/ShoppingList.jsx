import { useEffect, useState } from 'react'
import ShoppingListItem from './ShoppingListItem.jsx';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCartPlus } from '@fortawesome/free-solid-svg-icons';

function ShoppingList() {
    const [items, setItems] = useState([]);
    const [error, setError] = useState(null);
    const [addItemText, setAddItemText] = useState('');

    useEffect(() => {
        fetchShoppingList();
    }, []);

    function fetchShoppingList() {
        fetch('https://localhost:44343/api/ShoppingList')
            .then(res => {
                if (res.ok) {
                    return res.json();
                }
                throw res;
            })
            .then(data => setItems(data))
            .catch(error => {
                console.log(error);
                setError(error);
            })
    }

    function addItem(itemName) {

        if (itemName === '') {
            return;
        }

        fetch('https://localhost:44343/api/ShoppingList', {
            method: 'POST',
            body: JSON.stringify({
                itemName: itemName,
                isPickedUp: false,
            }),
            headers: {
                'Content-Type': 'application/json',
            }
        })
            .then(res => res.json())
            .then(fetchShoppingList)
        setAddItemText('');
    }

    function deleteItem(id) {

        if (id === null) {
            return;
        }

        fetch(`https://localhost:44343/api/ShoppingList/${id}`, {
            method: 'DELETE',
        })
            .then(res => res);

        setItems(items.filter(item => item.id !== id));
    }

    function toggleIsPickedUp(id) {
        setItems(items.map(item => {
            if (item.id === id) {
                fetch(`https://localhost:44343/api/ShoppingList/${id}?isPickedUp=${!item.isPickedUp}`, {
                    method: 'PATCH'
                })
                    .then(res => res.json());
                return {...item, isPickedUp: !item.isPickedUp};
            } else {
                return item;
            }
        }));
    }

    return (
        error ?
            "Error" :
            <div className="shopping-list">
                <div className="shopping-list-item">
                    <input
                        value={addItemText}
                        className="shopping-list-add-item-input"
                        onChange={e => setAddItemText(e.target.value)}
                        placeholder="Add Item..."
                        onKeyDown={event => {
                            if (event.key === 'Enter') {
                                addItem(addItemText);
                            }
                        }}
                    />
                    <button onClick={() => addItem(addItemText)}>
                        <FontAwesomeIcon icon={faCartPlus}/>
                    </button>
                </div>
                {items.length === 0 &&
                    <h3>Shopping List is Empty</h3>
                }
                {items.length > 0 &&
                    items.map(item => (
                        <ShoppingListItem
                            key={item.id}
                            item={item}
                            deleteItem={deleteItem}
                            toggleIsPickedUp={toggleIsPickedUp}
                        />
                    ))
                }

            </div>
    );
}

export default ShoppingList;