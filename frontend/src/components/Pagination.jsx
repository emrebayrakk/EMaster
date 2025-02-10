import React from 'react';
import { motion } from 'framer-motion';
import { FiChevronLeft, FiChevronRight, FiList } from 'react-icons/fi';

const Pagination = ({ pageNumber, totalPages, onPageChange, pageSize, onPageSizeChange }) => {
  const pageSizeOptions = [5, 10, 25, 50];

  return (
    <motion.div 
      initial={{ opacity: 0, y: 20 }}
      animate={{ opacity: 1, y: 0 }}
      className="mt-6 flex flex-col sm:flex-row justify-between items-center gap-4 py-4 px-6 bg-white dark:bg-gray-800 rounded-2xl border border-gray-200/50 dark:border-gray-700/50 shadow-xl backdrop-blur-xl"
    >
      {/* Page Size Selector */}
      <div className="flex items-center gap-3">
        <div className="w-10 h-10 rounded-xl bg-gray-50 dark:bg-gray-700 flex items-center justify-center">
          <FiList className="w-5 h-5 text-gray-600 dark:text-gray-400" />
        </div>
        <select
          value={pageSize}
          onChange={(e) => onPageSizeChange(Number(e.target.value))}
          className="px-4 py-2 bg-gray-50 dark:bg-gray-700 border-0 rounded-xl text-gray-600 dark:text-gray-300 font-medium focus:outline-none focus:ring-2 focus:ring-indigo-500/20 dark:focus:ring-indigo-500/20 transition-all duration-200"
        >
          {pageSizeOptions.map(size => (
            <option key={size} value={size}>
              Show {size} Records
            </option>
          ))}
        </select>
      </div>

      {/* Pagination Controls */}
      <div className="flex items-center gap-2">
        <motion.button
          whileHover={{ scale: 1.05 }}
          whileTap={{ scale: 0.95 }}
          onClick={() => onPageChange(pageNumber - 1)}
          disabled={pageNumber === 1}
          className={`w-10 h-10 rounded-xl flex items-center justify-center transition-all duration-200 ${
            pageNumber === 1
              ? "bg-gray-50 dark:bg-gray-700 text-gray-400 dark:text-gray-500 cursor-not-allowed"
              : `bg-gray-50 dark:bg-gray-700 text-gray-600 dark:text-gray-400 hover:bg-indigo-50 dark:hover:bg-indigo-900/20 hover:text-indigo-600 dark:hover:text-indigo-400`
          }`}
        >
          <FiChevronLeft className="w-5 h-5" />
        </motion.button>

        <div className="flex gap-2">
          {[...Array(totalPages)].map((_, index) => {
            const pageIndex = index + 1;
            const isCurrentPage = pageNumber === pageIndex;
            const isNearCurrentPage = Math.abs(pageNumber - pageIndex) <= 1;
            const isFirstOrLast = pageIndex === 1 || pageIndex === totalPages;

            if (!isNearCurrentPage && !isFirstOrLast) {
              if (pageIndex === 2 || pageIndex === totalPages - 1) {
                return (
                  <span key={pageIndex} className="w-10 h-10 rounded-xl flex items-center justify-center text-gray-400 dark:text-gray-500">
                    ...
                  </span>
                );
              }
              return null;
            }

            return (
              <motion.button
                key={pageIndex}
                onClick={() => onPageChange(pageIndex)}
                whileHover={{ scale: 1.05 }}
                whileTap={{ scale: 0.95 }}
                className={`w-10 h-10 rounded-xl text-sm font-medium transition-all duration-200 ${
                  isCurrentPage
                    ? "bg-gradient-to-r from-indigo-500 to-purple-500 text-white shadow-lg shadow-indigo-500/25"
                    : `bg-gray-50 dark:bg-gray-700 text-gray-600 dark:text-gray-400 hover:bg-indigo-50 dark:hover:bg-indigo-900/20 hover:text-indigo-600 dark:hover:text-indigo-400`
                }`}
              >
                {pageIndex}
              </motion.button>
            );
          })}
        </div>

        <motion.button
          whileHover={{ scale: 1.05 }}
          whileTap={{ scale: 0.95 }}
          onClick={() => onPageChange(pageNumber + 1)}
          disabled={pageNumber === totalPages}
          className={`w-10 h-10 rounded-xl flex items-center justify-center transition-all duration-200 ${
            pageNumber === totalPages
              ? "bg-gray-50 dark:bg-gray-700 text-gray-400 dark:text-gray-500 cursor-not-allowed"
              : `bg-gray-50 dark:bg-gray-700 text-gray-600 dark:text-gray-400 hover:bg-indigo-50 dark:hover:bg-indigo-900/20 hover:text-indigo-600 dark:hover:text-indigo-400`
          }`}
        >
          <FiChevronRight className="w-5 h-5" />
        </motion.button>
      </div>
    </motion.div>
  );
};

export default Pagination;