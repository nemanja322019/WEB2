import React, { useEffect, useState } from 'react';
import { IDisplayItem, IEditItem } from '../../Shared/Interfaces/itemInterfaces';
import { useLocation, useNavigate } from 'react-router-dom';
import { EditItem } from '../../Services/ItemService';
import { IUserProfile } from '../../Shared/Interfaces/userInterfaces';

const EditItemForm: React.FC = () => {
    const location = useLocation();
    const { userProfile, selectedItem } = location.state as { userProfile: IUserProfile, selectedItem: IDisplayItem };
    const [editedItem, setEditedItem] = useState<IEditItem>({
      itemName: selectedItem.itemName,
      price: selectedItem.price,
      amount: selectedItem.amount,
      description: selectedItem.description
    });
    const [error, setError] = useState<string | null>(null);

    const handleItemEdit = async (event: React.FormEvent) => {
      event.preventDefault();
  
      try {
        await EditItem(selectedItem.id,editedItem);
        setError('');
      } catch (err: any) {
        console.log('Error response:', err.response.data);
        setError(err.response?.data?.error || 'An error occurred');
      }
    };

    return (
        <div>
          <h2>Edit Item Page</h2>
          {error && <p style={{ color: 'red' }}>{error}</p>}
          <form onSubmit={handleItemEdit}>
          <div>
            <label>
              Item name:
              <input
                type="text"
                name="itemName"
                value={editedItem.itemName}
                onChange={(e) =>
                  setEditedItem({ ...editedItem, itemName: e.target.value })}
                disabled={false}
              />
            </label>
            </div>
            <div>
            <label>Price:</label>
                    <input
                        type="number"
                        step="0.01"
                        value={editedItem.price}
                        onChange={(e) =>
                          setEditedItem({ ...editedItem, price: parseFloat(e.target.value) })}
                    />
                </div>
                <div>
            <label>Amount:</label>
                    <input
                        type="number"
                        value={editedItem.amount}
                        onChange={(e) =>
                          setEditedItem({ ...editedItem, amount: parseInt(e.target.value) })}
                    />
                </div>
            <div>
                <label>Description:</label>
                <textarea
                value={editedItem.description}
                onChange={(e) =>
                  setEditedItem({ ...editedItem, description: e.target.value })}
                />
            </div>
            
            <button type="submit">Edit</button>
          </form>
        </div>
      );
    };
    
export default EditItemForm;