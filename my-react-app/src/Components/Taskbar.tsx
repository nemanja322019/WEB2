import React from 'react';
import { useNavigate } from 'react-router-dom';
import './Taskbar.css';
import LogoutButton from './LogoutButton';

const Taskbar: React.FC = () => {
  const navigate = useNavigate();

  const handleHome = () => {
    navigate('/dashboard');
  };

  return (
    <div className="taskbar">
      <button onClick={handleHome}>Home</button>
      <LogoutButton/>
    </div>
  );
};

export default Taskbar;