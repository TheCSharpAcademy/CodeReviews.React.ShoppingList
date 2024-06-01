import { useState } from 'react'
import './ShoppingList.css'
import ShoppingListItem from './ShoppingListItem.jsx';

function ShoppingList() {
  const [items, setItems] = useState([
      {
          id: 1,
          itemName: "Bananas",
          isPickedUp: false
      },
      {
          id: 2,
          itemName: "Cucumber",
          isPickedUp: false
      }
  ]);
  
  const [addItemText, setAddItemText] = useState('');
  
  function addItem(itemName)
  {
      const newItem = {
          id: items.length + 1,
          itemName: itemName,
          isPickedUp: false
      };
      
      setItems([...items, newItem]);
      setAddItemText('');
  }
  function deleteItem(id)
  {
      setItems(items.filter(item => item.id !== id));
  }
  
  function toggleIsPickedUp(id) {
      setItems(items.map(item => {
          if (item.id === id) {
              return {...item, isPickedUp: !item.isPickedUp};
          }
          else {
              return item;
          }
          }));
  }

  return (
      <div className="shopping-list">
          {
              items.map(item => (
                  <ShoppingListItem
                  key={item.id}
                  item={item}
                  deleteItem={deleteItem}
                  toggleIsPickedUp={toggleIsPickedUp}
                  />
              ))
          }
          
          <input
          value={addItemText}
          onChange={e => setAddItemText(e.target.value)}
          />
          <button onClick={() => addItem(addItemText)}>Add</button>
      </div>
  );
}

export default ShoppingList;