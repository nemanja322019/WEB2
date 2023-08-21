import React, { useEffect, useState } from 'react';
import { isLoggedIn, getUserType, fetchUserProfile } from '../Services/UserService';
import { Link, useNavigate } from 'react-router-dom';
import LogoutButton from '../Components/Login/LogoutButton';
import { IUserProfile } from '../Shared/Interfaces/userInterfaces';

const Dashboard: React.FC = () => {
  const navigate = useNavigate();
  const userType = getUserType();
  const [userProfile, setUserProfile] = useState<IUserProfile | null>(null);

  useEffect(() => {
    fetchUserProfile()
      .then((userData) => {
        setUserProfile(userData);
      });
  }, []);

  return (
    <div>
      {isLoggedIn() ? (
        <div>
          <h1>Welcome to the Dashboard</h1>
          <LogoutButton/>
          {userProfile?.verificationStatus !== 'ACCEPTED' ? (
            <p>Your account is not verified. Please wait for verification.</p>
          ) : (
            <>
              <Link to="/profile">Profile</Link>
              {userType === 'seller' && (
                <>
                  <Link to="/">Create Item</Link>
                  <Link to="/">New Orders</Link>
                  <Link to="/">Old Orders</Link>
                </>
              )}
              {userType === 'customer' && (
                <>
                  <Link to="/">New Order</Link>
                  <Link to="/">My Orders</Link>
                </>
              )}
              {userType === 'admin' && (
                <>
                  <Link to="/verifyuser">Verify User</Link>
                  <Link to="/">All Orders</Link>
                </>
              )}
            </>
          )}
        </div>
      ) : (
        <h1>Please login.</h1>
      )}
    </div>
  );
};
  
export default Dashboard;