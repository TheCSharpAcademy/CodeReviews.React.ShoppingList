import { url } from "../config/url";
import { PaginatedProducts } from "../models/PaginatedProducts";
import { Product } from "../models/Product";

class ProductsService {
  paginatedProducts: PaginatedProducts | null | undefined = undefined;

  async fetchProducts(page: number = 1, pageSize: number = 5): Promise<void> {
    const response = await fetch(
      url + `products/?page=${page}&pageSize=${pageSize}`
    );
    const data: PaginatedProducts = await response.json();
    this.paginatedProducts = data;
  }

  async addProduct(product: Product) {
    const response = await fetch(url + "products", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(product),
    });
    const newProduct: Product = await response.json();
    this.paginatedProducts!.products.push(newProduct);
  }

  async updateProduct(product: Product) {
    await fetch(url + "products", {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(product),
    });
    const productIndex = this.paginatedProducts!.products.findIndex(
      (p) => p.id === product.id
    );
    this.paginatedProducts!.products[productIndex] = product;
  }

  async deleteProduct(product: Product) {
    await fetch(url + `products/${product.id}`, {
      method: "DELETE",
      headers: { "Content-Type": "application/json" },
    });

    this.paginatedProducts!.products = this.paginatedProducts!.products.filter(
      (p) => p.id !== product.id
    );
  }

  getProducts(): PaginatedProducts | null | undefined {
    return this.paginatedProducts;
  }
}

export default new ProductsService();
