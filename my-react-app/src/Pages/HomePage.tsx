import React, { useEffect } from 'react';
import { Link,useNavigate } from 'react-router-dom';
import { isLoggedIn } from '../Services/UserService';

const HomePage: React.FC = () => {
  const navigate = useNavigate();

  useEffect(() => {
    if (isLoggedIn()) {
      navigate('/dashboard');
    }
  }, [navigate]);
  
  return (
    <div>
      <h1>Welcome to the Home Page</h1>
      <Link to="/login">
        <button>Login</button>
      </Link>
      <Link to="/register">
        <button>Register</button>
      </Link>
    </div>
  );
};

export default HomePage;