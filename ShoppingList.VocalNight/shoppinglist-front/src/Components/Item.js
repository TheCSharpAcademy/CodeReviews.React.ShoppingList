import { useEffect, useState } from "react";

export default function Item({ shoppingItem, fetchData }) {
    const [item, setItem] = useState(shoppingItem);
    const [lineClass, setLineClass] = useState('');

    useEffect(() => {
        validateCheckbox(shoppingItem.isPickedUp);
    }, []);

    const validateCheckbox = (isDashed) => {
        if (isDashed) {
            setLineClass('checkboxLined');
        } else {
            setLineClass('');
        }
    }

    const changeState = (event) => {
        let newItem = { ...item, isPickedUp: !item.isPickedUp, }

        validateCheckbox(newItem.isPickedUp);

        fetch('https://localhost:7125/shoppingItems/' + shoppingItem.id, {
            method: 'PUT',
            body: JSON.stringify(newItem),
            headers: {
                "Content-type": "application/json; charset=UTF-8"
            },
        })
            .then(res => {
                setItem(newItem);
                fetchData();
            })
    }

    const deleteItem = () => {

        fetch('https://localhost:7125/shoppingItems/' + shoppingItem.id, {
            method: 'DELETE'
        }).then(res => fetchData());
    }

    if (shoppingItem) {
        return (
            <div >

                <li data-testid="checkboxLabel" className={lineClass}>{shoppingItem.name}
                    <input
                        data-testid="checkboxItem"
                        checked={item.isPickedUp}
                        onChange={(e) => changeState(e)}
                        type='checkbox'></input></li>

                <button data-testid="itemDelete" onClick={() => deleteItem()}>Delete</button>
            </div>
        );
    }

    return (
        <></>
    );


}
