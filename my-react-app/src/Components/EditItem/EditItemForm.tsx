import React, { useEffect, useState } from 'react';
import { IDisplayItem, IEditItem } from '../../Shared/Interfaces/itemInterfaces';
import { Navigate, useLocation, useNavigate } from 'react-router-dom';
import { EditItem } from '../../Services/ItemService';
import { IUserProfile } from '../../Shared/Interfaces/userInterfaces';
import './EditItemForm.css'; 

const EditItemForm: React.FC = () => {
    const location = useLocation();
    const { userProfile, selectedItem } = location.state as { userProfile: IUserProfile, selectedItem: IDisplayItem };
    const [editedItem, setEditedItem] = useState<IEditItem>({
      itemName: selectedItem.itemName,
      price: selectedItem.price,
      amount: selectedItem.amount,
      description: selectedItem.description,
      image: selectedItem.image
    });
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();

    const [selectedImage, setSelectedImage] = useState<File | null>(null); 
    const handleImageChange = (files: FileList | null) => {
      if (files && files.length > 0) {
        setSelectedImage(files[0]);
      } else {
        setSelectedImage(null);
      }
    };

    const handleItemEdit = async (event: React.FormEvent) => {
      event.preventDefault();
  
      try {
        let updatedImageBase64 = editedItem.image;
  
        if (selectedImage) {
          const reader = new FileReader();
          reader.onload = async (e) => {
            updatedImageBase64 = e.target?.result as string;
            const updatedItemData: IEditItem = {
              ...editedItem,
              image: updatedImageBase64,
            };
  
            await EditItem(selectedItem.id, updatedItemData);
            setError('');
            navigate('/edititems', { state: { userProfile } });
          };
          reader.readAsDataURL(selectedImage);
        } else {
          const updatedItemData: IEditItem = {
            ...editedItem,
            image: updatedImageBase64,
          };
  
          await EditItem(selectedItem.id, updatedItemData);
          setError('');
          // Handle response and navigation
        }
      } catch (err: any) {
        console.log('Error response:', err.response.data);
        setError(err.response?.data?.error || 'An error occurred');
      }
    };

    return (
      <div className="container">
        <h2>Edit Item Page</h2>
        {error && <p className="error-message">{error}</p>}
        <form className="form" onSubmit={handleItemEdit}>
          <div className="form-group">
            <label className="form-label">
              Item name:
              <input
                type="text"
                name="itemName"
                value={editedItem.itemName}
                onChange={(e) =>
                  setEditedItem({ ...editedItem, itemName: e.target.value })}
                className="form-input"
                disabled={false}
              />
            </label>
          </div>
          <div className="form-group">
            <label className="form-label">Price:</label>
            <input
              type="number"
              step="0.01"
              value={editedItem.price}
              onChange={(e) =>
                setEditedItem({ ...editedItem, price: parseFloat(e.target.value) })}
              className="form-input"
            />
          </div>
          <div className="form-group">
            <label className="form-label">Amount:</label>
            <input
              type="number"
              value={editedItem.amount}
              onChange={(e) =>
                setEditedItem({ ...editedItem, amount: parseInt(e.target.value) })}
              className="form-input"
            />
          </div>
          <div className="form-group">
            <label className="form-label">Description:</label>
            <textarea
              value={editedItem.description}
              onChange={(e) =>
                setEditedItem({ ...editedItem, description: e.target.value })}
              className="form-textarea"
            />
          </div>
          {selectedItem && selectedItem.image && (
            <img 
              src={selectedItem.image}
              alt="Item Image"
              className="image-preview"
            />
          )}
          <div>
          <label className="custom-file-upload">
            New Item Image:
            <input
              type="file"
              accept="image/*"
              onChange={(e) => handleImageChange(e.target.files)}
            />
          </label>
          
          <button type="submit" className="button-sss">Edit</button></div>
        </form>
      </div>
    );
  };
  
    
export default EditItemForm;