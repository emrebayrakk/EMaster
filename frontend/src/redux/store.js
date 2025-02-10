import { configureStore } from "@reduxjs/toolkit";
import authReducer from "./authSlice.js";
import categoryReducer from "./categorySlice.js";
import expensesReducer from "./expensesSlice.js";
import incomesReducer from "./incomesSlice.js";
import dashboardReducer from "./dashboardSlice.js";

export const store = configureStore({
  reducer: {
    auth: authReducer,
    category: categoryReducer,
    expenses: expensesReducer,
    incomes: incomesReducer,
    dashboard: dashboardReducer,
  },
});

export default store;
