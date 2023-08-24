import React, { useEffect, useState } from 'react';
import { IOrder } from '../../Shared/Interfaces/orderInterfaces'; 
import { GetAllOrders } from '../../Services/OrderService';


const AllOrderForms: React.FC = () => {
  const [orders, setOrders] = useState<IOrder[]>([]);
  const [error, setError] = useState('');

  useEffect(() => {
    GetAllOrders()
      .then((response) => {
        const fetchedOrders: IOrder[] = response.data;
        setOrders(fetchedOrders);
      })
      .catch((error) => {
        console.error('Error fetching orders:', error);
        setError('Error fetching orders');
      });

      
  }, []);

  return (
    <div className="container">
      <h2>New Orders</h2>
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

export default AllOrderForms;