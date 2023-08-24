import React, { useEffect, useState } from 'react';
import { IDisplayItem } from '../../Shared/Interfaces/itemInterfaces';
import { useLocation, useNavigate } from 'react-router-dom';
import { GetItems } from '../../Services/ItemService';
import { IUserProfile } from '../../Shared/Interfaces/userInterfaces';
import { IOrderItem } from '../../Shared/Interfaces/orderInterfaces';
import AmountModal from './AmountModal'; 
import './AvailableItemsForm.css'; 

const AvailableItemsForm: React.FC = () => {
    const location = useLocation();
    const userProfile = (location.state as { userProfile: IUserProfile }).userProfile;
    const [itemsList, setItemsList] = useState<IDisplayItem[]>([]);
    const navigate = useNavigate();
    const [comment, setComment] = useState('');
    const [newOrderItems, setNewOrderItems] = useState<IOrderItem[]>([]); 

    const [isModalOpen, setIsModalOpen] = useState(false);
    const [selectedItem, setSelectedItem] = useState<IDisplayItem | null>(null);

    useEffect(() => {
        GetItems()
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
            console.error('Error fetching all items:', error);
          });
          setNewOrderItems([]);
      }, []); 

      const handleBasket = (selectedItem: IDisplayItem) => {
        setSelectedItem(selectedItem);
        setIsModalOpen(true);
      };

      const handleModalClose = () => {
        setIsModalOpen(false);
      };

      const handleModalSave = (selectedAmount: number) => {
        if (selectedItem) {
          newOrderItems.push({
            itemId: selectedItem.id,
            amount: selectedAmount,
          });
        }
        setIsModalOpen(false);
      };

      const handleOrder = () => {
        navigate('/neworder', { state: { userProfile, newOrderItems, comment } });
      };

      return (
        <div className="available-items-container">
          <h2>All available items</h2>
          <div className="order-section">
            <label className="comment-label">Comment:</label>
            <input
              className="comment-input"
              type="text"
              value={comment}
              onChange={(e) => setComment(e.target.value)}
            />
            <button className="order-button" onClick={handleOrder}>Order</button>
            {isModalOpen && (
              <AmountModal onClose={handleModalClose} onSave={handleModalSave} />
            )}
          </div>
          <div className="items-list">
            {itemsList.map((item) => (
              <div key={item.id} className="item-box">
                <div className="item-info">
                  <p className="item-name">{item.itemName}</p>
                  <p className="item-price">Price: {item.price}</p>
                  <p className="item-amount">Available: {item.amount}</p>
                  <p className="item-description">{item.description}</p>
                </div>
                <div className="item-image">
                  {item && item.image && (
                    <img src={item.image} alt="No Image" />
                  )}
                </div>
                <button className="add-to-basket-button" onClick={() => handleBasket(item)}>Add to basket</button>
              </div>
            ))}
          </div>
          
        </div>
      );
    };

export default AvailableItemsForm;