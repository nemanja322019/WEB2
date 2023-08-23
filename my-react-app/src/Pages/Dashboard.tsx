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

  const createItemLinkState = userProfile ? { userProfile } : undefined;

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
              <Link to="/profile">Profile</Link><br/>
              {userType === 'seller' && (
                <>
                  <Link to="/createitem" state={createItemLinkState}>Create Item</Link><br/>
                  <Link to="/edititems" state={createItemLinkState}>Edit Items</Link><br/>
                  <Link to="/sellerneworders" state={createItemLinkState}>New Orders</Link><br/>
                  <Link to="/selleroldorders" state={createItemLinkState}>Old Orders</Link><br/>
                </>
              )}
              {userType === 'customer' && (
                <>
                  <Link to="/availableitems" state={createItemLinkState}>Available items</Link><br/>
                  <Link to="/ongoingorders" state={createItemLinkState}>Ongoing Orders</Link><br/>
                  <Link to="/deliveredorders" state={createItemLinkState}>Delivered Orders</Link><br/>
                </>
              )}
              {userType === 'admin' && (
                <>
                  <Link to="/verifyuser">Verify User</Link><br/>
                  <Link to="/adminorders">All Orders</Link><br/>
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