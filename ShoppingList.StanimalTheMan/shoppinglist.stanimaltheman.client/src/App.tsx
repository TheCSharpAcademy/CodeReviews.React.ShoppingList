import { useEffect, useState } from 'react';
import './App.css';
import AddShoppingListItemForm from './components/AddShoppingListItemForm';

interface ShoppingListItem {
    id: number;
    name: string;
    isPickedUp: boolean;
}
function App() {
    const [shoppingListItems, setShoppingListItems] = useState<ShoppingListItem[]>([]);

    useEffect(() => {
        populateShoppingList();
    }, []);

    const handleFormSubmit = (data) => {
        setShoppingListItems(prevShoppingListItems => [...prevShoppingListItems, data])
    };

    const handleItemPickUp = (id: number) => {
        const itemToMarkAsPickedUp = shoppingListItems.filter(s => s.id == id)[0];
        // i do not let users toggle back and forth for simplicity's sake?  maybe user can make mistake so feel to let me know otherwise.
        if (!itemToMarkAsPickedUp || itemToMarkAsPickedUp.isPickedUp) {
            return; // Item not found or already picked up
        }
        itemToMarkAsPickedUp.isPickedUp = true;
        fetch(`https://localhost:7050/api/shoppinglistitem/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(itemToMarkAsPickedUp)
        });

        // Manually update the state to reflect the change as update endpoint returns NoContent
        const updatedItems = shoppingListItems.map(item =>
            item.id === id ? { ...item, isPickedUp: true } : item
        );
        setShoppingListItems(updatedItems);
    };

    const contents = shoppingListItems === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <ul>
            {shoppingListItems.map(s => {
                return <li key={s.id}
                    style={{ textDecoration: s.isPickedUp ? 'line-through' : 'none' }}
                    onClick={() => handleItemPickUp(s.id) }>{s.name} {s.isPickedUp}</li>
                })
            }
        </ul>;

    return (
        <div>
            <h1 id="tabelLabel">Shopping List</h1>
            <AddShoppingListItemForm onSubmit={handleFormSubmit}  />
            {contents}
{/*            <button onClick={handleAddItem}>Add Item</button>*/}
        </div>
    );

    async function populateShoppingList() {
        const response = await fetch('https://localhost:7050/api/shoppinglistitem');
        const data = await response.json();
        setShoppingListItems(data);
    }
}

export default App;