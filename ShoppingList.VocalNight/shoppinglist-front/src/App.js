
import { useEffect, useState } from 'react';
import './App.css';

import Item from './Components/Item';

function App() {
  const [data, setData] = useState([]);
  const [itemName, setItemName] = useState('');
  const [isInvalid, setIsInvalid] = useState(false);

  useEffect(() => {
    fetchData();
  }, [])

  const fetchData = async () => {
    fetch('https://localhost:7125/shoppingItems')
      .then((res) => res.json())
      .then((data) => {
        setData(data);
      })
      .catch((error) => {
        console.log(error);
      })
  };

  const AddItem = () => {

    if (itemName === '' || isInvalid === true) {
      return;
    }

    fetch('https://localhost:7125/shoppingItems', {
      method: 'POST',
      body: JSON.stringify({
        name: itemName, IsPickedUp: false
      }),
      headers: {
        "Content-type": "application/json; charset=UTF-8"
      }
    })
      .then(res => res.json())
      .then(data => fetchData());
  }

  function validateItem(event) {
    setItemName(event.target.value);
    setIsInvalid(
      data.some(({ name }) => name.toUpperCase() === event.target.value.toUpperCase())
    );
  }

  return (
    <>
      <h1>Shopping cart</h1>
      <div>
        <input
          type='text'
          data-testid="itemInput"
          value={itemName}
          onChange={event => validateItem(event)}
          placeholder='Add item to cart...'></input>
        <button data-testid="AddItemBut" onClick={() => AddItem()}>Include</button>
      </div>

      {data.length == 0 &&
        <div>
          <h3>Cart is empty</h3>
        </div>}

      {data.length != 0 &&
        <ul>
          {data.map((item) => (
            <Item 
              key={item.id + "div"}
              data-testid={item.name}
              shoppingItem={item}
              fetchData={() => fetchData()} />
          ))}
        </ul>}
      <br></br>

      {isInvalid &&
        <div>
          <h2>An Item with that name already exists!</h2>
        </div>
      }
    </>
  );
}

export default App;
