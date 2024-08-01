import { Link } from 'react-router-dom';
import { useEffect, useState } from "react";
import Button from 'react-bootstrap/Button';
import Item from '../components/ShopItem';
function Home() {
    const [items, setItems] = useState();
    const [isNotLoaded, setIsNotLoaded] = useState(true);
    const [newItem, setNewItem] = useState("");
    useEffect(() => {
        loadItems();
    }, []);

    const HandlePickChange = (itemId) => {
        console.log(items);
    };

    const HandleDelete = async (deletedId) => {
        setItems(
            items.filter(i=> i.id!==deletedId)
        )

    };

    const handleSubmit = async (event) => {
        event.preventDefault(); 
        if (newItem == "") {
            return;
        }
        const itemToAdd = { name: newItem, isPickedUp:false }; 

        try {
            const response = await fetch('https://localhost:7039/api/ShopItems', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(itemToAdd)
            });

            if (response.ok) {
                const createdItem = await response.json();
                setItems(items.concat(createdItem));
                setNewItem("");
            } else {
                console.error("Failed to add item to the database");
            }
        } catch (error) {
            console.error("Error adding item:", error);
        }
        console.log(items);

    };

    const handleInputChange = (event) => {
         setNewItem(event.target.value);
    };

    const contents = isNotLoaded ?
        (<div className="container text-center"><h1><em>Loading... </em></h1></div>) :
        (<div className="container outercontainer text-center" style={{ width: 440, height: 800 }}>
            <h3 id="tableLabel">Shopping List</h3>
            <br />

            <form onSubmit={handleSubmit}>
                <input type="text" value={newItem} onChange={handleInputChange} style={{
                    backgroundColor: '#f9ecec', marginRight: '10px', borderRadius:'5px'
                }} /> 
                <Button type="Submit" variant="outline-warning"  style={{ color: '#533e41', borderColor: '#533e41' }}>+</Button>
            </form>
          <br/>
            {items.map((item) =>
                <Item inputItem={item} key={item.id} onPickChange={HandlePickChange} onDelete={HandleDelete} />
            )}
        </div>);

    return (
        <div>
        <br/>
            {contents}
        </div>
    );



    async function loadItems() {
        try {
            const response = await fetch('https://localhost:7039/api/ShopItems');
            const data = await response.json();
            await setItems(data);
            setIsNotLoaded(false);
            console.log(items);
        }
        catch {
            loadItems();
        }
        
    }
}

export default Home;
