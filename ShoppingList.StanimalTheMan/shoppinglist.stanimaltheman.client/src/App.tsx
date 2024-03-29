import { useEffect, useState } from 'react';
import './App.css';

interface ShoppingListItem {
    id: number;
    name: string;
    isPickedUp: boolean;
}

const data = {
    name: 'Ramen',
    isPickedUp: false
};

// Define the fetch options for the POST request
const options = {
    method: 'POST',
    headers: {
        'Content-Type': 'application/json'
    },
    body: JSON.stringify(data)
};

function App() {
    const [shoppingListItems, setShoppingListItems] = useState<ShoppingListItem[]>();

    useEffect(() => {
        populateShoppingList();
    }, []);

    const handleAddItem = (e) => {
        fetch('https://localhost:7050/api/shoppinglistitem', options).then(res => res.json()).then(data => console.log(data));
    };

    const contents = shoppingListItems === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <ul>
            {shoppingListItems.map(s => {
                return <li>{s.name} {s.isPickedUp}</li>
            }) }
        </ul>;

    return (
        <div>
            <h1 id="tabelLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
            <button onClick={handleAddItem}>Add Item</button>
        </div>
    );

    async function populateShoppingList() {
        const response = await fetch('https://localhost:7050/api/shoppinglistitem');
        const data = await response.json();
        setShoppingListItems(data);
    }
}

export default App;