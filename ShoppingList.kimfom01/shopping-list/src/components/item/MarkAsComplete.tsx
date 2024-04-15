import { Button } from "../utils/Button";

interface Prop {
  setCompleted: React.Dispatch<React.SetStateAction<boolean>>;
  completed: boolean;
  itemId: string;
}

export const MarkAsComplete = ({ setCompleted, completed, itemId }: Prop) => {
  const handleClick = () => {
    fetch(`${import.meta.env.VITE_API_ROOT}/${itemId}`, {
      method: "patch",
    })
      .then((res) => {
        if (res.status === 204) {
          setCompleted(!completed);
        }
      })
      .catch((err) => console.error(err.message));
  };

  return (
    <Button
      style={{
        padding: "0.3rem",
        paddingRight: "0.8rem",
        paddingLeft: "0.8rem",
        backgroundColor: "inherit",
        borderRadius: "8px",
      }}
      onClick={handleClick}
    >
      ✅
    </Button>
  );
};
