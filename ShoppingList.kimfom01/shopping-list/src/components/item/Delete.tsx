import { Button } from "../utils/Button";

interface Prop {
  setDeleted: React.Dispatch<React.SetStateAction<boolean>>;
  deleted: boolean;
  itemId: string;
}

export const Delete = ({ itemId, setDeleted, deleted }: Prop) => {
  const handleClick = () => {
    fetch(`${import.meta.env.VITE_API_ROOT}/${itemId}`, {
      method: "delete",
    })
      .then((res) => {
        if (res.status === 204) {
          setDeleted(!deleted);
        }

        if (res.status == 404) {
          window.location.reload();
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
      ❌
    </Button>
  );
};
