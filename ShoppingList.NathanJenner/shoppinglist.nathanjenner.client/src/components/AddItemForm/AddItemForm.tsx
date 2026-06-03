import { useState } from 'react';
import { Button, Field, Input, SpinButton, makeStyles, tokens } from '@fluentui/react-components';
import { Add24Regular } from '@fluentui/react-icons';
import type { ShoppingItemInput } from '../../types/item';

const useStyles = makeStyles({
    form: {
        display: 'flex',
        flexDirection: 'row',
        alignItems: 'flex-end',
        gap: tokens.spacingHorizontalM,
        flexWrap: 'wrap',
        padding: tokens.spacingVerticalM,
        backgroundColor: tokens.colorNeutralBackground2,
        borderRadius: tokens.borderRadiusMedium,
    },
    field: {
        flexGrow: 1,
        minWidth: '150px',
    },
    quantityField: {
        width: '120px',
    },
});

interface AddItemFormProps {
    onAdd: (item: ShoppingItemInput) => Promise<void>;
}

export function AddItemForm({ onAdd }: AddItemFormProps) {
    const styles = useStyles();
    const [name, setName] = useState('');
    const [quantity, setQuantity] = useState(1);
    const [submitting, setSubmitting] = useState(false);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (!name.trim()) return;

        setSubmitting(true);
        try {
            await onAdd({ name: name.trim(), quantity });
            setName('');
            setQuantity(1);
        } finally {
            setSubmitting(false);
        }
    };

    return (
        <form className={styles.form} onSubmit={handleSubmit}>
            <Field label="Item" className={styles.field}>
                <Input
                    value={name}
                    onChange={(_, d) => setName(d.value)}
                    placeholder="e.g. Milk"
                    disabled={submitting}
                />
            </Field>
            <Field label="Quantity" className={styles.quantityField}>
                <SpinButton
                    value={quantity}
                    min={0}
                    step={1}
                    onChange={(_, d) => setQuantity(d.value ?? 1)}
                    disabled={submitting}
                />
            </Field>
            <Button
                type="submit"
                appearance="primary"
                icon={<Add24Regular />}
                disabled={!name.trim() || submitting}
            >
                Add
            </Button>
        </form>
    );
}