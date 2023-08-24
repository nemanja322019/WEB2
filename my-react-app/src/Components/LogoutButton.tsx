import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { logout } from '../Services/UserService'; 


const LogoutButton = () => {
    const navigate = useNavigate();

    const handleLogout = () => {
      logout();
      navigate('/');
    };


    return (
        <div>
            <button onClick={handleLogout}>Logout</button>
        </div>
      );
};

export default LogoutButton;