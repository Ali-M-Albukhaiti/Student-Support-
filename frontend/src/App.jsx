import { BrowserRouter, Routes, Route, Navigate, Link } from "react-router-dom";
import Layout from "./components/layout/Layout";
import Home from "./pages/Home";
import PostsPage from "./pages/PostsPage";
import QAPage from "./pages/QAPage";

function App() {
  return (
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<Home />} />
          <Route path="posts" element={<PostsPage />} />
          <Route path="qa" element={<QAPage />} />
          <Route path="*" element={<Navigate to="/" replace />} />
        </Route>
      </Routes>   
  );
}

export default App;

