import { Outlet } from "react-router-dom";
import Header from "./Header";
import Footer from "./Footer";

const Layout = () => {
  return (
    <div className="flex flex-col min-h-screen">
      <Header />
      <main className="flex-1 p-8">
        <Outlet /> {/* <-- This renders the child routes */}
      </main>
      <Footer />
    </div>
  );
};

export default Layout;
