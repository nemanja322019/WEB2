import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import ProfileForm from '../Components/Profile/ProfileForm';
import { isLoggedIn } from '../Services/UserService';

const ProfilePage: React.FC = () => {
  const navigate = useNavigate();

  useEffect(() => {
    if (!isLoggedIn()) {
      navigate('/');
    }
  }, [navigate]);

  return (
    <div>
      <ProfileForm />
    </div>
  );
};

export default ProfilePage;