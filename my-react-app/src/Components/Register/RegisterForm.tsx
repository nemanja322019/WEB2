import React, { useState } from 'react';
import { IUserRegister } from "../../Shared/Interfaces/userInterfaces";
import { Register } from '../../Services/UserService'; 
import { Link, useNavigate } from 'react-router-dom';
import { UserTypes } from '../../Shared/Enums/enumerations';

const RegisterForm = () => {
    const navigate = useNavigate();

    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [name, setName] = useState('');
    const [lastName, setLastName] = useState('');
    const [birthDate, setBirthDate] = useState(new Date());
    const [address, setAddress] = useState('');
    const [userType, setUserType] = useState<string>('customer'); 
    const [error, setError] = useState('');
  
    const handleRegister = async (e: React.FormEvent<HTMLFormElement>) => {
      e.preventDefault();
      try {
        const userRegisterData: IUserRegister = {
          username,
          email,
          password,
          name,
          lastName,
          birthDate,
          address,
          userType: userType.toString().trim() as UserTypes
        };
  
        const response = await Register(userRegisterData); // Call the Register function from your UserService
        navigate('/');

      } catch (err: any) {
        console.log('Error response:', err.response.data);
        setError(err.response?.data?.error || 'An error occurred'); // Handle registration error here
      }
    };
  
    return (
      <div>
        <h2>Register</h2>
        {error && <p style={{ color: 'red' }}>{error}</p>}
        <form onSubmit={handleRegister}>
          <div>
            <label>Username:</label>
            <input
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
          </div>
          <div>
            <label>Email:</label>
            <input
              type="email"
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
          <div>
            <label>Name:</label>
            <input
              type="text"
              value={name}
              onChange={(e) => setName(e.target.value)}
            />
          </div>
          <div>
            <label>Last Name:</label>
            <input
              type="text"
              value={lastName}
              onChange={(e) => setLastName(e.target.value)}
            />
          </div>
          <div>
          <label>Birth Date:</label>
          <input
            type="date"
            value={birthDate ? new Date(birthDate).toISOString().split('T')[0] : ''}
            onChange={(e) => setBirthDate(new Date(e.target.value))}
          />
          </div>
          <div>
            <label>Address:</label>
            <input
              type="text"
              value={address}
              onChange={(e) => setAddress(e.target.value)}
            />
          </div>
          <div>
          <label>User Type:</label>
          <select
            value={userType}
            onChange={(e) => setUserType(e.target.value)}
          >
            <option value="customer">Customer</option>
            <option value="seller">Seller</option>
          </select>
        </div>
          <button type="submit">Register</button>
        </form>
      </div>
    );
  };
  
  export default RegisterForm;