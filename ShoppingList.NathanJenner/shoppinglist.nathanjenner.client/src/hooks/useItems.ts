import { useState, useEffect, useCallback } from 'react';
import { itemsApi } from '../api/itemsApi';
import type { ShoppingItem, ShoppingItemInput } from '../types/item';

export function useItems() {
    const [items, setItems] = useState<ShoppingItem[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        itemsApi.getAll()
            .then(setItems)
            .catch(() => setError('Failed to load items'))
            .finally(() => setLoading(false));
    }, []);

    const withErrorHandling = useCallback(async (action: () => Promise<void>) => {
        try {
            await action();
        } catch {
            setError('An error occurred. Please try again.');
        }
    }, []);

    const addItem = useCallback((item: ShoppingItemInput) =>
        withErrorHandling(async () => {
            const created = await itemsApi.create(item);
            setItems(prev => [...prev, created]);
        }), [withErrorHandling]);

    const updateItem = useCallback((id: number, item: ShoppingItemInput) =>
        withErrorHandling(async () => {
            const updated = await itemsApi.update(id, item);
            setItems(prev => prev.map(i => i.id === id ? updated : i));
        }), [withErrorHandling]);

    const deleteItem = useCallback((id: number) =>
        withErrorHandling(async () => {
            await itemsApi.remove(id);
            setItems(prev => prev.filter(i => i.id !== id));
        }), [withErrorHandling]);

    const toggleItem = useCallback((id: number) =>
        withErrorHandling(async () => {
            const updated = await itemsApi.toggle(id);
            setItems(prev => prev.map(i => i.id === id ? updated : i));
        }), [withErrorHandling]);

    const reorderItems = useCallback((reordered: ShoppingItem[]) =>
        withErrorHandling(async () => {
            setItems(reordered);
            await itemsApi.reorder(reordered.map(i => ({ id: i.id, sortOrder: i.sortOrder })));
        }), [withErrorHandling]);

    return { items, loading, error, addItem, updateItem, deleteItem, toggleItem, reorderItems };
}