import { useEffect, useState } from 'react'
import './ShoppingList.css'
import ShoppingListItem from './ShoppingListItem.jsx';

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
            .then(res => res.json());

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

                <input
                    value={addItemText}
                    onChange={e => setAddItemText(e.target.value)}
                    placeholder="Add Item..."
                />
                <button onClick={() => addItem(addItemText)}>
                    Add
                </button>
            </div>
    );
}

export default ShoppingList;