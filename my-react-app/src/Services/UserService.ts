import { axiosClient } from "./axiosClient";
import { API } from "../Constants/Constants";
import jwt_decode from 'jwt-decode';
import {
    IUserLogin,
    IUserPasswordChange,
    IUserProfile,
    IUserRegister,
    IUserUpdate
} from "../Shared/Interfaces/userInterfaces";

export const GetUserById = (id: number) => {
  return axiosClient.get(`${API}/users/${id}`);
};

export const Login = (userLogin: IUserLogin) => {
    return axiosClient.post(`${API}/users/login`, userLogin);
  };

  export const Register = (userRegister: IUserRegister) => {
    return axiosClient.post(`${API}/users/registration`, userRegister);
  };

  export const UpdateProfile = (id: number, userUpdate: IUserUpdate) => {
    return axiosClient.put(`${API}/users/${id}`, userUpdate);
  };

  export const ChangePassword = (id: number, userPasswordChange: IUserPasswordChange) => {
    return axiosClient.put(`${API}/users/${id}/change-password`, userPasswordChange);
  };

  export const GetSellers = () => {
    return axiosClient.get(`${API}/users/sellers`);
  };

  export const VerifySeller = (id: number,isAccepted: boolean) => {
    return axiosClient.put(`${API}/users/${id}/verify/${isAccepted}`);
  };

export const isLoggedIn = () => {
    const token = localStorage.getItem('token');
    return !!token; // Returns true if token is present, false otherwise
  };

  export const logout = () => {
    localStorage.removeItem("token")
  };

  export const fetchUserProfile = async (): Promise<IUserProfile | null> => {
    const token = localStorage.getItem('token');
    if (!token) {
      return null;
    }
    try {
      const decoded = jwt_decode(token) as { id: number };
      const userId = decoded.id;
  
      if (!userId) {
        return null;
      }
  
      const response = await GetUserById(userId);
      const userData: IUserProfile = response.data;
      return userData;
    } catch (error) {
      console.error('Error fetching user profile:', error);
      return null;
    }
  };

  
  interface DecodedToken {
    [key: string]: string;
  }
  export const getUserType = () => {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken: DecodedToken = jwt_decode(token);
      return decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    }
    return null;
  };