import React from "react";
import ListItem from "./ListItem";
import { Item } from "../types";

type listProps = {
  items: Item[];
  onCheck: (id: number) => void;
};

const ShoppingList: React.FC<listProps> = ({ items, onCheck }) => {
  if (!items.length) {
    return <p className="text-slate-400 p-4">You have no items...</p>;
  }
  return (
    <ul className="flex flex-col gap-2 mt-4 font-bold">
      {items.map((i) => (
        <ListItem item={i} key={i.id} onCheckClick={onCheck} />
      ))}
    </ul>
  );
};

export default ShoppingList;
