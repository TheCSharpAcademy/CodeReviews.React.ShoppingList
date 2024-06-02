import { useEffect, useState } from 'react'
import './ShoppingList.css'
import ShoppingListItem from './ShoppingListItem.jsx';

function ShoppingList() {
    const [items, setItems] = useState([]
        //     [
        //     {
        //         id: 1,
        //         itemName: "Bananas",
        //         isPickedUp: false
        //     },
        //     {
        //         id: 2,
        //         itemName: "Cucumber",
        //         isPickedUp: false
        //     }
        // ]
    );
    const [error, setError] = useState(null);

    useEffect(() => {
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
    }, []);


    const [addItemText, setAddItemText] = useState('');

    function addItem(itemName) {
        const newItem = {
            id: items.length + 1,
            itemName: itemName,
            isPickedUp: false
        };

        setItems([...items, newItem]);
        setAddItemText('');
    }

    function deleteItem(id) {
        setItems(items.filter(item => item.id !== id));
    }

    function toggleIsPickedUp(id) {
        setItems(items.map(item => {
            if (item.id === id) {
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
                />
                <button onClick={() => addItem(addItemText)}>Add</button>
            </div>
    );
}

export default ShoppingList;