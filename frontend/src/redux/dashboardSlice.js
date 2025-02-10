import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { dashboardAPI } from "../services/api";

const initialState = {
  expenseData: {},
  incomeData: {},
  expenseMonthlyCategory: [],
  incomeMonthlyCategory: [],
  status: "idle",
  error: null,
};

export const fetchIncomeSalary = createAsyncThunk(
  "incomes/getSalary",
  async ({companyId}) => {
    const response = await dashboardAPI.getIncomeSalary({companyId});
    return response.data;
  }
);

export const fetchExpenseSalary = createAsyncThunk(
  "expenses/getSalary",
  async ({companyId}) => {
    const response = await dashboardAPI.getExpenseSalary({companyId});
    return response.data;
  }
);
export const fetchExpenseMonthlyCategory = createAsyncThunk(
  "expenses/getExpenseMonthlyCategory",
  async ({companyId}) => {
    const response = await dashboardAPI.getExpenseMonthlyCategory({companyId});
    return response.data;
  }
);
export const fetchIncomeMonthlyCategory = createAsyncThunk(
  "incomes/getIncomeMonthlyCategory",
  async ({companyId}) => {
    const response = await dashboardAPI.getIncomeMonthlyCategory({companyId});
    return response.data;
  }
);

const dashboardSlice = createSlice({
  name: "dashboard",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchIncomeSalary.pending, (state) => {
        state.status = "loading";
      })
      .addCase(fetchIncomeSalary.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.incomeData = action.payload;
      })
      .addCase(fetchExpenseMonthlyCategory.pending, (state) => {
        state.status = "loading";
      })
      .addCase(fetchExpenseMonthlyCategory.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.expenseMonthlyCategory = action.payload;
      })
      .addCase(fetchIncomeMonthlyCategory.pending, (state) => {
        state.status = "loading";
      })
      .addCase(fetchIncomeMonthlyCategory.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.incomeMonthlyCategory = action.payload;
      })
      .addCase(fetchExpenseSalary.pending, (state) => {
        state.status = "loading";
      })
      .addCase(fetchExpenseSalary.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.expenseData = action.payload;
      });
  },
});

export default dashboardSlice.reducer;
