import { useState } from "react";
import { Product } from "../../../models/Product";
import ProductsService from "../../../services/ProductsService";

const ListItem = ({
  product,
  onDelete,
}: {
  product: Product;
  onDelete: () => void;
}) => {
  const [isPickedUp, setIsPickedUp] = useState(product.isPickedUp);

  const onPickupChange = async (): Promise<void> => {
    const updatedIsPickedUp = !isPickedUp;
    setIsPickedUp(updatedIsPickedUp);
    await ProductsService.updateProduct({
      ...product,
      isPickedUp: updatedIsPickedUp,
    });
  };

  const handleDelete = async (product: Product) => {
    await ProductsService.deleteProduct(product);
    onDelete();
  };

  return (
    <div className="flex items-center mt-4 justify-between w-full">
      <p
        onClick={onPickupChange}
        className={`font-playwrite border-b select-none cursor-pointer border-black w-full mr-10 ${
          isPickedUp ? "line-through" : ""
        }`}
      >
        {product.title}
      </p>

      <button
        onClick={async () => await handleDelete(product)}
        className="bg-red-300 hover:bg-red-500 transition-all duration-200 text-white font-bold px-2 py-1"
      >
        Delete
      </button>
    </div>
  );
};

export default ListItem;
