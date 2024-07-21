import {  useState } from "react";
import Button from 'react-bootstrap/Button';

function ShopItem({ inputItem,onPickChange,onDelete }) {
    const [isPicked, setIsPicked] = useState(inputItem.isPickedUp);
    var item = inputItem;
    return (<>
        <div className="container itemcontainer" style={{width:400} }>  
    <div className="row m-2">
        <div className="col-11">
            {isPicked ? (
                    <span onClick={HandlePickChange}><del>{inputItem.name}</del></span>
            ) : (
                        <span onClick={HandlePickChange}>{inputItem.name}</span>
            )}
        </div>
        <div className="col-1">
           
                    <Button variant="outline-warning" style={{ color: '#533e41', borderColor:'#533e41' }} size="sm" onClick={HandleDelete}>X</Button>
        </div>
        </div>
       </div>
        </>
    )

    function HandlePickChange() {
        setIsPicked(prev => !prev);
        item.isPickedUp = !item.isPickedUp;
        fetch(`https://localhost:7039/api/ShopItems/${inputItem.id}`, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json; charset=UTF-8'
            },
            body: JSON.stringify(item)
        });
        onPickChange(inputItem.id);    
    }

    function HandleDelete() {
        console.log(inputItem)
        if (confirm("Are you sure to delete this item?") == true) {
            fetch(`https://localhost:7039/api/ShopItems/${inputItem.id}`, {
                method: 'DELETE',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json; charset=UTF-8'
                }
            }).then(response => {
                console.log(response.status);
                if (response.status === 200) {
                    console.log(response.status);
                    onDelete(inputItem.id);
                }
                })
            ;
        }
        else return;
    }
}




export default ShopItem;