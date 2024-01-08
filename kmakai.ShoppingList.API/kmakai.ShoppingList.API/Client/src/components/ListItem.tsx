import React from "react";
import { Item } from "../types";

type itemProps = {
  item: Item;
  onCheckClick: (id: number) => void;
};

const ListItem: React.FC<itemProps> = ({ item, onCheckClick }) => {
  return (
    <li className="flex gap-4 rounded-sm border-b p-3 items-center">
      <input
        type="checkbox"
        defaultChecked={item.isChecked}
        onClick={() => {
          onCheckClick(item.id);
        }}
        className="w-5 h-5 border-2 border-slate-400 rounded-sm"
      />
      <span
        className={
          item.isChecked
            ? "line-through text-slate-400 uppercase tracking-wide"
            : "uppercase tracking-wide"
        }
      >
        {item.name}
      </span>
    </li>
  );
};

export default ListItem;
