import { useState, useEffect } from 'react';
import {
    Button,
    Dialog,
    DialogActions,
    DialogBody,
    DialogContent,
    DialogSurface,
    DialogTitle,
    DialogTrigger,
    Field,
    Input,
    SpinButton,
    makeStyles,
    tokens,
    type InputOnChangeData,
    type SpinButtonOnChangeData,
} from '@fluentui/react-components';
import type { ShoppingItem } from '../../types/item';

const useStyles = makeStyles({
    content: {
        display: 'flex',
        flexDirection: 'column',
        gap: tokens.spacingVerticalM,
    },
});

interface EditItemDialogProps {
    item: ShoppingItem | null;
    onClose: () => void;
    onSave: (id: number, item: Omit<ShoppingItem, 'id' | 'isPurchased' | 'sortOrder'>) => Promise<void>;
}

export function EditItemDialog({ item, onClose, onSave }: EditItemDialogProps) {
    const styles = useStyles();
    const [name, setName] = useState('');
    const [quantity, setQuantity] = useState(1);
    const [saving, setSaving] = useState(false);

    useEffect(() => {
        if (item) {
            setName(item.name);
            setQuantity(item.quantity);
        }
    }, [item]);

    const handleSave = async () => {
        if (!item || !name.trim()) return;

        setSaving(true);
        try {
            await onSave(item.id, { name: name.trim(), quantity});
            onClose();
        } finally {
            setSaving(false);
        }
    };

    return (
        <Dialog open={!!item} onOpenChange={(_, d) => { if (!d.open) onClose(); }}>
            <DialogSurface>
                <DialogBody>
                    <DialogTitle>Edit Item</DialogTitle>
                    <DialogContent className={styles.content}>
                        <Field label="Item">
                            <Input
                                value={name}
                                onChange={(_e, d: InputOnChangeData) => setName(d.value)}
                                placeholder="e.g. Milk"
                                disabled={saving}
                            />
                        </Field>
                        <Field label="Quantity">
                            <SpinButton
                                value={quantity}
                                min={0}
                                step={1}
                                onChange={(_e, d: SpinButtonOnChangeData) => setQuantity(d.value ?? 1)}
                                disabled={saving}
                            />
                        </Field>
                    </DialogContent>
                    <DialogActions>
                        <DialogTrigger disableButtonEnhancement>
                            <Button appearance="secondary" onClick={onClose} disabled={saving}>
                                Cancel
                            </Button>
                        </DialogTrigger>
                        <Button
                            appearance="primary"
                            onClick={handleSave}
                            disabled={!name.trim() || saving}
                        >
                            Save
                        </Button>
                    </DialogActions>
                </DialogBody>
            </DialogSurface>
        </Dialog>
    );
}