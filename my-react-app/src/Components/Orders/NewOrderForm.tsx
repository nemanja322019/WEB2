import React, { useEffect, useState } from 'react';
import { IDisplayItem } from '../../Shared/Interfaces/itemInterfaces';
import { useLocation, useNavigate } from 'react-router-dom';
import { IUserProfile } from '../../Shared/Interfaces/userInterfaces';
import { NewOrder } from '../../Services/OrderService';
import { IOrderItem } from '../../Shared/Interfaces/orderInterfaces';
import './NewOrderForm.css';

const NewOrderForm: React.FC = () => {
    const location = useLocation();
    const { userProfile, newOrderItems, comment } = location.state as { userProfile: IUserProfile, newOrderItems: IOrderItem[], comment: string };
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();


    const mergeOrderItems = (items: IOrderItem[]): IOrderItem[] => {
        const mergedItemsMap: Record<number, number> = {};

        items.forEach(item => {
            if (mergedItemsMap[item.itemId]) {
                mergedItemsMap[item.itemId] += item.amount;
            } else {
                mergedItemsMap[item.itemId] = item.amount;
            }
        });

        const mergedItems: IOrderItem[] = [];

        for (const itemId in mergedItemsMap) {
            mergedItems.push({
                itemId: parseInt(itemId),
                amount: mergedItemsMap[itemId],
            });
        }

        return mergedItems;
    };

    const mergedOrderItems = mergeOrderItems(newOrderItems);
    console.log("Naruceni: ",mergedOrderItems);
    const handleNewOrder = async () => {
        try {
            const newOrder = {
                comment: comment,
                address: userProfile.address,
                customerId: userProfile.id,
                orderItems: mergedOrderItems,
            };

            await NewOrder(newOrder);
            navigate('/availableitems', { state: { userProfile }});
        } catch (err : any) {
            setError(err.response?.data?.error || 'An error occurred'); 
        }
    };

    const handleDiscard = async () => {
        navigate('/availableitems', { state: { userProfile }});
    };

    return (
        <div className="container">
          <h2>Order Overview</h2>
          {error && <p className="error-message">{error}</p>}
          <div className="order-items">
            <h3>Order Items:</h3>
            <h5>Shipping price: 200</h5>
            <ul>
              {mergedOrderItems.map((item, index) => (
                <li className="order-item" key={index}>
                  <p>Item ID: {item.itemId}</p>
                  <p>Amount: {item.amount}</p>
                </li>
              ))}
            </ul>
          </div>
          <div className="action-buttons">
            <button className="action-button" onClick={handleNewOrder}>
              Confirm
            </button>
            <button className="action-button" onClick={handleDiscard}>
              Discard
            </button>
          </div>
        </div>
      );
    };

export default NewOrderForm;