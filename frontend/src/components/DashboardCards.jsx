import { FaMoneyCheckAlt, FaTruckLoading } from "react-icons/fa";

const DashboardCards = ({incomeData, expenseData }) => {
  
  return (
    <div className="row g-4">
      <div className="col-md-6">
        <div className="card h-100">
          <div className="card-body d-flex align-items-center">
            <div className="flex-grow-1">
              <h6 className="card-subtitle mb-2 text-muted">Total Incomes</h6>
              <h3 className="card-title mb-0">$ {incomeData.total}</h3>
            </div>
            <FaMoneyCheckAlt size={40} style={{ color: "#2ecc71" }} />
          </div>
        </div>
      </div>
      <div className="col-md-6">
        <div className="card h-100">
          <div className="card-body d-flex align-items-center">
            <div className="flex-grow-1">
              <h6 className="card-subtitle mb-2 text-muted">Total Expenses</h6>
              <h3 className="card-title mb-0">$ {expenseData.total}</h3>
            </div>
            <FaTruckLoading size={40} style={{ color: "#e74c3c" }} />
          </div>
        </div>
      </div>
      <div className="col-md-6">
        <div className="card h-100">
          <div className="card-body d-flex align-items-center">
            <div className="flex-grow-1">
              <h6 className="card-subtitle mb-2 text-muted">Monthly Incomes</h6>
              <h3 className="card-title mb-0">$ {incomeData.monthly}</h3>
            </div>
            <FaMoneyCheckAlt size={40} style={{ color: "#2ecc71" }} />
          </div>
        </div>
      </div>
      <div className="col-md-6">
        <div className="card h-100">
          <div className="card-body d-flex align-items-center">
            <div className="flex-grow-1">
              <h6 className="card-subtitle mb-2 text-muted">
                Monthly Expenses
              </h6>
              <h3 className="card-title mb-0">$ {expenseData.monthly}</h3>
            </div>
            <FaTruckLoading size={40} style={{ color: "#e74c3c" }} />
          </div>
        </div>
      </div>
    </div>
  );
};

export default DashboardCards;
