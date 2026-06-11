import { Link } from "react-router-dom";
import { Trophy, Github, Twitter, Mail } from "lucide-react";

const Footer = () => {
  return (
    <footer className="bg-gray-900 text-gray-300 mt-20">
      <div className="container mx-auto px-4 py-12">
        <div className="grid grid-cols-1 md:grid-cols-4 gap-8">

          {/* Brand */}
          <div className="space-y-4">
            <div className="flex items-center space-x-2">
              <div className="bg-purple-600 p-2 rounded-lg">
                <Trophy className="h-5 w-5 text-white" />
              </div>
              <div>
                <h3 className="font-semibold text-white">Student Support</h3>
                <p className="text-sm text-gray-400">Fontys Community</p>
              </div>
            </div>
            <p className="text-sm text-gray-400">
              Empowering Fontys students to share knowledge, earn recognition, and build a stronger academic community.
            </p>
          </div>

          {/* Quick Links */}
          <div className="space-y-4">
            <h4 className="font-semibold text-white">Community</h4>
            <div className="space-y-2 text-sm">
              <Link to="/posts" className="block hover:text-purple-500 transition">Browse Posts</Link>
              <Link to="/qa" className="block hover:text-purple-500 transition">Q&A Forum</Link>
              <Link to="/leaderboard" className="block hover:text-purple-500 transition">Leaderboard</Link>
            </div>
          </div>

          {/* Resources */}
          <div className="space-y-4">
            <h4 className="font-semibold text-white">Resources</h4>
            <div className="space-y-2 text-sm">
              <Link to="/help" className="block hover:text-purple-500 transition">Help Center</Link>
              <Link to="/contact" className="block hover:text-purple-500 transition">Contact Us</Link>
            </div>
          </div>

          {/* Connect */}
          <div className="space-y-4">
            <h4 className="font-semibold text-white">Stay Connected</h4>
            <div className="flex space-x-3">
              <a href="#" className="p-2 rounded-md bg-gray-700 hover:bg-purple-500 transition"><Twitter className="h-4 w-4 text-white" /></a>
              <a href="#" className="p-2 rounded-md bg-gray-700 hover:bg-purple-500 transition"><Github className="h-4 w-4 text-white" /></a>
              <a href="#" className="p-2 rounded-md bg-gray-700 hover:bg-purple-500 transition"><Mail className="h-4 w-4 text-white" /></a>
            </div>
            <p className="text-xs text-gray-400">
              Follow us for updates, tips, and community highlights.
            </p>
          </div>
        </div>

        {/* Bottom Section */}
        <div className="border-t border-gray-700 mt-8 pt-8 flex flex-col md:flex-row justify-between items-center text-sm">
          <p className="text-gray-400">&copy; 2024 Student Support. Built for Fontys University students.</p>
          <div className="flex space-x-6 mt-4 md:mt-0">
            <Link to="/privacy" className="hover:text-purple-500 transition">Privacy</Link>
            <Link to="/terms" className="hover:text-purple-500 transition">Terms</Link>
            <Link to="/accessibility" className="hover:text-purple-500 transition">Accessibility</Link>
          </div>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
