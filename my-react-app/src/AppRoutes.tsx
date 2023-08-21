import React, { FC } from "react";
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import HomePage from './Pages/HomePage';
import LoginPage from './Pages/LoginPage';
import RegisterPage from './Pages/RegisterPage';
import Dashboard from "./Pages/Dashboard";
import ProfilePage from "./Pages/ProfilePage";
import VerifyUserPage from "./Pages/VerifyUserPage";

const AppRoutes: FC = () => {
  return (
    <Router>
        <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/homepage" element={<HomePage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/profile" element={<ProfilePage />} />
        <Route path="/verifyuser" element={<VerifyUserPage />} />
        </Routes>
    </Router>
    );
};

export default AppRoutes;