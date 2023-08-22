import React, { FC } from "react";
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import HomePage from './Pages/HomePage';
import LoginPage from './Pages/LoginPage';
import RegisterPage from './Pages/RegisterPage';
import Dashboard from "./Pages/Dashboard";
import ProfilePage from "./Pages/ProfilePage";
import VerifyUserPage from "./Pages/VerifyUserPage";
import CreateItemPage from "./Pages/CreateItemPage";
import EditItemsPage from "./Pages/EditItemsPage";
import EditItemPage from "./Pages/EditItemPage";
import AvailableItemsPage from "./Pages/AvailableItemsPage";
import NewOrderPage from "./Pages/NewOrderPage";

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
        <Route path="/createitem" element={<CreateItemPage />} />
        <Route path="/edititems" element={<EditItemsPage />} />
        <Route path="/edititem" element={<EditItemPage />} />
        <Route path="/availableitems" element={<AvailableItemsPage />} />
        <Route path="/neworder" element={<NewOrderPage />} />
        </Routes>
    </Router>
    );
};

export default AppRoutes;