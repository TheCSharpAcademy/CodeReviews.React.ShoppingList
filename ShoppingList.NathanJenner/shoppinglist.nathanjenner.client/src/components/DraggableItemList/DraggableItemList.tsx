import { DragDropContext, Droppable } from '@hello-pangea/dnd';
import type { DropResult } from '@hello-pangea/dnd';
import { Text, makeStyles, tokens } from '@fluentui/react-components';
import { ShoppingItemRow } from '../ShoppingItemRow/ShoppingItemRow';
import type { ShoppingItem } from '../../types/item';

const useStyles = makeStyles({
    container: {
        display: 'flex',
        flexDirection: 'column',
        gap: tokens.spacingVerticalS,
    },
    empty: {
        textAlign: 'center',
        color: tokens.colorNeutralForeground3,
        padding: tokens.spacingVerticalXXL,
    },
});

interface DraggableItemListProps {
    items: ShoppingItem[];
    onToggle: (id: number) => Promise<void>;
    onEdit: (item: ShoppingItem) => void;
    onDelete: (id: number) => Promise<void>;
    onReorder: (items: ShoppingItem[]) => Promise<void>;
}

export function DraggableItemList({ items, onToggle, onEdit, onDelete, onReorder }: DraggableItemListProps) {
    const styles = useStyles();

    const handleDragEnd = async (result: DropResult) => {
        if (!result.destination || result.destination.index === result.source.index) return;

        const reordered = Array.from(items);
        const [moved] = reordered.splice(result.source.index, 1);
        reordered.splice(result.destination.index, 0, moved);

        const withUpdatedSortOrder = reordered.map((item, index) => ({
            ...item,
            sortOrder: index,
        }));

        await onReorder(withUpdatedSortOrder);
    };

    if (items.length === 0) {
        return (
            <Text className={styles.empty}>
                No items yet — add one above!
            </Text>
        );
    }

    return (
        <DragDropContext onDragEnd={handleDragEnd}>
            <Droppable droppableId="shopping-list">
                {(provided) => (
                    <div
                        className={styles.container}
                        ref={provided.innerRef}
                        {...provided.droppableProps}
                    >
                        {items.map((item, index) => (
                            <ShoppingItemRow
                                key={item.id}
                                item={item}
                                index={index}
                                onToggle={onToggle}
                                onEdit={onEdit}
                                onDelete={onDelete}
                            />
                        ))}
                        {provided.placeholder}
                    </div>
                )}
            </Droppable>
        </DragDropContext>
    );
}