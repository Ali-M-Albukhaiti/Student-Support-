import { Link } from "react-router-dom";
import { Trophy, Search } from "lucide-react";

const Header = () => {
  return (
    <header className="bg-gray-900 text-gray-300 border-b border-gray-700 sticky top-0 z-50">
      <div className="container mx-auto flex flex-col md:flex-row items-center justify-between px-4 py-4 space-y-3 md:space-y-0">

        {/* Logo / Brand */}
        <div className="flex items-center space-x-2">
          <div className="bg-purple-600 p-2 rounded-lg">
            <Trophy className="h-5 w-5 text-white" />
          </div>
          <h1 className="text-white font-semibold text-lg">Student Support</h1>
        </div>

        {/* Navigation Links */}
        <nav className="flex space-x-6 text-sm font-medium">
          <Link to="/" className="hover:text-purple-500 transition">Home</Link>
          <Link to="/posts" className="hover:text-purple-500 transition">Posts</Link>
          <Link to="/qa" className="hover:text-purple-500 transition">Q&A</Link>
          <Link to="/leaderboard" className="hover:text-purple-500 transition">Leaderboard</Link>
          <Link to="/profile" className="hover:text-purple-500 transition">Profile</Link>
        </nav>

        {/* Right Side: Search + Get Started */}
        <div className="flex items-center space-x-4">
          {/* Search Bar */}
          <div className="relative">
            <Search className="absolute left-2 top-2.5 h-4 w-4 text-gray-400" />
            <input
              type="text"
              placeholder="Search..."
              className="bg-gray-800 text-gray-200 pl-8 pr-3 py-1.5 rounded-md text-sm focus:outline-none focus:ring-2 focus:ring-purple-500"
            />
          </div>

          {/* Get Started Button */}
          <Link
            to="/get-started"
            className="bg-purple-600 text-white px-4 py-2 rounded-lg text-sm font-semibold hover:bg-purple-700 transition"
          >
            Get Started
          </Link>
        </div>
      </div>
    </header>
  );
};

export default Header;
