import React, { useEffect } from 'react';
import { Link,useNavigate } from 'react-router-dom';
import { isLoggedIn } from '../Services/UserService';
import './HomePage.css'; 

const HomePage: React.FC = () => {
  const navigate = useNavigate();

  useEffect(() => {
    if (isLoggedIn()) {
      navigate('/dashboard');
    }
  }, [navigate]);
  
  return (
    <div className="home-page">
      <h1>Welcome to the Home Page</h1>
      <Link to="/login" className="link-button">
        <button>Login</button>
      </Link>
      <Link to="/register" className="link-button">
        <button>Register</button>
      </Link>
    </div>
  );
};

export default HomePage;