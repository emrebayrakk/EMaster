import { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux";
import { logout } from "../redux/authSlice";
import {
  BiCategory,
  BiSolidHome,
  BiMoney,
  BiMoneyWithdraw,
  BiMessageSquare,
} from "react-icons/bi";
import { FaSignOutAlt, FaBars } from "react-icons/fa";

const Sidebar = ({ onToggle , user }) => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [isCollapsed, setIsCollapsed] = useState(false);
  if(!user){
    const data = localStorage.getItem("user");
    user = JSON.parse(data);
  }
  const handleToggle = () => {
    setIsCollapsed(!isCollapsed);
    onToggle(!isCollapsed);
  };

  const handleLogout = () => {
    dispatch(logout());
    localStorage.removeItem("token");
    navigate("/login");
  };

  const menuItems = [
    { icon: BiSolidHome, text: "Dashboard", path: "/dashboard" },
    { icon: BiCategory, text: "Category", path: "/categories" },
    { icon: BiMoney, text: "Expenses", path: "/expenses" },
    { icon: BiMoneyWithdraw, text: "Incomes", path: "/incomes" },
    { icon: BiMessageSquare, text: "AI Chat", path: "/ai-chat" },
  ];

  return (
    <div className={`sidebar ${isCollapsed ? "w-20" : "w-64"} 
    transition-all duration-300 
    bg-gray-800
    min-h-screen
    bg-gradient-to-b from-indigo-900 via-purple-900 to-purple-800
    shadow-xl 
    text-white`}>
      <div className="sidebar-header flex items-center justify-between p-4 border-b border-gray-700">
        <h3 className={`brand ${isCollapsed ? "hidden" : "block"} text-xl font-semibold`}>{user.companyName}</h3>
        <button
          className="toggle-btn text-white hover:text-gray-300"
          onClick={handleToggle}
          title={isCollapsed ? "Expand" : "Collapse"}
        >
          <FaBars />
        </button>
      </div>

      <nav className="sidebar-nav mt-2">
        {menuItems.map((item, index) => (
          <Link
            key={index}
            to={item.path}
            className={`nav-link flex items-center p-3 rounded-lg transition-colors duration-200 
              ${isCollapsed ? "justify-center" : "justify-start"} 
              hover:bg-gray-700 text-gray-300 hover:text-white`}
            title={isCollapsed ? item.text : ""}
          >
            <item.icon className="nav-icon w-6 h-6" />
            <span className={`nav-text ${isCollapsed ? "hidden" : "ml-3"}`}>{item.text}</span>
          </Link>
        ))}
      </nav>

      <div className="sidebar-footer mt-auto p-4 border-t border-gray-700">
        <button
          onClick={handleLogout}
          className="logout-btn flex items-center text-gray-300 hover:bg-gray-700 rounded-lg p-2 w-full"
          title={isCollapsed ? "Logout" : ""}
        >
          <FaSignOutAlt className="nav-icon w-6 h-6" />
          <span className={`ml-3 ${isCollapsed ? "hidden" : ""}`}>Logout</span>
        </button>
      </div>
    </div>
  );
};

export default Sidebar;
