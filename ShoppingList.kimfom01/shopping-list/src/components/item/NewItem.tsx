import { useState } from "react";
import { ListItem } from "../../models/ListItem";
import { Modal } from "../modal/Modal";
import { Button } from "../utils/Button";
import "./newItem.css";

interface AddNewItemProps {
  isOpen: boolean;
  onSubmit: (data: ListItem) => void;
  onClose: () => void;
}

export const NewItem = ({ isOpen, onSubmit, onClose }: AddNewItemProps) => {
  const defaultState: ListItem = {
    isPickedUp: false,
    item: "",
  };
  const [formState, setFormState] = useState<ListItem>(defaultState);

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    setFormState((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSumit = (event: React.FormEvent) => {
    event.preventDefault();
    formState && onSubmit(formState);
    resetForm();
  };

  const resetForm = () => {
    setFormState(defaultState);
  };

  return (
    <Modal isOpened={isOpen} onClose={onClose} hasCloseBtn={true}>
      <form method="post" onSubmit={handleSumit}>
        <h3>Enter new item</h3>
        <input
          id="item"
          value={formState?.item}
          type="text"
          onChange={handleInputChange}
          name="item"
          required
          placeholder="Enter your input"
          className="input-style"
        />
        <Button
          style={{
            padding: "0.6em 1.2em",
            borderRadius: "8px",
          }}
          type="submit"
        >
          Add
        </Button>
      </form>
    </Modal>
  );
};
