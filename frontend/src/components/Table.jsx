import React, { useState } from "react";
import { motion, AnimatePresence } from "framer-motion";
import { FiEdit2, FiTrash2, FiClock, FiDollarSign, FiAlignLeft,FiEdit  } from "react-icons/fi";
import DeleteConfirmationModal from './DeleteConfirmationModal';

const Table = ({ data, onEdit, onDelete }) => {
  const [deleteModal, setDeleteModal] = useState({
    isOpen: false,
    itemId: null
  });

  const handleDeleteClick = (id) => {
    setDeleteModal({
      isOpen: true,
      itemId: id
    });
  };

  const handleDeleteConfirm = () => {
    onDelete(deleteModal.itemId);
    setDeleteModal({ isOpen: false, itemId: null });
  };

  if (!data?.length) {
    return (
      <motion.div
        initial={{ opacity: 0, y: 20 }}
        animate={{ opacity: 1, y: 0 }}
        className="text-center py-20 bg-white dark:bg-gray-800 rounded-2xl shadow-xl border border-gray-200/50 dark:border-gray-700/50 backdrop-blur-xl"
      >
        <div className="space-y-6">
          <div className="w-24 h-24 mx-auto bg-gradient-to-tr from-gray-100 to-gray-50 dark:from-gray-700 dark:to-gray-800 rounded-full flex items-center justify-center shadow-inner">
            <svg
              className="w-12 h-12 text-gray-400 dark:text-gray-500"
              fill="none"
              stroke="currentColor"
              viewBox="0 0 24 24"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth={1.5}
                d="M9 5H7a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2"
              />
            </svg>
          </div>
          <div>
            <h3 className="text-2xl font-semibold text-gray-900 dark:text-white mb-2">
              No Records Found
            </h3>
            <p className="text-gray-500 dark:text-gray-400">
              No records have been added yet. Start by adding a new record.
            </p>
          </div>
        </div>
      </motion.div>
    );
  }

  return (
    <>
      <div className="bg-white dark:bg-gray-800 rounded-2xl shadow-xl border border-gray-200/50 dark:border-gray-700/50 backdrop-blur-xl overflow-hidden">
        <div className="overflow-x-auto">
          <table className="w-full">
            <thead>
              <tr className="bg-gradient-to-r from-gray-50 to-gray-100 dark:from-gray-800 dark:to-gray-750">
                <th className="px-6 py-5 text-left">
                  <div className="flex items-center gap-2 text-sm font-semibold text-gray-900 dark:text-white">
                    <span className="w-8 h-8 rounded-lg bg-indigo-50 dark:bg-indigo-900/20 flex items-center justify-center">
                      <FiAlignLeft className="w-4 h-4 text-indigo-600 dark:text-indigo-400" />
                    </span>
                    Category
                  </div>
                </th>
                <th className="px-6 py-5 text-left">
                  <div className="flex items-center gap-2 text-sm font-semibold text-gray-900 dark:text-white">
                    <span className="w-8 h-8 rounded-lg bg-emerald-50 dark:bg-emerald-900/20 flex items-center justify-center">
                      <FiDollarSign className="w-4 h-4 text-emerald-600 dark:text-emerald-400" />
                    </span>
                    Amount
                  </div>
                </th>
                <th className="px-6 py-5 text-left">
                  <div className="flex items-center gap-2 text-sm font-semibold text-gray-900 dark:text-white">
                    <span className="w-8 h-8 rounded-lg bg-purple-50 dark:bg-purple-900/20 flex items-center justify-center">
                      <FiClock className="w-4 h-4 text-purple-600 dark:text-purple-400" />
                    </span>
                    Date
                  </div>
                </th>
                <th className="px-6 py-5 text-left">
                  <div className="flex items-center gap-2 text-sm font-semibold text-gray-900 dark:text-white">
                    <span className="w-8 h-8 rounded-lg bg-blue-50 dark:bg-blue-900/20 flex items-center justify-center">
                      <FiAlignLeft className="w-4 h-4 text-blue-600 dark:text-blue-400" />
                    </span>
                    Description
                  </div>
                </th>
                <th className="px-6 py-5 text-left">
                  <div className="flex items-center gap-2 text-sm font-semibold text-gray-900 dark:text-white">
                    <span className="w-8 h-8 rounded-lg bg-red-50 dark:bg-red-900/20 flex items-center justify-center">
                      <FiEdit className="w-4 h-4 text-red-600 dark:text-red-400" />
                    </span>
                    Actions
                  </div>
                </th>
              </tr>
            </thead>
            <tbody className="divide-y divide-gray-100 dark:divide-gray-700/50">
              <AnimatePresence>
                {data.map((item, index) => (
                  <motion.tr
                    initial={{ opacity: 0, y: 20 }}
                    animate={{ opacity: 1, y: 0 }}
                    exit={{ opacity: 0, y: -20 }}
                    transition={{ delay: index * 0.1 }}
                    key={item.id}
                    className="group hover:bg-gray-50/50 dark:hover:bg-gray-800/50 transition-all duration-200"
                  >
                    <td className="px-6 py-4">
                      <div className="flex items-center gap-3">
                        <div className="w-10 h-10 rounded-xl bg-gradient-to-r from-indigo-500 to-purple-500 
                        flex items-center justify-center text-white font-medium shadow-lg shadow-indigo-500/20">
                          {item.categoryName.charAt(0).toUpperCase()} 
                        </div>
                        <span className="text-sm font-medium text-gray-900 dark:text-white">
                          {item.categoryName}
                        </span>
                      </div>
                    </td>
                    <td className="px-6 py-4">
                      <span className="text-sm font-semibold text-emerald-600 dark:text-emerald-400">
                        {item.amount.toLocaleString('tr-TR', { minimumFractionDigits: 2 })}
                      </span>
                    </td>
                    <td className="px-6 py-4">
                      <span className="text-sm text-gray-600 dark:text-gray-400">
                        {new Date(item.date).toLocaleDateString('tr-TR')}
                      </span>
                    </td>
                    <td className="px-6 py-4">
                      <span className="text-sm text-gray-600 dark:text-gray-400">
                        {item.description}
                      </span>
                    </td>
                    <td className="px-6 py-4">
                      <div className="flex justify-end space-x-2">
                        <motion.button
                          whileHover={{ scale: 1.05 }}
                          whileTap={{ scale: 0.95 }}
                          onClick={() => onEdit(item)}
                          className="p-2 rounded-xl text-gray-600 hover:text-indigo-600 dark:text-gray-400 
                          dark:hover:text-indigo-400 hover:bg-indigo-50 dark:hover:bg-indigo-900/20 
                          transition-all duration-200"
                        >
                          <FiEdit2 size={18} />
                        </motion.button>
                        <motion.button
                          whileHover={{ scale: 1.05 }}
                          whileTap={{ scale: 0.95 }}
                          onClick={() => handleDeleteClick(item.id)}
                          className="p-2 rounded-xl text-gray-600 hover:text-red-600 dark:text-gray-400 
                          dark:hover:text-red-400 hover:bg-red-50 dark:hover:bg-red-900/20 
                          transition-all duration-200"
                        >
                          <FiTrash2 size={18} />
                        </motion.button>
                      </div>
                    </td>
                  </motion.tr>
                ))}
              </AnimatePresence>
            </tbody>
          </table>
        </div>
      </div>

      <DeleteConfirmationModal
        isOpen={deleteModal.isOpen}
        onClose={() => setDeleteModal({ isOpen: false, itemId: null })}
        onConfirm={handleDeleteConfirm}
        title="Delete Record"
        message="Are you sure you want to delete this record? This action cannot be undone."
      />
    </>
  );
};

export default Table;