import React, { useState } from 'react';
import { IUserRegister } from "../../Shared/Interfaces/userInterfaces";
import { Register } from '../../Services/UserService'; 
import { Link, useNavigate } from 'react-router-dom';
import { UserTypes } from '../../Shared/Enums/enumerations';
import './RegisterForm.css'; 

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

    const [selectedImage, setSelectedImage] = useState<File | null>(null); 

    const handleImageChange = (files: FileList | null) => {
      if (files && files.length > 0) {
        setSelectedImage(files[0]);
      } else {
        setSelectedImage(null);
      }
    };
  
    const handleRegister = async (e: React.FormEvent<HTMLFormElement>) => {
      e.preventDefault();
      try {
        let imageBase64 = '';
        if (selectedImage) {
          const reader = new FileReader();
          reader.onload = async (e) => {
            imageBase64 = e.target?.result as string;
            const userRegisterData: IUserRegister = {
              username,
              email,
              password,
              name,
              lastName,
              birthDate,
              address,
              userType: userType.toString().trim() as UserTypes,
              image: imageBase64, // Send base64 encoded image
            };
            
            const response = await Register(userRegisterData); // Call the Register function from your UserService
            navigate('/');
          };
          reader.readAsDataURL(selectedImage);
        } else {
          const userRegisterData: IUserRegister = {
            username,
            email,
            password,
            name,
            lastName,
            birthDate,
            address,
            userType: userType.toString().trim() as UserTypes,
            image: "", // Send null for the image field
          };
    
          const response = await Register(userRegisterData); // Call the Register function from your UserService
          navigate('/');
        }
      } catch (err: any) {
        console.log('Error response:', err.response.data);
        setError(err.response?.data?.error || 'An error occurred'); // Handle registration error here
      }
    };
  
    return (
      <div className="profile-form">
        <div>
        <h2 className='profile-form'>Register</h2>
        {error && <p className="error-message">{error}</p>}
        <form onSubmit={handleRegister}>
          <div className="form-columns">
            <div className="form-column">
              <label>Username:</label>
              <input
                type="text"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
              />
              <label>Email:</label>
              <input
                type="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
              <label>Password:</label>
              <input
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
              <label>Name:</label>
              <input
                type="text"
                value={name}
                onChange={(e) => setName(e.target.value)}
              />
              <label>Last Name:</label>
              <input
                type="text"
                value={lastName}
                onChange={(e) => setLastName(e.target.value)}
              />
              <label>Birth Date:</label>
              <input
                type="date"
                value={birthDate ? new Date(birthDate).toISOString().split('T')[0] : ''}
                onChange={(e) => setBirthDate(new Date(e.target.value))}
              />
              <label>Address:</label>
              <input
                type="text"
                value={address}
                onChange={(e) => setAddress(e.target.value)}
              />
              <label>User Type:</label>
              <select
                value={userType}
                onChange={(e) => setUserType(e.target.value)}
              >
                <option value="customer">Customer</option>
                <option value="seller">Seller</option>
              </select>
            </div>
            <div className="form-column">
              <div className="profile-image">
                {selectedImage && (
                  <img src={URL.createObjectURL(selectedImage)} alt="Profile" className="profile-image"/>
                )}
                <label>
                  <span className="custom-file-upload">Upload Image</span>
                  <input
                    type="file"
                    accept="image/*"
                    onChange={(e) => handleImageChange(e.target.files)}
                  />
                </label>
              </div>
            </div>
          </div>
          <button type="submit" className='profile-form button'>Register</button>
        </form>
        </div>
      </div>
    );
  };
  
  export default RegisterForm;