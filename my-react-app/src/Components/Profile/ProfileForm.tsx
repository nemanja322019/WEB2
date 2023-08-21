import React, { useEffect, useState } from 'react';
import jwt_decode from 'jwt-decode';
import LogoutButton from '../../Components/Login/LogoutButton';
import { IUserProfile, IUserUpdate } from '../../Shared/Interfaces/userInterfaces';
import { GetUserById, UpdateProfile } from '../../Services/UserService';
import { useNavigate } from 'react-router-dom';
import ChangePasswordForm from './ChangePasswordForm'; 

const ProfileForm: React.FC = () => {
  const [userProfile, setUserProfile] = useState<IUserProfile | null>(null);
  const [error, setError] = useState<string | null>(null);

  const navigate = useNavigate();
  
  const token = localStorage.getItem('token');

  useEffect(() => {
    if (token) {
      const decoded = jwt_decode(token) as { id: number };

      const userId = decoded.id;
      if (userId) {
        GetUserById(userId)
          .then((response) => {
            const userData: IUserProfile = response.data;
            setUserProfile(userData);
          })
          .catch((error) => console.error('Error fetching user profile:', error));
      }
    }
  }, [token]);


  const handleProfileUpdate = async  (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    try {
        const userUpdateData: IUserUpdate = {
            name: userProfile?.name || '',
            lastName: userProfile?.lastName || '',
            birthDate: userProfile?.birthDate || new Date(),
            address: userProfile?.address || ''
        };
        setError('');
        const response = await  UpdateProfile(userProfile?.id || 0, userUpdateData);
        //navigate('/');

      } catch (err: any) {
        console.log('Error response:', err.response.data);
        setError(err.response?.data?.error || 'An error occurred');
      }
  };



    return (
        <div>
        <LogoutButton />
        {userProfile ? (
          <div>
            <h2>Profile</h2>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            <form onSubmit={handleProfileUpdate}>
            <p>User ID: {userProfile.id}</p>
            <label>
              Name:
              <input
                type="text"
                value={userProfile.name}
                onChange={(e) =>
                  setUserProfile({ ...userProfile, name: e.target.value })
                }
                disabled={false}
              />
            </label>
            <label>
            Last Name:
            <input
              type="text"
              value={userProfile.lastName}
              onChange={(e) =>
                setUserProfile({ ...userProfile, lastName: e.target.value })
              }
              disabled={false}
            />
          </label>
          <label>
            Birth Date:
            <input
              type="date"
              value={userProfile.birthDate ? new Date(userProfile.birthDate).toISOString().split('T')[0] : ''}
              onChange={(e) =>
                setUserProfile({ ...userProfile, birthDate: new Date(e.target.value) })
              }
              disabled={false}
            />
          </label>
          <label>
            Address:
            <input
              type="text"
              value={userProfile.address}
              onChange={(e) =>
                setUserProfile({ ...userProfile, address: e.target.value })
              }
              disabled={false}
            />
          </label>
          <label>
              Username:
              <input
                type="text"
                value={userProfile.username}
                disabled={true}
              />
            </label>
            <label>
              Email:
              <input
                type="text"
                value={userProfile.email}
                disabled={true}
              />
            </label>
            <p>Verification status: {userProfile.verificationStatus}</p>
            <button type="submit">Edit</button>
            </form>
            <ChangePasswordForm userProfile={userProfile} />
          </div>
        ) : (
          <p>Loading...</p>
        )}
      </div>
      );
};

export default ProfileForm;