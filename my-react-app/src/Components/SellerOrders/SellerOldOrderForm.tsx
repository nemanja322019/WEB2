import React, { useEffect, useState } from 'react';
import { IOrder } from '../../Shared/Interfaces/orderInterfaces'; 
import { GetOldOrdersFromSeller } from '../../Services/OrderService';
import { useLocation } from 'react-router-dom';
import { IUserProfile } from '../../Shared/Interfaces/userInterfaces';
import './SellerOrders.css'; 

const SellerNewOrdersForm: React.FC = () => {
    const location = useLocation();
    const userProfile = (location.state as { userProfile: IUserProfile }).userProfile;
  const [orders, setOrders] = useState<IOrder[]>([]);
  const [error, setError] = useState('');

  useEffect(() => {
    GetOldOrdersFromSeller(userProfile.id)
      .then((response) => {
        const fetchedOrders: IOrder[] = response.data;
        setOrders(fetchedOrders);
        console.log('Fetched customer orders:', fetchedOrders);
      })
      .catch((error) => {
        console.error('Error fetching customer orders:', error);
        setError('Error fetching customer orders');
      });
  }, [userProfile.id]);

  return (
    <div className="container">
      <h2>Old Orders</h2>
      {error && <p className="error-message">{error}</p>}
      <ul className="orders-list">
        {orders.map((order) => (
          <li key={order.id} className="order-item">
            <p>Order ID: {order.id}</p>
            <p>Comment: {order.comment}</p>
            <p>Address: {order.address}</p>
            <p>Price: {order.price}</p>
            <p>Order Time: {order.orderTime}</p>
            <p>Delivery Time: {order.deliveryTime}</p>
            <p>Is Canceled: {order.isCanceled ? 'Yes' : 'No'}</p>
            <p>Order Status: {order.status}</p>
            <p>
              Ordered Items: {order.orderItems.map((item) => item.itemName).join(', ')}
            </p>
          </li>
        ))}
      </ul>
    </div>
  );
};


export default SellerNewOrdersForm;
