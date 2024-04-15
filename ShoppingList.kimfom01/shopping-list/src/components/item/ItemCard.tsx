import { ListItem } from "../../models/ListItem";
import { Delete } from "./Delete";
import { MarkAsComplete } from "./MarkAsComplete";
import "./itemCard.css";

interface Prop {
  item: ListItem;
  setDeleted: React.Dispatch<React.SetStateAction<boolean>>;
  deleted: boolean;
  setCompleted: React.Dispatch<React.SetStateAction<boolean>>;
  completed: boolean;
}

export const ItemCard = ({
  item,
  setDeleted,
  deleted,
  setCompleted,
  completed,
}: Prop) => {
  const greyColor = "#808080";

  return (
    <div
      className="item-card"
      style={{
        backgroundColor: item.isPickedUp ? greyColor : "",
      }}
    >
      <div className="flex-space">
        <div
          style={{
            textDecoration: item.isPickedUp ? "line-through" : "",
          }}
        >
          {item.item}
        </div>
        <div style={{ display: "flex", gap: "1rem" }}>
          <MarkAsComplete
            setCompleted={setCompleted}
            completed={completed}
            itemId={item.id ?? ""}
          />
          <Delete
            setDeleted={setDeleted}
            itemId={item.id ?? ""}
            deleted={deleted}
          />
        </div>
      </div>
    </div>
  );
};
