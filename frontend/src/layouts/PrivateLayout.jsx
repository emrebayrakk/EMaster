import Sidebar from "../components/Sidebar";
import Footer from "../components/Footer";
import { useState } from "react";
import { Outlet } from "react-router-dom";
import { selectCurrentUser } from "../redux/authSlice";
import { useSelector } from "react-redux";

const PrivateLayout = () => {
  const [isSidebarCollapsed, setIsSidebarCollapsed] = useState(false);

  const handleSidebarToggle = (collapsed) => {
    setIsSidebarCollapsed(collapsed);
  };
  const user = useSelector(selectCurrentUser);
  return (
    <div className="dashboard-container">
      <Sidebar onToggle={handleSidebarToggle} user={user} />
      <div className={`main-content ${isSidebarCollapsed ? "shifted" : ""}`}>
        <Outlet />
      </div>
    </div>
  );
};

export default PrivateLayout;
