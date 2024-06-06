import React from 'react'
import ReactDOM from 'react-dom/client'
import ShoppingList from './ShoppingList.jsx';
import './index.css'
import { ShoppingListHeader } from './ShoppingListHeader.jsx';

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
      <ShoppingListHeader />
      <ShoppingList />
  </React.StrictMode>,
)
