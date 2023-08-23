import React, { useEffect, useState } from 'react';
import { IOrder } from '../../Shared/Interfaces/orderInterfaces'; 
import { CancelOrder, GetOngoingOrdersFromCustomer } from '../../Services/OrderService';
import { useLocation } from 'react-router-dom';
import { IUserProfile } from '../../Shared/Interfaces/userInterfaces';

const CustomerOngoingOrdersForm: React.FC = () => {
    const location = useLocation();
    const userProfile = (location.state as { userProfile: IUserProfile }).userProfile;
  const [orders, setOrders] = useState<IOrder[]>([]);
  const [error, setError] = useState('');
  const [refetchTrigger, setRefetchTrigger] = useState(false);

  useEffect(() => {
    GetOngoingOrdersFromCustomer(userProfile.id)
      .then((response) => {
        const fetchedOrders: IOrder[] = response.data;
        setOrders(fetchedOrders);
        console.log('Fetched customer orders:', fetchedOrders);
      })
      .catch((error) => {
        console.error('Error fetching customer orders:', error);
        setError('Error fetching customer orders');
      });
  }, [userProfile.id,refetchTrigger]);

  const handleCancel = async (selectedOrder: IOrder) => {
    try{
     await CancelOrder(selectedOrder.id);
     setRefetchTrigger((prevTrigger) => !prevTrigger);
    }catch (err:any) {
      console.log('Error response:', err.response); 
      setError(err.response?.data?.error || 'An error occurred'); 
    }
  };

  return (
    <div>
      <h2>Ongoing orders:</h2>
      {error && <p style={{ color: 'red' }}>{error}</p>}
      <ul>
        {orders.map((order) => (
          <li key={order.id}>
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
            <button onClick={() => handleCancel(order)}>Cancel</button>
          </li>
        ))}
      </ul>
    </div>
  );
};


export default CustomerOngoingOrdersForm;
