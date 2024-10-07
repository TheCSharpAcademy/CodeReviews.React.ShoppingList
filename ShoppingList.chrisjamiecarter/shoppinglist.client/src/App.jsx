import "./App.css";
import Header from "./components/Header";
import AddShoppingListItem from "./components/AddShoppingListItem";
import ShoppingList from "./components/ShoppingList";
import ShoppingListSummary from "./components/ShoppingListSummary";
import Footer from "./components/Footer";
import { useState, useEffect } from "react";

function App() {
    const apiUrl = "https://localhost:7257/api/shoppinglistitems";
    const [list, setList] = useState([]);
    const [newItem, setNewItem] = useState("");
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const fetchData = async () => {
        fetch(apiUrl)
            .then(response => {
                if (response.ok) {
                    return response.json()
                }
                throw response;
            })
            .then(data => {
                console.log(data);
                setList(data);
            })
            .catch(error => {
                console.error("Error fetching data: ", error);
                setError(error);
            })
            .finally(() => {
                setLoading(false);
            });
    }

    const addItems = async (item) => {
        
        const createRequest = {
            name: item,
        };
        
        const options = {
            method: "POST",
            headers: {
                "content-Type": "application/json",
            },
            body: JSON.stringify(createRequest),
        };

        await fetch(apiUrl, options)
            .then(response => response.json())
            .then(data => {
                const items = [...list, data];
                setList(items);
            })
            .catch(error => console.error(error));
    };

    const handleCheck = async (id) => {
        const listItem = list.map((item) =>
            item.id === id
                ? {
                    ...item,
                    isPickedUp: !item.isPickedUp,
                }
                : item
        );

        setList(listItem);

        const item = listItem.filter((list) => list.id === id);

        const options = {
            method: "PUT",
            headers: {
                "content-Type": "application/json",
            },
            body: JSON.stringify({
                name: item[0].name,
                isPickedUp: item[0].isPickedUp,
            }),
        };

        const reqUrl = `${apiUrl}/${id}`;
        
        await fetch(reqUrl, options)
            .catch(error => console.error(error));
    };

    const handleDelete = async (id) => {
        const items = list.filter((item) => item.id !== id);
        setList(items);

        const options = {
            method: "DELETE",
        };

        const reqUrl = `${apiUrl}/${id}`;

        await fetch(reqUrl, options)
            .catch(error => consol.error(error));
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        addItems(newItem);

        setNewItem("");
    };
    
    useEffect(() => {
        fetchData();
    }, []);

    if (loading) return (
        <div className="App">
            <Header />
            <div className="loading">
                Loading Data...
            </div>
            <Footer />
        </div>
    );

    if (error) return (
        
        <div className="App">
            <Header />
            <div className="error">
                Error Loading Data! {error.message}
            </div>
            <Footer />
        </div>
    );
    
    return (
        <div className="App">
            <Header />

            <AddShoppingListItem
                newItem={newItem}
                setNewItem={setNewItem}
                handleSubmit={handleSubmit}
            />

            <ShoppingList
                list={list}
                handleCheck={handleCheck}
                handleDelete={handleDelete}
            />

            <ShoppingListSummary list={list} />

            <Footer />
        </div>
    );
}

export default App;
