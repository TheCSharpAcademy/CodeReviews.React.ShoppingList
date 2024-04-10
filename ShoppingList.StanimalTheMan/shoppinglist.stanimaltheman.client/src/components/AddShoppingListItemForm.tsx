import { useState } from "react";

const AddShoppingListItemForm = ({ onSubmit }) => {
    const [formData, setFormData] = useState({
        name: '',
        isPickedUp: false,
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prevData => {
            return {
                ...prevData,
                [name]: value,
            }
        })
    }

    // Define the fetch options for the POST request
    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(formData)
    };

    const handleAddItem = (e) => {
        e.preventDefault();
        fetch('https://localhost:7050/api/shoppinglistitem', options).then(res => res.json()).then(data => {
            onSubmit(data);
        });
    };
    return (
        <form onSubmit={handleAddItem}>
            <label>
                Item Name:
                <input
                    type="text"
                    name="name"
                    value={formData.name}
                    onChange={handleChange}
                />
            </label>
{/*            I chose not to enable users to add an already isPickedUp item.  Open to implement otherwise.*/}
           <button type="submit">Submit</button>
        </form>
    );
};

export default AddShoppingListItemForm;