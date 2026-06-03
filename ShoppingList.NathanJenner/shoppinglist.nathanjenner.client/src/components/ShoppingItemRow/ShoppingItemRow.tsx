import {
    Badge,
    Button,
    Checkbox,
    Text,
    makeStyles,
    mergeClasses,
    tokens,
} from '@fluentui/react-components';
import { Delete24Regular, Edit24Regular, DragRegular } from '@fluentui/react-icons';
import { Draggable } from '@hello-pangea/dnd';
import type { ShoppingItem } from '../../types/item';

const useStyles = makeStyles({
    row: {
        display: 'flex',
        alignItems: 'center',
        gap: tokens.spacingHorizontalM,
        padding: `${tokens.spacingVerticalS} ${tokens.spacingHorizontalM}`,
        backgroundColor: tokens.colorNeutralBackground1,
        borderRadius: tokens.borderRadiusMedium,
        boxShadow: tokens.shadow4,
    },
    dragHandle: {
        cursor: 'grab',
        color: tokens.colorNeutralForeground3,
        display: 'flex',
        alignItems: 'center',
    },
    itemText: {
        flexGrow: 1,
    },
    purchased: {
        textDecorationLine: 'line-through',
        color: tokens.colorNeutralForeground3,
    },
    actions: {
        display: 'flex',
        gap: tokens.spacingHorizontalS,
    },
});

interface ShoppingItemRowProps {
    item: ShoppingItem;
    index: number;
    onToggle: (id: number) => Promise<void>;
    onEdit: (item: ShoppingItem) => void;
    onDelete: (id: number) => Promise<void>;
}

export function ShoppingItemRow({ item, index, onToggle, onEdit, onDelete }: ShoppingItemRowProps) {
    const styles = useStyles();

    return (
        <Draggable draggableId={String(item.id)} index={index}>
            {(provided) => (
                <div
                    className={styles.row}
                    ref={provided.innerRef}
                    {...provided.draggableProps}
                >
                    <span className={styles.dragHandle} {...provided.dragHandleProps}>
                        <DragRegular />
                    </span>
                    <Checkbox
                        checked={item.isPurchased}
                        onChange={() => onToggle(item.id)}
                    />
                    <Text
                        className={mergeClasses(styles.itemText, item.isPurchased && styles.purchased)}
                    >
                        {item.name}
                    </Text>
                    <Badge appearance="filled" color="informative">
                        {item.quantity}
                    </Badge>
                    <div className={styles.actions}>
                        <Button
                            appearance="subtle"
                            icon={<Edit24Regular />}
                            onClick={() => onEdit(item)}
                            aria-label="Edit item"
                        />
                        <Button
                            appearance="subtle"
                            icon={<Delete24Regular />}
                            onClick={() => onDelete(item.id)}
                            aria-label="Delete item"
                        />
                    </div>
                </div>
            )}
        </Draggable>
    );
}