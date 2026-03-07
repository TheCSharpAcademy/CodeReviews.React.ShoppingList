import { useEffect, useState } from 'react';
import './index.css';
import { useAppDispatch, useAppSelector } from './store/hooks';
import {
  addNewItem,
  cancelDelete,
  cancelEditing,
  fetchItems,
  removeItem,
  requestDelete,
  setEditName,
  setEditQuantity,
  startEditing,
  togglePurchased,
  updateExistingItem,
} from './store/shoppingListSlice';

function formatDateAdded(value: string) {
  const parsed = new Date(value);
  if (Number.isNaN(parsed.getTime())) return value;

  return parsed.toLocaleDateString(undefined, {
    year: 'numeric',
    month: 'short',
    day: 'numeric',
  });
}

function App() {
  const dispatch = useAppDispatch();
  const {
    items,
    loading,
    error,
    editingId,
    editName,
    editQuantity,
    itemBusyId,
    pendingDelete,
  } = useAppSelector(state => state.shoppingList);

  const [name, setName] = useState('');
  const [quantity, setQuantity] = useState(1);

  useEffect(() => {
    dispatch(fetchItems());
  }, [dispatch]);

  async function onAdd() {
    const trimmedName = name.trim();
    if (!trimmedName) return;

    await dispatch(
      addNewItem({
        name: trimmedName,
        quantity: Math.max(1, quantity),
      })
    );
    setName('');
    setQuantity(1);
  }

  async function onTogglePurchased(item: (typeof items)[0]) {
    await dispatch(togglePurchased(item));
  }

  async function onSaveEdit(item: (typeof items)[0]) {
    const trimmedName = editName.trim();
    if (!trimmedName) return;

    await dispatch(
      updateExistingItem({
        id: item.id,
        name: trimmedName,
        dateAdded: item.dateAdded,
        quantity: Math.max(1, editQuantity),
        isPurchased: item.isPurchased,
      })
    );
  }

  async function confirmDelete() {
    if (!pendingDelete) return;
    await dispatch(removeItem(pendingDelete.id));
  }

  return (
    <main className="app-shell">
      <section className="panel" aria-label="Shopping list panel">
        <header className="panel-header">
          <p className="eyebrow">Kitchen Tracker</p>
          <h1>Shopping List</h1>
        </header>

        <form
          className="add-form"
          onSubmit={e => {
            e.preventDefault();
            void onAdd();
          }}
        >
          <label>
            <span>Item name</span>
            <input
              value={name}
              onChange={e => setName(e.target.value)}
              placeholder="e.g. Tomatoes"
            />
          </label>
          <label>
            <span>Quantity</span>
            <input
              type="number"
              min={1}
              value={quantity}
              onChange={e => setQuantity(Number(e.target.value))}
            />
          </label>
          <button type="submit">Add Item</button>
        </form>

        {loading && <p className="status">Loading items...</p>}
        {error && <p className="status error">{error}</p>}

        <ul className="items-list">
          {items.map(item => (
            <li key={item.id} className="item-row">
              <div className="item-main">
                <input
                  className="check-toggle"
                  type="checkbox"
                  checked={item.isPurchased}
                  onChange={() => onTogglePurchased(item)}
                  disabled={itemBusyId === item.id}
                  aria-label={`Mark ${item.name} as purchased`}
                />

                {editingId === item.id ? (
                  <div className="edit-inline">
                    <input
                      value={editName}
                      onChange={e => dispatch(setEditName(e.target.value))}
                      placeholder="Item name"
                    />
                    <input
                      type="number"
                      min={1}
                      value={editQuantity}
                      onChange={e =>
                        dispatch(setEditQuantity(Number(e.target.value)))
                      }
                    />
                  </div>
                ) : (
                  <div>
                    <p
                      className={
                        item.isPurchased ? 'item-name purchased' : 'item-name'
                      }
                    >
                      {item.name}
                    </p>
                    <p className="item-meta">Qty: {item.quantity}</p>
                    <p className="item-date">
                      Added: {formatDateAdded(item.dateAdded)}
                    </p>
                  </div>
                )}
              </div>

              <div className="item-actions">
                <span
                  className={
                    item.isPurchased ? 'pill purchased' : 'pill pending'
                  }
                >
                  {item.isPurchased ? 'Purchased' : 'Pending'}
                </span>

                {editingId === item.id ? (
                  <>
                    <button
                      type="button"
                      className="item-btn"
                      onClick={() => onSaveEdit(item)}
                      disabled={itemBusyId === item.id}
                    >
                      Save
                    </button>
                    <button
                      type="button"
                      className="item-btn ghost"
                      onClick={() => dispatch(cancelEditing())}
                      disabled={itemBusyId === item.id}
                    >
                      Cancel
                    </button>
                  </>
                ) : (
                  <>
                    <button
                      type="button"
                      className="item-btn ghost"
                      onClick={() => dispatch(startEditing(item))}
                      disabled={itemBusyId === item.id}
                    >
                      Edit
                    </button>
                    <button
                      type="button"
                      className="item-btn danger"
                      onClick={() =>
                        dispatch(
                          requestDelete({ id: item.id, name: item.name })
                        )
                      }
                      disabled={itemBusyId === item.id}
                    >
                      Delete
                    </button>
                  </>
                )}
              </div>
            </li>
          ))}
        </ul>
      </section>

      {pendingDelete && (
        <dialog className="modal" open aria-labelledby="delete-modal-title">
          <h2 id="delete-modal-title">Delete item?</h2>
          <p>
            This will permanently remove <strong>{pendingDelete.name}</strong>{' '}
            from your shopping list.
          </p>

          <div className="modal-actions">
            <button
              type="button"
              className="item-btn ghost"
              onClick={() => dispatch(cancelDelete())}
              disabled={itemBusyId === pendingDelete.id}
            >
              Cancel
            </button>
            <button
              type="button"
              className="item-btn danger"
              onClick={() => void confirmDelete()}
              disabled={itemBusyId === pendingDelete.id}
            >
              {itemBusyId === pendingDelete.id ? 'Deleting...' : 'Delete'}
            </button>
          </div>
        </dialog>
      )}
    </main>
  );
}

export default App;
