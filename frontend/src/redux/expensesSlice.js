import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { categoryAPI, expenseAPI } from "../services/api"; // Import your API functions

const initialState = {
  expenses: [],
  categories: [],
  status: "idle",
  error: null,
  totalExpenseCount: 0,
};

export const fetchExpenses = createAsyncThunk(
  "expenses/fetchExpenses",
  async ({companyId, pageNumber, pageSize, filters}) => {
    const response = await expenseAPI.getExpenses({companyId, pageNumber, pageSize, filters});
    return response;
  }
);

export const fetchCategories = createAsyncThunk(
  "expenses/fetchCategories",
  async ({companyId,pageNumber,pageSize}) => {
    const response = await categoryAPI.getCategories({companyId,pageNumber,pageSize});
    return response.data;
  }
);

export const addExpense = createAsyncThunk(
  "expenses/addExpense",
  async (newExpense) => {
    const response = await expenseAPI.createExpense(newExpense);
    return response.data;
  }
);

export const updateExpense = createAsyncThunk(
  "expenses/updateExpense",
  async (updatedExpense) => {
    const response = await expenseAPI.updateExpense(updatedExpense);
    return response.data;
  }
);
export const deleteExpense = createAsyncThunk(
  "expenses/deleteExpense",
  async (id) => {
    const response = await expenseAPI.deleteExpense(id);
    return response.data;
  }
);

const expensesSlice = createSlice({
  name: "expenses",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchExpenses.pending, (state) => {
        state.status = "loading";
      })
      .addCase(fetchExpenses.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.expenses = action.payload.data;
        state.totalExpenseCount = action.payload.totalCount;
      })
      .addCase(fetchExpenses.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.error.message;
      })
      .addCase(fetchCategories.pending, (state) => {
        state.status = "loading";
      })
      .addCase(fetchCategories.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.categories = action.payload;
      })
      .addCase(fetchCategories.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.error.message;
      })
      .addCase(addExpense.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.expenses.push(action.payload);
      })
      .addCase(updateExpense.fulfilled, (state, action) => {
        state.status = "succeeded";
        const updatedExpense = action.payload;
        const index = state.expenses.findIndex(
          (expense) => expense.id === updatedExpense.id
        );
        if (index !== -1) {
          state.expenses[index] = updatedExpense;
        }
      })
      .addCase(addExpense.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.error.message;
      })
      .addCase(updateExpense.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.error.message;
      })
      .addCase(deleteExpense.pending, (state) => {
        state.status = "loading";
      })
      .addCase(deleteExpense.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.error.message;
      })
      .addCase(deleteExpense.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.expenses = state.expenses.filter(
          (expense) => expense.id !== action.payload
        );
      });
  },
});

export default expensesSlice.reducer;
