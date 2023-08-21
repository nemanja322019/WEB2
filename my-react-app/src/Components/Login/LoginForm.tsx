import React, { useState } from 'react';
import { IUserLogin } from "../../Shared/Interfaces/userInterfaces";
import { Login } from '../../Services/UserService'; 
import { useNavigate  } from 'react-router-dom';

const LoginForm = () => {
  const navigate = useNavigate();

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');

  const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    try {
      const userLoginData: IUserLogin = {
        email,
        password
      };

      const response = await Login(userLoginData);
      const token = response.data;
      localStorage.setItem('token', token);
      
      navigate('/dashboard');

    } catch (err:any) {
      setError(err.response?.data?.error || 'An error occurred'); 
    }
  };

  return (
    <div>
      <h2>Login</h2>
      {error && <p style={{ color: 'red' }}>{error}</p>}
      <form onSubmit={handleLogin}>
        <div>
          <label>Email:</label>
          <input
            type="text"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </div>
        <div>
          <label>Password:</label>
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>
        <button type="submit">Login</button>
      </form>
    </div>
  );
};

export default LoginForm;