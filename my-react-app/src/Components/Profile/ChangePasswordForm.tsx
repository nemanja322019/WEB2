import React, { useState } from 'react';
import {ChangePassword} from '../../Services/UserService';
import { IUserPasswordChange, IUserProfile } from '../../Shared/Interfaces/userInterfaces';

interface ChangePasswordFormProps {
    userProfile: IUserProfile | null;
  }

  const ChangePasswordForm: React.FC<ChangePasswordFormProps> = ({ userProfile }) => {
  const [oldPassword, setOldPassword] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [error, setError] = useState<string | null>(null);

  const handlePasswordChange = async () => {
    try {
      const userPasswordChangeData: IUserPasswordChange = {
        oldpassword: oldPassword,
        newpassword: newPassword
      };

      setError('');
      const response = await ChangePassword(userProfile?.id || 0, userPasswordChangeData);
      // Do something with the response, e.g., show a success message
    } catch (err: any) {
      console.log('Error response:', err.response?.data);
      setError(err.response?.data?.error || 'An error occurred');
    }
  };

  return (
    <div className="change-password-form">
      <h2>Change Password</h2>
      {error && <p style={{ color: 'red' }}>{error}</p>}
      <label>
        Old Password:
        <input
          type="password"
          value={oldPassword}
          onChange={(e) => setOldPassword(e.target.value)}
        />
      </label>
      <label>
        New Password:
        <input
          type="password"
          value={newPassword}
          onChange={(e) => setNewPassword(e.target.value)}
        />
      </label>
      <button type="button" onClick={handlePasswordChange}>
        Change Password
      </button>
    </div>
  );
};

export default ChangePasswordForm;