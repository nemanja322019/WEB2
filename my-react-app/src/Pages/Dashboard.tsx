import React, { useEffect, useState } from 'react';
import { isLoggedIn, getUserType, fetchUserProfile } from '../Services/UserService';
import { Link, useNavigate } from 'react-router-dom';
import { IUserProfile } from '../Shared/Interfaces/userInterfaces';
import './Dashboard.css'; 

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
    <div className="dashboard">
      {isLoggedIn() ? (
        <div>
          <h1>Welcome to the Dashboard</h1>
          {userProfile?.verificationStatus !== 'ACCEPTED' ? (
            <p className="verification-warning">Your account is not verified. Please wait for verification.</p>
          ) : (
            <div className="links-container">
              <Link to="/profile" className="dashboard-link">Profile</Link>
              {userType === 'seller' && (
                <>
                  <Link to="/createitem" className="dashboard-link" state={createItemLinkState}>Create Item</Link>
                  <Link to="/edititems" className="dashboard-link" state={createItemLinkState}>Edit Items</Link>
                  <Link to="/sellerneworders" className="dashboard-link" state={createItemLinkState}>New Orders</Link>
                  <Link to="/selleroldorders" className="dashboard-link" state={createItemLinkState}>Old Orders</Link>
                </>
              )}
              {userType === 'customer' && (
                <>
                  <Link to="/availableitems" className="dashboard-link" state={createItemLinkState}>Available items</Link>
                  <Link to="/ongoingorders" className="dashboard-link" state={createItemLinkState}>Ongoing Orders</Link>
                  <Link to="/deliveredorders" className="dashboard-link" state={createItemLinkState}>Delivered Orders</Link>
                </>
              )}
              {userType === 'admin' && (
                <>
                  <Link to="/verifyuser" className="dashboard-link">Verify User</Link>
                  <Link to="/adminorders" className="dashboard-link">All Orders</Link>
                </>
              )}
            </div>
          )}
        </div>
      ) : (
        <h1>Please login.</h1>
      )}
    </div>
  );
};
  
export default Dashboard;