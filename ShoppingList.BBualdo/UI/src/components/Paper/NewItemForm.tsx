import { useRef } from "react";
import { Product } from "../../../models/Product";
import ProductsService from "../../../services/ProductsService";

const NewItemForm = ({ onAdd }: { onAdd: () => void }) => {
  const ref = useRef<HTMLInputElement | null>(null);

  const submit = async (e: React.FormEvent) => {
    e.preventDefault();

    const newProduct: Product = {
      id: 0,
      isPickedUp: false,
      title: ref.current!.value,
    };
    await ProductsService.addProduct(newProduct);
    onAdd();
    ref.current!.value = "";
  };

  return (
    <form onSubmit={submit} className="flex items-center gap-4">
      <input
        ref={ref}
        type="text"
        required
        className="border-2 border-black py-1 px-2"
      />
      <button className="px-4 py-1 bg-green-500 hover:bg-green-700 transition-all duration-200 text-white font-bold">
        Add
      </button>
    </form>
  );
};

export default NewItemForm;
