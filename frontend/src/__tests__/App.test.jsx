import { render, screen } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';  // ✅ import MemoryRouter
import App from '../App';

test('renders something on screen', () => {
  render(
    <MemoryRouter>   {/* ✅ Wrap App inside router */}
      <App />
    </MemoryRouter>
  );
});
