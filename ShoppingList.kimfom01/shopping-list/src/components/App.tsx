import "./App.css";
import { useState, useEffect } from "react";
import { ItemCard } from "./item/ItemCard";
import { ListItem } from "../models/ListItem";
import { Header } from "./utils/Header";
import { Button } from "./utils/Button";
import { NewItem } from "./item/NewItem";

export const App = () => {
  const [shoppingItems, setShoppingItems] = useState<ListItem[]>();
  const [newFormModal, setNewFormModal] = useState<boolean>(false);
  const [added, setAdded] = useState<boolean>(false);
  const [completed, setCompleted] = useState<boolean>(false);
  const [deleted, setDeleted] = useState<boolean>(false);

  const handleIsOpen = () => {
    setNewFormModal(true);
  };

  const handleModalClose = () => {
    setNewFormModal(false);
  };

  useEffect(() => {
    fetch(import.meta.env.VITE_API_ROOT)
      .then((res) => res.json())
      .then((data: ListItem[]) => setShoppingItems(data))
      .catch((err) => console.error(err.message));
  }, [added, newFormModal, completed, deleted]);

  const submitNewItem = (data: ListItem) => {
    fetch(import.meta.env.VITE_API_ROOT, {
      method: "post",
      body: JSON.stringify(data),
      headers: {
        "Content-Type": "application/json",
      },
    })
      .then((res) => {
        if (res.status == 204) {
          setAdded(true);
          handleModalClose();
        } else {
          setAdded(false);
        }
      })
      .catch((err) => console.error(err));
  };

  return (
    <>
      <main className="main">
        <Header />
        <NewItem
          onSubmit={submitNewItem}
          isOpen={newFormModal}
          onClose={handleModalClose}
        />
        <Button
          style={{
            padding: "0.6em 1.2em",
            borderRadius: "8px",
            width: "fit-content",
            alignSelf: "center",
          }}
          onClick={handleIsOpen}
        >
          Add New Item
        </Button>
        <div className="items-container">
          <div className="content">
            {shoppingItems?.map((item, idx) => {
              return (
                <ItemCard
                  key={idx}
                  item={item}
                  setDeleted={setDeleted}
                  deleted={deleted}
                  setCompleted={setCompleted}
                  completed={completed}
                />
              );
            })}
          </div>
        </div>
      </main>
    </>
  );
};
