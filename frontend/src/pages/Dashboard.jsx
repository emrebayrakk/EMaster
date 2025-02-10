import DashboardCards from "../components/DashboardCards";
import DashboardHeader from "../components/DashboardHeader";

import { useDispatch, useSelector } from "react-redux";
import {
  fetchExpenseSalary,
  fetchIncomeSalary,
  fetchExpenseMonthlyCategory,
  fetchIncomeMonthlyCategory,
} from "../redux/dashboardSlice";
import { useEffect } from "react";
import MonthlyChart from "../components/MonthlyChart";
import Spinner from "../components/Spinner";
import React from "react";
import { selectCurrentUser } from "../redux/authSlice";

const Dashboard = () => {

  const dispatch = useDispatch();
  const {
    expenseData,
    incomeData,
    expenseMonthlyCategory,
    incomeMonthlyCategory,
    status,
    error,
  } = useSelector((state) => state.dashboard);
  var user = useSelector(selectCurrentUser);
  if(!user){
    const data = localStorage.getItem("user");
    user = JSON.parse(data);
  }
  useEffect(() => {
    dispatch(fetchIncomeSalary({companyId: user?.companyId}));
    dispatch(fetchExpenseSalary({companyId: user?.companyId}));
    dispatch(fetchExpenseMonthlyCategory({companyId: user?.companyId}));
    dispatch(fetchIncomeMonthlyCategory({companyId: user?.companyId}));
  }, [dispatch]);
  if (status === "loading") {
    return (
      <div className="flex items-center justify-center min-h-screen bg-[#f8fafc]">
        <div className="p-8 bg-white/40 backdrop-blur-lg rounded-full">
          <Spinner />
        </div>
      </div>
    );
  }

  if (status === "failed") {
    return (
      <div className="flex items-center justify-center min-h-screen bg-[#f8fafc]">
        <div className="p-8 bg-white rounded-[2rem] shadow-[0_8px_30px_rgb(0,0,0,0.06)] border-l-4 border-red-500">
          <p className="text-xl font-medium text-gray-800">
            <span className="text-red-500 font-semibold">Error:</span> {error}
          </p>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-[#f8fafc]">
      <main className="container mx-auto px-6 py-8">
        <DashboardHeader user={user} />

        <div className="bg-white/80 backdrop-blur-xl rounded-[2rem] shadow-[0_8px_30px_rgb(0,0,0,0.06)] p-8 mb-8">
          <DashboardCards
            incomeData={incomeData}
            expenseData={expenseData}
          />
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
          <div className="group relative h-full">
            <div className="absolute inset-0 bg-gradient-to-r from-purple-500/20 to-pink-500/20 rounded-[2rem] blur-3xl transition-all duration-300 group-hover:blur-xl"></div>
            <div className="relative h-full bg-white/80 backdrop-blur-xl p-8 rounded-[2rem] shadow-[0_8px_30px_rgb(0,0,0,0.06)] border border-white/20
                          transition-all duration-300 hover:shadow-lg hover:bg-white">
              <h3 className="text-xl font-bold text-gray-800 mb-6 flex items-center gap-3">
                <span className="flex h-10 w-10 items-center justify-center rounded-xl bg-purple-500/10">
                  <span className="h-3 w-3 rounded-full bg-purple-500"></span>
                </span>
                Expense Categories
              </h3>
              <div className="w-full transition-transform duration-300 group-hover:scale-[1.01]">
                <MonthlyChart
                  data={expenseMonthlyCategory}
                  title="Expense Categories"
                />
              </div>
            </div>
          </div>
          
          <div className="group relative h-full">
            <div className="absolute inset-0 bg-gradient-to-r from-blue-500/20 to-cyan-500/20 rounded-[2rem] blur-3xl transition-all duration-300 group-hover:blur-xl"></div>
            <div className="relative h-full bg-white/80 backdrop-blur-xl p-8 rounded-[2rem] shadow-[0_8px_30px_rgb(0,0,0,0.06)] border border-white/20
                          transition-all duration-300 hover:shadow-lg hover:bg-white">
              <h3 className="text-xl font-bold text-gray-800 mb-6 flex items-center gap-3">
                <span className="flex h-10 w-10 items-center justify-center rounded-xl bg-blue-500/10">
                  <span className="h-3 w-3 rounded-full bg-blue-500"></span>
                </span>
                Income Categories
              </h3>
              <div className="w-full transition-transform duration-300 group-hover:scale-[1.01]">
                <MonthlyChart 
                  data={incomeMonthlyCategory} 
                  title="Income Categories" 
                />
              </div>
            </div>
          </div>
        </div>
      </main>
    </div>
  );
};

export default Dashboard;
