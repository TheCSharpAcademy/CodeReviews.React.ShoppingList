import type { ShoppingItem, ShoppingItemInput } from '../types/item';

const BASE_URL = '/api/items';

async function request<T>(url: string, options?: RequestInit): Promise<T> {
    const response = await fetch(url, {
        headers: options?.body ? { 'Content-Type': 'application/json' } : undefined,
        ...options,
    });
    if (!response.ok) throw new Error(`Request failed: ${response.status} ${response.statusText}`);
    if (response.status === 204) return undefined as T;
    return response.json();
}

export const itemsApi = {
    getAll: () => request<ShoppingItem[]>(BASE_URL),

    create: (item: ShoppingItemInput) =>
        request<ShoppingItem>(BASE_URL, { method: 'POST', body: JSON.stringify(item) }),

    update: (id: number, item: ShoppingItemInput) =>
        request<ShoppingItem>(`${BASE_URL}/${id}`, { method: 'PUT', body: JSON.stringify(item) }),

    remove: (id: number) =>
        request<void>(`${BASE_URL}/${id}`, { method: 'DELETE' }),

    toggle: (id: number) =>
        request<ShoppingItem>(`${BASE_URL}/${id}/toggle`, { method: 'PATCH' }),

    reorder: (items: Pick<ShoppingItem, 'id' | 'sortOrder'>[]) =>
        request<void>(`${BASE_URL}/reorder`, { method: 'PUT', body: JSON.stringify(items) }),
};