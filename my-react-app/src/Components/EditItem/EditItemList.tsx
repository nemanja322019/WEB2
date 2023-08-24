import React, { useEffect, useState } from 'react';
import { IDisplayItem } from '../../Shared/Interfaces/itemInterfaces';
import { useLocation, useNavigate } from 'react-router-dom';
import { DeleteItem, GetItemsForSeller } from '../../Services/ItemService';
import { IUserProfile } from '../../Shared/Interfaces/userInterfaces';

const EditItemList: React.FC = () => {
    const location = useLocation();
    const userProfile = (location.state as { userProfile: IUserProfile }).userProfile;
    const [itemsList, setItemsList] = useState<IDisplayItem[]>([]);
    const navigate = useNavigate();
    const [error, setError] = useState('');

    useEffect(() => {
        GetItemsForSeller(userProfile.id)
          .then((response) => {
            const items = response.data.map((displayItem: IDisplayItem) => {
              return {
                id: displayItem.id,
                itemName: displayItem.itemName,
                price: displayItem.price,
                amount: displayItem.amount,
                description: displayItem.description,
                image: displayItem.image
              };
            });
            setItemsList(items);
          })
          .catch((error) => {
            console.error('Error fetching items from seller:', error);
          });
      }, []); 

      const handleEdit = (selectedItem: IDisplayItem) => {
        navigate('/edititem', { state: { userProfile, selectedItem } });
      };
      const handleDelete = async (selectedItem: IDisplayItem) => {
        await DeleteItem(selectedItem.id)
          .then(() => {
            setItemsList(prevItems => prevItems.filter(item => item.id !== selectedItem.id));
          })
          .catch((err:any) => {
            setError(err.response?.data?.error || 'An error occurred'); 
          });
      };

      return (
        <div className="edit-item-list-container"> {/* Add a class name for the container */}
          <h2>All items from seller</h2>
          {error && <p className="error-message">{error}</p>} {/* Add a class for the error message */}
          <ul className="items-list"> {/* Add a class for the items list */}
            {itemsList.map((item) => (
              <li key={item.id} className="item">
                <p className="item-name">Item name: {item.itemName}</p>
                <p>Price: {item.price}</p>
                <p>Amount: {item.amount}</p>
                <p>Description: {item.description}</p>
                {item && item.image && (
                  <img
                    src={item.image}
                    alt="No Image"
                    style={{ maxWidth: '100px' }}
                    className="item-image"
                  />
                )}
                <div>
                <button className="edit-button" onClick={() => handleEdit(item)}>Edit</button>
                <button className="delete-button" onClick={() => handleDelete(item)}>Delete</button></div>
              </li>
            ))}
          </ul>
        </div>
      );
    };

export default EditItemList;