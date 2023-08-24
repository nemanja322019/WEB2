import React, { useEffect, useState } from 'react';
import { GetSellers, VerifySeller } from '../../Services/UserService';
import { IUserProfile } from '../../Shared/Interfaces/userInterfaces';
import './VerifyUserForm.css'; 
const VerifyUserForm: React.FC = () => {
  const [usersToVerify, setUsersToVerify] = useState<IUserProfile[]>([]);

  useEffect(() => {

    GetSellers()
      .then((response) => {
        const users = response.data.map((userProfile: IUserProfile) => {
          return {
            id: userProfile.id,
            username: userProfile.username,
            email: userProfile.email,
            name: userProfile.name,
            lastName: userProfile.lastName,
            birthDate: new Date(userProfile.birthDate),
            address: userProfile.address,
            isVerified: userProfile.isVerified,
            verificationStatus: userProfile.verificationStatus,
            userType: userProfile.userType,
            image: userProfile.image
          };
        });
        setUsersToVerify(users);
      })
      .catch((error) => {
        console.error('Error fetching users to verify:', error);
      });
  }, []); 

  const handleVerification = async (userId: number, isAccepted: boolean) => {
    try {
      await VerifySeller(userId, isAccepted);
      // Update the usersToVerify state to reflect the change in verification status
      setUsersToVerify(prevUsers => prevUsers.map(user => {
        if (user.id === userId) {
          return { ...user, verificationStatus: isAccepted ? 'ACCEPTED' : 'REJECTED' };
        }
        return user;
      }));
    } catch (error) {
      console.error('Error verifying user:', error);
    }
  };

  return (
    <div className="container">
      <h2>Verify User</h2>
      <ul className="user-list">
        {usersToVerify.map((user) => (
          <li key={user.id} className="user-item">
            <div className="user-details">
              <p>Name: {user.name}</p>
              <p>Last Name: {user.lastName}</p>
              <p>Address: {user.address}</p>
              <p>Email: {user.email}</p>
              <p>Status: {user.verificationStatus}</p>
            </div>
            {user && user.image && (
              <img
                src={user.image}
                alt="No Image"
                className="user-image"
              />
            )}
            <div className="action-buttons">
              <button
                className="action-button"
                onClick={() => handleVerification(user.id, true)}
              >
                Accept
              </button>
              <button
                className="action-button reject"
                onClick={() => handleVerification(user.id, false)}
              >
                Reject
              </button>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default VerifyUserForm;
