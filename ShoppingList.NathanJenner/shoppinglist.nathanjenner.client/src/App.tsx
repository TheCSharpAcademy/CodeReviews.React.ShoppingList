import './App.css';
import { useState } from 'react';
import { Text, makeStyles, tokens } from '@fluentui/react-components';
import { AddItemForm } from './components/AddItemForm/AddItemForm';
import { DraggableItemList } from './components/DraggableItemList/DraggableItemList';
import { EditItemDialog } from './components/EditItemDialog/EditItemDialog';
import { useItems } from './hooks/useItems';
import type { ShoppingItem } from './types/item';

const useStyles = makeStyles({
    root: {
        maxWidth: '700px',
        margin: '0 auto',
        padding: tokens.spacingVerticalXXL,
        display: 'flex',
        flexDirection: 'column',
        gap: tokens.spacingVerticalL,
    },
    header: {
        textAlign: 'center',
        color: tokens.colorPaletteGreenForeground1,
    },
    error: {
        color: tokens.colorPaletteRedForeground1,
        textAlign: 'center',
    },
});

function App() {
    const styles = useStyles();
    const { items, loading, error, addItem, updateItem, deleteItem, toggleItem, reorderItems } = useItems();
    const [editingItem, setEditingItem] = useState<ShoppingItem | null>(null);

    if (loading) return <Text>Loading...</Text>;
    if (error) return <Text className={styles.error}>{error}</Text>;

    return (
        <div className={styles.root}>
            <Text as="h1" size={800} weight="bold" className={styles.header}>
                🛒 Shopping List
            </Text>
            <AddItemForm onAdd={addItem} />
            <DraggableItemList
                items={items}
                onToggle={toggleItem}
                onEdit={setEditingItem}
                onDelete={deleteItem}
                onReorder={reorderItems}
            />
            <EditItemDialog
                item={editingItem}
                onClose={() => setEditingItem(null)}
                onSave={updateItem}
            />
        </div>
    );
}

export default App;