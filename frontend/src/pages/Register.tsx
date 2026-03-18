import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

export default function Register() {
  const [fullName, setFullName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleRegister = async () => {
    try {
      await api.post('/api/auth/register', { fullName, email, password });
      navigate('/login');
    } catch {
      setError('Registration failed. Try again.');
    }
  };

  return (
    <div className="container">
      <h2>Register</h2>
      {error && <p style={{ color: 'red' }}>{error}</p>}
      <input placeholder="Full Name" value={fullName} onChange={e => setFullName(e.target.value)} />
      <input placeholder="Email" value={email} onChange={e => setEmail(e.target.value)} />
      <input placeholder="Password" type="password" value={password} onChange={e => setPassword(e.target.value)} />
      <button onClick={handleRegister}>Register</button>
      <p>Already have an account? <a href="/login">Login</a></p>
    </div>
  );
}