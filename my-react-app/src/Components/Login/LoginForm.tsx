import React, { useState } from 'react';
import { IGoogleToken, IUserLogin } from "../../Shared/Interfaces/userInterfaces";
import { Login, LoginGoogle } from '../../Services/UserService'; 
import { useNavigate  } from 'react-router-dom';
import { GoogleLogin } from '@react-oauth/google';
import './LoginForm.css'; 

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

  const responseMessage = async (response: any) => {
    try{
    const googleToken: IGoogleToken = {
      token: response.credential
    } 
    const responseData = await LoginGoogle(googleToken)
    const token = responseData.data;
    localStorage.setItem('token', token);
    
    navigate('/dashboard');
  }
  catch(err:any){
    setError(err.response?.data?.error || 'An error occurred'); 
  }
  };

  return (
    <div className="login-form">
      <h2>Login</h2>
      {error && <p>{error}</p>}
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
      <div className="google-login-button">
        <GoogleLogin
          onSuccess={responseMessage}
          onError={() => {
            console.log('Login Failed');
            setError('Google login failed!')
          }}
        />
      </div>
    </div>
  );
};


export default LoginForm;