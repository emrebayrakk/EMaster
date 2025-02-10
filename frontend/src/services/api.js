import axios from "axios";

const BASE_URL = "https://localhost:7103";
const AI_BASE_URL = "https://darkness.ashlynn.workers.dev";

const api = axios.create({
  baseURL: BASE_URL,
  headers: {
    "Content-Type": "application/json",
  },
});
const aiApi = axios.create({
  baseURL: AI_BASE_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("token");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

api.interceptors.response.use(
  (response) => {
    const responseData = response.data;
    if (responseData.code == 200) {
      return {
        code: response.status,
        status: true,
        message: "Success",
        totalCount: responseData.totalCount,
        pageNumber: responseData.pageCount,
        pageSize: responseData.pageNumber,
        data: responseData.data || responseData,
      };
    }else {
      return Promise.reject({
        code: error.response?.status || 500,
        status: false,
        message: error.response?.data?.message || "An error occurred",
        totalCount: responseData.totalCount,
        pageNumber: responseData.pageCount,
        pageSize: responseData.pageNumber,
        data: null,
      });
    }
  },
  (error) => {
    if (error.response?.status == 401) {
      localStorage.removeItem("token");
      window.location.href = "/login";
    } else {
      return Promise.reject({
        code: error.response?.status || 500,
        status: false,
        message: error.response?.data?.message || "An error occurred",
        data: null,
      });
    }
  }
);

aiApi.interceptors.response.use(
  (response) => {
    console.log(response.data.response);
      return {
        code: response.status,
        status: true,
        message: "Success",
        data: response.data.response,
      };
    },
  (error) => {
      return Promise.reject({
        code: error.response?.status || 500,
        status: false,
        message: error.response?.data?.message || "An error occurred",
        data: null,
      });
  }
);

export const authAPI = {
  login: (credentials) => api.post("/User/login", credentials),
  register: (userData) => api.post("/User/register", userData),
};

export const dashboardAPI = {
  getExpenseSalary: (companyId) => api.post("/Expense/GetSalary",companyId),
  getIncomeSalary: (companyId) => api.post("/Income/GetSalary",companyId),
  getExpenseMonthlyCategory: (companyId) =>
    api.post("/Expense/GetExpenseMonthlyCategory",companyId),
  getIncomeMonthlyCategory: (companyId) => api.post("/Income/GetIncomeMonthlyCategory",companyId),
};

export const categoryAPI = {
  getCategories: (categoryData) => api.post("/Category/List",categoryData),
  addCategory: (categoryData) => api.post("/Category/Create", categoryData),
  updateCategory: (categoryData) => api.put("/Category/Update", categoryData),
  deleteCategory: (categoryId) => api.delete(`/Category/Delete/${categoryId}`),
};

export const expenseAPI = {
  getExpenses: (expenseData) => api.post("/Expense/List", expenseData),
  createExpense: (expenseData) => api.post("/Expense/Create", expenseData),
  updateExpense: (expenseData) => api.put(`/Expense/Update`, expenseData),
  deleteExpense: (expenseId) => api.delete(`/Expense/Delete/${expenseId}`),
};
export const incomesAPI = {
  getIncomes: (incomeData) => api.post("/Income/List",incomeData),
  createIncomes: (incomeData) => api.post("/Income/Create", incomeData),
  updateIncomes: (incomeData) => api.put(`/Income/Update`, incomeData),
  deleteIncomes: (incomeId) => api.delete(`/Income/Delete/${incomeId}`),
};

export const aiChatAPI = {
  sendMessage: (message) => aiApi.get(`/chat/?prompt=${message}&model=claude-3-haiku-20240307`),
};

export default api;
