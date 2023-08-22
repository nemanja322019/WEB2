import React, { useState } from 'react';

interface AmountModalProps {
  onClose: () => void;
  onSave: (amount: number) => void;
}

const AmountModal: React.FC<AmountModalProps> = ({ onClose, onSave }) => {
  const [amount, setAmount] = useState(1);

  const handleSave = () => {
    onSave(amount);
    onClose();
  };

  return (
    <div className="modal">
      <div className="modal-content">
        <h2>Set Amount</h2>
        <input
          type="number"
          min={1}
          value={amount}
          onChange={(e) => setAmount(parseInt(e.target.value))}
        />
        <button onClick={handleSave}>Save</button>
        <button onClick={onClose}>Cancel</button>
      </div>
    </div>
  );
};

export default AmountModal;
