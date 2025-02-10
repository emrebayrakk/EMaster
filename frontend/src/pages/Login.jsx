import { useState } from "react";
import { useDispatch } from "react-redux";
import { useNavigate, Link } from "react-router-dom";
import { authAPI } from "../services/api";
import { setCredentials } from "../redux/authSlice";
import { toast } from "react-toastify";

const Login = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsLoading(true);
    try {
      const response = await authAPI.login({ email, password });
      console.log("Login API Response:", response);
      const { token, user } = response.data;
      dispatch(setCredentials({ token, user }));
      navigate("/dashboard");
      toast.success("Login successful!");
    } catch (error) {
      console.error("Login Error:", error);
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
      <div className="position-absolute w-100 h-100" style={{ zIndex: 1 }}>
        <div className="animated-bg-circle" style={{
          position: "absolute",
          width: "400px",
          height: "400px",
          background: "linear-gradient(135deg, #6B73FF 0%, #000DFF 100%)",
          borderRadius: "50%",
          filter: "blur(80px)",
          opacity: "0.4",
          top: "-10%",
          left: "-10%",
          animation: "float 8s ease-in-out infinite"
        }}></div>
        <div className="animated-bg-circle" style={{
          position: "absolute",
          width: "300px",
          height: "300px",
          background: "linear-gradient(135deg, #FF6B6B 0%, #FF000D 100%)",
          borderRadius: "50%",
          filter: "blur(80px)",
          opacity: "0.3",
          bottom: "-5%",
          right: "-5%",
          animation: "float 6s ease-in-out infinite reverse"
        }}></div>
      </div>

      <div className="card border-0 rounded-4 position-relative" 
           style={{ 
             maxWidth: "400px", 
             width: "90%",
             backgroundColor: "rgba(255, 255, 255, 0.9)",
             backdropFilter: "blur(20px)",
             boxShadow: "0 8px 32px 0 rgba(31, 38, 135, 0.37)",
             zIndex: 2
           }}>
        <div className="card-body p-5">
          <h2 className="text-center mb-4 fw-bold">Welcome Back!</h2>
          <p className="text-center text-muted mb-4">Please enter your credentials to login</p>
          
          <form onSubmit={handleSubmit}>
            <div className="mb-4">
              <label className="form-label small text-muted">Email Address</label>
              <div className="input-group">
                <span className="input-group-text bg-light border-end-0">
                  <i className="bi bi-envelope"></i>
                </span>
                <input
                  type="email"
                  className="form-control border-start-0 ps-0"
                  placeholder="name@example.com"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
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
                  placeholder="Enter your password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
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
                  Signing in...
                </>
              ) : (
                'Sign In'
              )}
            </button>
            
            <p className="text-center mb-0">
              Don't have an account? {" "}
              <Link to="/register" className="text-primary text-decoration-none fw-bold">
                Create Account
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

export default Login;
