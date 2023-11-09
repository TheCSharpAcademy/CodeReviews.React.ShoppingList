import "./App.css";
import { useState, useEffect } from "react";
import { Item } from "./types";
import ShoppingList from "./components/ShoppingList";

function App() {
  const [items, setItems] = useState<Item[]>([]);

  async function getItems() {
    const response = await fetch("https://localhost:7189/api/ShoppingList");
    const items = await response.json();
    setItems(items);
  }

  async function handleCheck(id: number) {
    const item = items.find((i) => i.id === id);
    if (!item) return;
    item.isChecked = !item.isChecked;

    const res = await fetch(`https://localhost:7189/api/ShoppingList`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(item),
    });

    if (!res.ok) {
      console.log("Error updating item");
      return;
    }

    getItems();
  }

  async function formHandler(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    const formDate = new FormData(e.currentTarget);
    const newItem: Item = {
      name: formDate.get("item") as string,
      isChecked: false,
      id: 0,
    };

    const res = await fetch(`https://localhost:7189/api/ShoppingList`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(newItem),
    });

    if (!res.ok) {
      console.log("Error adding item");
      return;
    }

    getItems();

    const input = document.getElementById("item") as HTMLInputElement;
    input.value = "";
  }

  useEffect(() => {
    getItems();
  }, []);

  return (
    <div className="mx-auto">
      <h1 className="my-4 uppercase bg-slate-900 text-slate-200 p-2 font-bold">
        Shopping List
      </h1>
      <form className="form border p-2 rounded" onSubmit={formHandler}>
        <input
          type="text"
          name="item"
          placeholder="Add a new item"
          id="item"
          className="p-2 focus:outline-none"
        />
        <button
          type="submit"
          className="bg-slate-500 font-bold p-2 rounded text-slate-200"
        >
          Add Item
        </button>
      </form>

      <ShoppingList items={items} onCheck={handleCheck} />
    </div>
  );
}

export default App;
