import React, { useEffect, useState } from 'react';
import { GetSellers, VerifySeller } from '../../Services/UserService';
import { IUserProfile } from '../../Shared/Interfaces/userInterfaces';

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
    <div>
      <h2>Verify User</h2>
      <ul>
        {usersToVerify.map((user) => (
          <li key={user.id}>
            <p>Name: {user.name}</p>
            <p>Last Name: {user.lastName}</p>
            <p>Adress: {user.address}</p>
            <p>Email: {user.email}</p>
            <p>Status: {user.verificationStatus}</p>
            <button onClick={() => handleVerification(user.id, true)}>Accept</button>
            <button onClick={() => handleVerification(user.id, false)}>Reject</button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default VerifyUserForm;
