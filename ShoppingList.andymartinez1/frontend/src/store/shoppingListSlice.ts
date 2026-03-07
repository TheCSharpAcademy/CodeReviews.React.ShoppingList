import {
  createAsyncThunk,
  createSlice,
  type PayloadAction,
} from '@reduxjs/toolkit';
import type { ShoppingItem } from '../api/shoppingListApi';
import * as api from '../api/shoppingListApi';

interface ShoppingListState {
  items: ShoppingItem[];
  loading: boolean;
  error: string | null;
  editingId: number | null;
  editName: string;
  editQuantity: number;
  itemBusyId: number | null;
  pendingDelete: { id: number; name: string } | null;
}

const initialState: ShoppingListState = {
  items: [],
  loading: false,
  error: null,
  editingId: null,
  editName: '',
  editQuantity: 1,
  itemBusyId: null,
  pendingDelete: null,
};

// Async thunks
export const fetchItems = createAsyncThunk(
  'shoppingList/fetchItems',
  async () => {
    return await api.getItems();
  }
);

export const addNewItem = createAsyncThunk(
  'shoppingList/addNewItem',
  async (payload: { name: string; quantity: number }) => {
    return await api.addItem({
      name: payload.name,
      dateAdded: new Date().toISOString(),
      quantity: payload.quantity,
    });
  }
);

export const togglePurchased = createAsyncThunk(
  'shoppingList/togglePurchased',
  async (item: ShoppingItem) => {
    return await api.updateItem(item.id, {
      ...item,
      isPurchased: !item.isPurchased,
    });
  }
);

export const updateExistingItem = createAsyncThunk(
  'shoppingList/updateExistingItem',
  async (payload: {
    id: number;
    name: string;
    dateAdded: string;
    quantity: number;
    isPurchased: boolean;
  }) => {
    return await api.updateItem(payload.id, payload);
  }
);

export const removeItem = createAsyncThunk(
  'shoppingList/removeItem',
  async (id: number) => {
    await api.deleteItem(id);
    return id;
  }
);

const shoppingListSlice = createSlice({
  name: 'shoppingList',
  initialState,
  reducers: {
    startEditing: (state, action: PayloadAction<ShoppingItem>) => {
      state.editingId = action.payload.id;
      state.editName = action.payload.name;
      state.editQuantity = action.payload.quantity;
      state.error = null;
    },
    cancelEditing: state => {
      state.editingId = null;
      state.editName = '';
      state.editQuantity = 1;
    },
    setEditName: (state, action: PayloadAction<string>) => {
      state.editName = action.payload;
    },
    setEditQuantity: (state, action: PayloadAction<number>) => {
      state.editQuantity = action.payload;
    },
    requestDelete: (
      state,
      action: PayloadAction<{ id: number; name: string }>
    ) => {
      state.pendingDelete = action.payload;
    },
    cancelDelete: state => {
      if (state.itemBusyId !== state.pendingDelete?.id) {
        state.pendingDelete = null;
      }
    },
    clearError: state => {
      state.error = null;
    },
  },
  extraReducers: builder => {
    builder
      // Fetch items
      .addCase(fetchItems.pending, state => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchItems.fulfilled, (state, action) => {
        state.loading = false;
        state.items = action.payload;
      })
      .addCase(fetchItems.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message || 'Failed to load items';
      })
      // Add new item
      .addCase(addNewItem.pending, state => {
        state.error = null;
      })
      .addCase(addNewItem.fulfilled, (state, action) => {
        state.items.push(action.payload);
      })
      .addCase(addNewItem.rejected, (state, action) => {
        state.error = action.error.message || 'Failed to add item';
      })
      // Toggle purchased
      .addCase(togglePurchased.pending, (state, action) => {
        state.itemBusyId = action.meta.arg.id;
        state.error = null;
      })
      .addCase(togglePurchased.fulfilled, (state, action) => {
        state.itemBusyId = null;
        const index = state.items.findIndex(
          item => item.id === action.payload.id
        );
        if (index !== -1) {
          state.items[index] = action.payload;
        }
      })
      .addCase(togglePurchased.rejected, (state, action) => {
        state.itemBusyId = null;
        state.error = action.error.message || 'Failed to toggle purchased';
      })
      // Update item
      .addCase(updateExistingItem.pending, (state, action) => {
        state.itemBusyId = action.meta.arg.id;
        state.error = null;
      })
      .addCase(updateExistingItem.fulfilled, (state, action) => {
        state.itemBusyId = null;
        state.editingId = null;
        state.editName = '';
        state.editQuantity = 1;
        const index = state.items.findIndex(
          item => item.id === action.payload.id
        );
        if (index !== -1) {
          state.items[index] = action.payload;
        }
      })
      .addCase(updateExistingItem.rejected, (state, action) => {
        state.itemBusyId = null;
        state.error = action.error.message || 'Failed to update item';
      })
      // Remove item
      .addCase(removeItem.pending, (state, action) => {
        state.itemBusyId = action.meta.arg;
        state.error = null;
      })
      .addCase(removeItem.fulfilled, (state, action) => {
        state.itemBusyId = null;
        state.pendingDelete = null;
        if (state.editingId === action.payload) {
          state.editingId = null;
          state.editName = '';
          state.editQuantity = 1;
        }
        state.items = state.items.filter(item => item.id !== action.payload);
      })
      .addCase(removeItem.rejected, (state, action) => {
        state.itemBusyId = null;
        state.error = action.error.message || 'Failed to delete item';
      });
  },
});

export const {
  startEditing,
  cancelEditing,
  setEditName,
  setEditQuantity,
  requestDelete,
  cancelDelete,
  clearError,
} = shoppingListSlice.actions;

export default shoppingListSlice.reducer;
