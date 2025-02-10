import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import { authAPI } from "../services/api";
import { toast } from "react-toastify";

const Register = () => {
  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    username: "",
    email: "",
    passwordhash: "",
    companyName: "",
  });
  const [isLoading, setIsLoading] = useState(false);
  const navigate = useNavigate();

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsLoading(true);
    try {
      await authAPI.register(formData);
      toast.success("Registration successful! Please login.");
      navigate("/login");
    } catch (error) {
      toast.error(error.message);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="min-vh-100 d-flex align-items-center justify-content-center position-relative overflow-hidden" 
         style={{
           background: "linear-gradient(45deg, #1a1a1a, #2d2d2d)",
         }}>
      {/* Animasyonlu arka plan elementleri */}
      <div className="position-absolute w-100 h-100" style={{ zIndex: 1 }}>
        <div className="animated-bg-circle" style={{
          position: "absolute",
          width: "500px",
          height: "500px",
          background: "linear-gradient(135deg, #6B73FF 0%, #000DFF 100%)",
          borderRadius: "50%",
          filter: "blur(80px)",
          opacity: "0.4",
          top: "-15%",
          right: "-15%",
          animation: "float 10s ease-in-out infinite"
        }}></div>
        <div className="animated-bg-circle" style={{
          position: "absolute",
          width: "400px",
          height: "400px",
          background: "linear-gradient(135deg, #FF6B6B 0%, #FF000D 100%)",
          borderRadius: "50%",
          filter: "blur(80px)",
          opacity: "0.3",
          bottom: "-10%",
          left: "-10%",
          animation: "float 8s ease-in-out infinite reverse"
        }}></div>
      </div>

      <div className="card border-0 rounded-4 position-relative" 
           style={{ 
             maxWidth: "450px", 
             width: "90%",
             backgroundColor: "rgba(255, 255, 255, 0.9)",
             backdropFilter: "blur(20px)",
             boxShadow: "0 8px 32px 0 rgba(31, 38, 135, 0.37)",
             zIndex: 2
           }}>
        <div className="card-body p-5">
          <h2 className="text-center mb-4 fw-bold">Create Account</h2>
          <p className="text-center text-muted mb-4">Fill in your details to get started</p>

          <form onSubmit={handleSubmit}>
            <div className="row mb-3">
              <div className="col-md-6 mb-3 mb-md-0">
                <label className="form-label small text-muted">First Name</label>
                <input
                  type="text"
                  className="form-control"
                  name="firstName"
                  placeholder="John"
                  value={formData.firstName}
                  onChange={handleChange}
                  required
                />
              </div>
              <div className="col-md-6">
                <label className="form-label small text-muted">Last Name</label>
                <input
                  type="text"
                  className="form-control"
                  name="lastName"
                  placeholder="Doe"
                  value={formData.lastName}
                  onChange={handleChange}
                  required
                />
              </div>
            </div>
            <div className="mb-3">
              <label className="form-label small text-muted">Company Name</label>
              <div className="input-group">
                <span className="input-group-text bg-light border-end-0">
                  <i className="bi bi-building"></i>
                </span>
                <input
                  type="text"
                  className="form-control border-start-0 ps-0"
                  name="companyName"
                  placeholder="Choose a company name"
                  value={formData.companyName}
                  onChange={handleChange}
                  required
                />
              </div>
            </div>
            <div className="mb-3">
              <label className="form-label small text-muted">Username</label>
              <div className="input-group">
                <span className="input-group-text bg-light border-end-0">
                  <i className="bi bi-person"></i>
                </span>
                <input
                  type="text"
                  className="form-control border-start-0 ps-0"
                  name="username"
                  placeholder="Choose a username"
                  value={formData.username}
                  onChange={handleChange}
                  required
                />
              </div>
            </div>

            <div className="mb-3">
              <label className="form-label small text-muted">Email Address</label>
              <div className="input-group">
                <span className="input-group-text bg-light border-end-0">
                  <i className="bi bi-envelope"></i>
                </span>
                <input
                  type="email"
                  className="form-control border-start-0 ps-0"
                  name="email"
                  placeholder="name@example.com"
                  value={formData.email}
                  onChange={handleChange}
                  required
                />
              </div>
            </div>

            <div className="mb-4">
              <label className="form-label small text-muted">Password</label>
              <div className="input-group">
                <span className="input-group-text bg-light border-end-0">
                  <i className="bi bi-lock"></i>
                </span>
                <input
                  type="password"
                  className="form-control border-start-0 ps-0"
                  name="passwordhash"
                  placeholder="Create a strong password"
                  value={formData.passwordhash}
                  onChange={handleChange}
                  required
                />
              </div>
            </div>

            <button type="submit" 
                    className="btn btn-primary w-100 mb-4 py-2 rounded-3"
                    disabled={isLoading}
                    style={{
                      background: "linear-gradient(135deg, #6B73FF 0%, #000DFF 100%)",
                      border: "none"
                    }}>
              {isLoading ? (
                <>
                  <span className="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>
                  Creating Account...
                </>
              ) : (
                'Create Account'
              )}
            </button>

            <p className="text-center mb-0">
              Already have an account? {" "}
              <Link to="/login" className="text-primary text-decoration-none fw-bold">
                Sign In
              </Link>
            </p>
          </form>
        </div>
      </div>

      <style>
        {`
          @keyframes float {
            0% { transform: translate(0, 0) rotate(0deg); }
            50% { transform: translate(20px, 20px) rotate(5deg); }
            100% { transform: translate(0, 0) rotate(0deg); }
          }
        `}
      </style>
    </div>
  );
};

export default Register;
