export interface ShoppingItem {
  id: number;
  name: string;
  quantity: number;
  isPurchased: boolean;
  sortOrder: number;
}

export type ShoppingItemInput = Omit<ShoppingItem, 'id' | 'isPurchased' | 'sortOrder'>;