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
        <div>
          <h2>All items from seller</h2>
          {error && <p style={{ color: 'red' }}>{error}</p>}
          <ul>
            {itemsList.map((item) => (
              <li key={item.id}>
                <p>Item name: {item.itemName}</p>
                <p>Price: {item.price}</p>
                <p>Amount: {item.amount}</p>
                <p>Description: {item.description}</p>

                <button onClick={() => handleEdit(item)}>Edit</button>
                <button onClick={() => handleDelete(item)}>Delete</button>
              </li>
            ))}
          </ul>
        </div>
      );
};

export default EditItemList;