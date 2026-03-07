export type ShoppingItem = {
  id: number;
  name: string;
  dateAdded: string;
  quantity: number;
  isPurchased: boolean;
};

export type GroceryItemAddRequest = {
  name: string;
  dateAdded: string;
  quantity: number;
};

export type GroceryItemUpdateRequest = {
  id: number;
  name: string;
  dateAdded: string;
  quantity: number;
  isPurchased: boolean;
};

async function api<T>(url: string, init?: RequestInit): Promise<T> {
  const res = await fetch(url, {
    headers: { 'Content-Type': 'application/json', ...init?.headers },
    ...init,
  });

  if (!res.ok) throw new Error(`${res.status} ${res.statusText}`);
  if (res.status === 204) return undefined as T;
  return res.json() as Promise<T>;
}

const BASE = '/api/shoppinglist';

export const getItems = () => api<ShoppingItem[]>(BASE);
export const getItem = (id: number) => api<ShoppingItem>(`${BASE}/${id}`);
export const addItem = (body: GroceryItemAddRequest) =>
  api<ShoppingItem>(BASE, { method: 'POST', body: JSON.stringify(body) });
export const updateItem = (id: number, body: GroceryItemUpdateRequest) =>
  api<ShoppingItem>(`${BASE}/${id}`, {
    method: 'PUT',
    body: JSON.stringify(body),
  });
export const deleteItem = (id: number) =>
  api<void>(`${BASE}/${id}`, { method: 'DELETE' });
