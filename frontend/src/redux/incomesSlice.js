import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { categoryAPI, incomesAPI } from "../services/api";

const initialState = {
  incomes: [],
  categories: [],
  status: "idle",
  error: null,
  totalIncomeCount: 0,
};

export const fetchIncomes = createAsyncThunk(
  "incomes/fetchIncomes",
  async ({companyId,pageNumber,pageSize,filters}) => {
    const response = await incomesAPI.getIncomes({companyId,pageNumber,pageSize,filters});
    return response;
  }
);

export const fetchCategories = createAsyncThunk(
  "incomes/fetchCategories",
  async ({companyId,pageNumber,pageSize}) => {
    const response = await categoryAPI.getCategories({companyId,pageNumber,pageSize});
    return response.data;
  }
);

export const addIncome = createAsyncThunk(
  "incomes/addIncome",
  async (newIncome) => {
    const response = await incomesAPI.createIncomes(newIncome);
    return response.data;
  }
);

export const updateIncome = createAsyncThunk(
  "incomes/updateIncome",
  async (updatedIncome) => {
    const response = await incomesAPI.updateIncomes(updatedIncome);
    return response.data;
  }
);
export const deleteIncome = createAsyncThunk(
  "incomes/deleteIncome",
  async (id) => {
    const response = await incomesAPI.deleteIncomes(id);
    return response.data;
  }
);

const incomesSlice = createSlice({
  name: "incomes",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchIncomes.pending, (state) => {
        state.status = "loading";
      })
      .addCase(fetchIncomes.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.incomes = action.payload.data;
        state.totalIncomeCount = action.payload.totalCount;
      })
      .addCase(fetchIncomes.rejected, (state, action) => {
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
      .addCase(addIncome.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.incomes.push(action.payload);
      })
      .addCase(updateIncome.fulfilled, (state, action) => {
        state.status = "succeeded";
        const updatedIncome = action.payload;
        const index = state.incomes.findIndex(
          (expense) => expense.id === updatedIncome.id
        );
        if (index !== -1) {
          state.incomes[index] = updatedIncome;
        }
      })
      .addCase(addIncome.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.error.message;
      })
      .addCase(updateIncome.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.error.message;
      })
      .addCase(deleteIncome.pending, (state) => {
        state.status = "loading";
      })
      .addCase(deleteIncome.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.error.message;
      })
      .addCase(deleteIncome.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.expenses = state.incomes.filter(
          (expense) => expense.id !== action.payload
        );
      });
  },
});

export default incomesSlice.reducer;
