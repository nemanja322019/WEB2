import React, { useEffect, useState } from 'react';
import { ICreateItem } from '../../Shared/Interfaces/itemInterfaces';
import { useLocation, useNavigate } from 'react-router-dom';
import { CreateItem } from '../../Services/ItemService';
import { IUserProfile } from '../../Shared/Interfaces/userInterfaces';


const CreateItemForm: React.FC = () => {
    const location = useLocation();
    const userProfile = (location.state as { userProfile: IUserProfile }).userProfile;
    const [itemName, setItemName] = useState('');
    const [price, setPrice] = useState<number>(0); 
    const [amount, setAmount] = useState<number>(0)
    const [description, setDescription] = useState('');
    const [error, setError] = useState('');

    const handleCreateItem = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        try {
          const itemData: ICreateItem = {
            itemName,
            price,
            amount,
            description,
            sellerid: userProfile?.id || 0, 
          };
          const response = await CreateItem(itemData);
          console.log("Item added!");
          
        } catch (err: any) {
          console.log('Error response:', err.response.data);
          setError(err.response?.data?.error || 'An error occurred');
        }
      };

      return (
        <div>
          <h2>Create Item</h2>
          {error && <p style={{ color: 'red' }}>{error}</p>}
          <form onSubmit={handleCreateItem}>
                <div>
                 <label>Item name:</label>
                 <input
                   type="text"
                   value={itemName}
                   onChange={(e) => setItemName(e.target.value)}
                 />
               </div>
               <div>
            <label>Price:</label>
                    <input
                        type="number"
                        step="0.01"
                        value={price}
                        onChange={(e) => setPrice(parseFloat(e.target.value))}
                    />
                </div>
                <div>
            <label>Amount:</label>
                    <input
                        type="number"
                        value={amount}
                        onChange={(e) => setAmount(parseInt(e.target.value))}
                    />
                </div>
            <div>
                <label>Description:</label>
                <textarea
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                />
            </div>
            <button type="submit">Create Item</button>
          </form>
        </div>
      );
};

export default CreateItemForm;
