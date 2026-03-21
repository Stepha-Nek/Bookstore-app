import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';
import { Book } from '../types';

export default function Books() {
  const [books, setBooks] = useState<Book[]>([]);
  const [title, setTitle] = useState('');
  const [author, setAuthor] = useState('');
  const [year, setYear] = useState('');
  const navigate = useNavigate();
  const token = localStorage.getItem('token');
  const [recommendation, setRecommendation] = useState('');
  const [loadingRec, setLoadingRec] = useState(false);

  useEffect(() => {
    fetchBooks();
  }, []);

  const fetchBooks = async () => {
    const response = await api.get('/api/books');
    setBooks(response.data);
  };
  const handleRecommend = async (title: string) => {
  setLoadingRec(true);
  setRecommendation('');
  try {
    const response = await api.post('/api/books/recommend', { bookTitle: title });
    setRecommendation(response.data.recommendations);
  } catch {
    setRecommendation('Failed to get recommendations.');
  }
  setLoadingRec(false);
};

  const handleAdd = async () => {
    await api.post('/api/books', { title, author, yearPublished: parseInt(year) });
    setTitle(''); setAuthor(''); setYear('');
    fetchBooks();
  };

  const handleDelete = async (id: number) => {
    await api.delete(`/api/books/${id}`);
    fetchBooks();
  };

  const handleLogout = () => {
    localStorage.removeItem('token');
    navigate('/login');
  };

  return (
    <div className="container">
      <div style={{ display: 'flex', justifyContent: 'space-between' }}>
        <h2>📚 Bookstore</h2>
        {token && <button onClick={handleLogout}>Logout</button>}
      </div>

      {token && (
        <div className="add-form">
          <h3>Add a Book</h3>
          <input placeholder="Title" value={title} onChange={e => setTitle(e.target.value)} />
          <input placeholder="Author" value={author} onChange={e => setAuthor(e.target.value)} />
          <input placeholder="Year" value={year} onChange={e => setYear(e.target.value)} />
          <button onClick={handleAdd}>Add Book</button>
        </div>
      )}

      <div className="book-list">
        {books.map(book => (
          <div key={book.id} className="book-card">
            <h3>{book.title}</h3>
            <p>{book.author} — {book.yearPublished}</p>
	    <button onClick={() => handleRecommend(book.title)}>
  🤖 Recommend
</button>
            {token && <button onClick={() => handleDelete(book.id)}>Delete</button>}
          </div>
        ))}
      </div>
      {recommendation && (
      <div className="recommendation-box">
        <h3>📚 AI Recommendations</h3>
        <p>{loadingRec ? 'Loading...' : recommendation}</p>
      </div>
    )}

    </div>
  );
}
