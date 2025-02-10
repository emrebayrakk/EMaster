import React, { useEffect, useState, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import { motion } from "framer-motion";
import { IoAdd, IoSearch, IoClose } from "react-icons/io5";
import {
  fetchExpenses,
  fetchCategories,
  addExpense,
  updateExpense,
  deleteExpense,
} from "../redux/expensesSlice";
import Pagination from "../components/Pagination";
import Modal from "../components/Modal";
import Table from "../components/Table";
import Spinner from "../components/Spinner";
import { selectCurrentUser } from "../redux/authSlice";
import debounce from 'lodash/debounce';

const Expenses = () => {
  const dispatch = useDispatch();
  const { expenses, categories, status, error, totalExpenseCount } = useSelector(
    (state) => state.expenses
  );
  var user = useSelector(selectCurrentUser);
  if(!user){
    const data = localStorage.getItem("user");
    user = JSON.parse(data);
  }
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isEditMode, setIsEditMode] = useState(false);
  const [selectedExpense, setSelectedExpense] = useState(null);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [isLoading, setIsLoading] = useState(false);
  const [searchText, setSearchText] = useState("");
  const [debouncedSearchText, setDebouncedSearchText] = useState("");

  const isInitialMount = React.useRef(true);

  const debouncedSearch = useCallback(
    debounce((text) => {
      setDebouncedSearchText(text);
    }, 500),
    []
  );

  useEffect(() => {
    dispatch(fetchCategories({ companyId: user?.companyId, pageNumber: 1, pageSize: 100 }));
  }, []);
  useEffect(() => {
    if (isInitialMount.current) {
      dispatch(fetchExpenses({ 
        companyId: user?.companyId, 
        pageNumber, 
        pageSize,
        filters: null
      }));
      isInitialMount.current = false;
      return;
    }
    if (debouncedSearchText.length >= 3 || debouncedSearchText === "") {
      const filters = debouncedSearchText.length >= 3 ? [
        {
          propertyName: "Description",
          value: debouncedSearchText,
          comparison: 6
        }
      ] : null;

      dispatch(fetchExpenses({ 
        companyId: user?.companyId, 
        pageNumber, 
        pageSize,
        filters 
      }));
    }
  }, [pageNumber, pageSize, debouncedSearchText]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsLoading(true);
    const formData = {
      companyId: user?.companyId,
      categoryID: parseInt(e.target.categoryID.value),
      amount: parseFloat(e.target.amount.value),
      date: e.target.date.value,
      description: e.target.description.value,
    };

    try {
      if (isEditMode) {
        await dispatch(updateExpense({ ...formData, id: selectedExpense.id }));
      } else {
        await dispatch(addExpense(formData));
      }
      closeModal();
    } catch (error) {
      console.error("Error:", error);
    } finally {
      setIsLoading(false);
    }
  };

  const closeModal = () => {
    setIsModalOpen(false);
    setIsEditMode(false);
    setSelectedExpense(null);
  };

  const openEditModal = (expense) => {
    setSelectedExpense(expense);
    setIsEditMode(true);
    setIsModalOpen(true);
  };

  const handleSearch = (e) => {
    setSearchText(e.target.value);
    debouncedSearch(e.target.value);
    setPageNumber(1);
  };

  const clearSearch = () => {
    setSearchText("");
    setDebouncedSearchText("");
    setPageNumber(1);
  };

  if (status === "loading" && debouncedSearchText.length < 3) {
    return <Spinner />;
  }

  if (status === "failed") {
    return (
      <div className="text-center py-12">
        <div className="inline-flex items-center px-4 py-2 rounded-lg bg-red-50 dark:bg-red-900/20">
          <span className="text-red-600 dark:text-red-400">
            Hata oluştu: {error}
          </span>
        </div>
      </div>
    );
  }

  return (
    <motion.div
      initial={{ opacity: 0, y: 20 }}
      animate={{ opacity: 1, y: 0 }}
      className="container mx-auto px-4 py-8 max-w-7xl"
    >
      <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4 mb-12">
        <div className="relative">
          <h1 className="text-5xl font-medium tracking-tight">
            <span className="relative">
              <span className="bg-gradient-to-r from-gray-800 to-gray-600 dark:from-white dark:to-gray-200 bg-clip-text text-transparent">
                Expenses
              </span>
              <div className="absolute bottom-0 left-0 w-8 h-[3px] bg-indigo-500"></div>
            </span>
          </h1>
        </div>

        <button
          onClick={() => setIsModalOpen(true)}
          className="group flex items-center gap-2 px-6 py-2.5 rounded-lg bg-gray-900 dark:bg-white text-white dark:text-gray-900 hover:bg-gray-800 dark:hover:bg-gray-100 transition-all duration-200"
        >
          <IoAdd 
            size={20} 
            className="transition-transform duration-200 group-hover:-rotate-90"
          />
          <span className="font-medium">
            Add expense
          </span>
        </button>
      </div>

      <div className="mb-6">
        <div className="relative group">
          <input
            type="text"
            value={searchText}
            onChange={handleSearch}
            placeholder="Search expenses..."
            className="w-full px-4 py-3.5 pr-12 rounded-xl border border-gray-200 dark:border-gray-700 bg-white dark:bg-gray-800 
            focus:outline-none focus:ring-2 focus:ring-indigo-500/50 dark:focus:ring-indigo-400/50 
            shadow-sm hover:border-gray-300 dark:hover:border-gray-600
            transition-all duration-200"
          />
          {!searchText && (
            <div className="absolute right-4 top-1/2 transform -translate-y-1/2 text-gray-400 dark:text-gray-500 
              transition-all duration-200 group-hover:text-indigo-500 dark:group-hover:text-indigo-400">
              <IoSearch size={20} />
            </div>
          )}
          {searchText && (
            <button
              onClick={clearSearch}
              className="absolute right-4 top-1/2 transform -translate-y-1/2 p-1 rounded-full
                text-gray-400 hover:text-gray-600 dark:text-gray-500 dark:hover:text-gray-300 
                hover:bg-gray-100 dark:hover:bg-gray-700
                transition-all duration-200"
            >
              <IoClose size={18} />
            </button>
          )}
          {searchText.length > 0 && searchText.length < 3 && (
            <div className="absolute left-0 right-0 -bottom-6">
              <p className="text-xs text-gray-500 dark:text-gray-400">
                Please enter at least 3 characters to search
              </p>
            </div>
          )}
        </div>
      </div>

      <Table
        data={expenses}
        onEdit={openEditModal}
        onDelete={(id) => dispatch(deleteExpense(id))}
      />

      {totalExpenseCount > pageSize && (
        <div className="mt-6">
          <Pagination
            pageNumber={pageNumber}
            totalPages={Math.ceil(totalExpenseCount / pageSize)}
            onPageChange={setPageNumber}
            pageSize={pageSize}
            onPageSizeChange={(newSize) => {
              setPageSize(newSize);
              setPageNumber(1);
            }}
          />
        </div>
      )}

      <Modal
        isModalOpen={isModalOpen}
        isEditMode={isEditMode}
        selectedModal={selectedExpense}
        categories={categories}
        onSubmit={handleSubmit}
        onClose={closeModal}
        name="Expense"
        isLoading={isLoading}
      />
    </motion.div>
  );
};

export default Expenses;
