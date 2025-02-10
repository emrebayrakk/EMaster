import React from "react";
import { motion, AnimatePresence } from "framer-motion";
import { IoClose } from "react-icons/io5";

const Modal = ({
  isModalOpen,
  isEditMode,
  selectedModal,
  categories,
  onSubmit,
  onClose,
  name,
  isLoading
}) => {
  if (!isModalOpen) return null;

  return (
    <AnimatePresence>
      <motion.div
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        exit={{ opacity: 0 }}
        className="fixed inset-0 z-50 bg-black/60 flex items-center justify-center backdrop-blur-sm p-4"
      >
        <motion.div
          initial={{ scale: 0.95, opacity: 0 }}
          animate={{ scale: 1, opacity: 1 }}
          exit={{ scale: 0.95, opacity: 0 }}
          className="bg-white dark:bg-gray-800 p-6 rounded-2xl shadow-2xl w-full max-w-md relative overflow-hidden"
        >
          {/* Decorative gradient blur */}
          <div className="absolute -top-10 -right-10 w-40 h-40 bg-gradient-to-br from-violet-600/30 to-indigo-600/30 blur-3xl rounded-full"></div>
          
          <button
            onClick={onClose}
            className="absolute right-4 top-4 p-1 rounded-lg hover:bg-gray-100 dark:hover:bg-gray-700 transition-colors"
          >
            <IoClose size={24} className="text-gray-500" />
          </button>

          <h2 className="text-3xl font-bold mb-8 bg-gradient-to-r from-violet-600 to-indigo-600 bg-clip-text text-transparent">
            {isEditMode ? `Update ${name}` : `Add ${name}`}
          </h2>
          
          <form onSubmit={onSubmit} className="space-y-6">
            <div className="space-y-5">
              <div>
                <label className="block text-sm font-medium mb-2 text-gray-700 dark:text-gray-200">
                  Category
                </label>
                <select
                  name="categoryID"
                  defaultValue={selectedModal?.categoryID || ""}
                  className="w-full px-4 py-2.5 rounded-xl border border-gray-300 dark:border-gray-600 bg-white dark:bg-gray-700 focus:ring-2 focus:ring-violet-500 focus:border-transparent transition-all"
                >
                  {categories.map((category) => (
                    <option key={category.id} value={category.id}>
                      {category.name}
                    </option>
                  ))}
                </select>
              </div>

              <div>
                <label className="block text-sm font-medium mb-2 text-gray-700 dark:text-gray-200">
                  Amount
                </label>
                <input
                  type="number"
                  name="amount"
                  defaultValue={selectedModal?.amount || ""}
                  className="w-full px-4 py-2.5 rounded-xl border border-gray-300 dark:border-gray-600 bg-white dark:bg-gray-700 focus:ring-2 focus:ring-violet-500 focus:border-transparent transition-all"
                  required
                />
              </div>

              <div>
                <label className="block text-sm font-medium mb-2 text-gray-700 dark:text-gray-200">
                  Date
                </label>
                <input
                  type="date"
                  name="date"
                  defaultValue={selectedModal?.date?.split("T")[0] || ""}
                  className="w-full px-4 py-2.5 rounded-xl border border-gray-300 dark:border-gray-600 bg-white dark:bg-gray-700 focus:ring-2 focus:ring-violet-500 focus:border-transparent transition-all"
                  required
                />
              </div>

              <div>
                <label className="block text-sm font-medium mb-2 text-gray-700 dark:text-gray-200">
                  Description
                </label>
                <input
                  type="text"
                  name="description"
                  defaultValue={selectedModal?.description || ""}
                  className="w-full px-4 py-2.5 rounded-xl border border-gray-300 dark:border-gray-600 bg-white dark:bg-gray-700 focus:ring-2 focus:ring-violet-500 focus:border-transparent transition-all"
                  required
                />
              </div>
            </div>

            <div className="flex justify-end gap-3 mt-6">
              <button
                type="button"
                onClick={onClose}
                disabled={isLoading}
                className="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 
                         bg-white dark:bg-gray-800 border border-gray-300 dark:border-gray-600 
                         rounded-lg hover:bg-gray-50 dark:hover:bg-gray-700 
                         focus:outline-none focus:ring-2 focus:ring-offset-2 
                         focus:ring-indigo-500 dark:focus:ring-offset-gray-800"
              >
                Cancel
              </button>
              <button
                type="submit"
                disabled={isLoading}
                className="px-4 py-2 text-sm font-medium text-white 
                         bg-gradient-to-r from-indigo-500 to-purple-600 
                         rounded-lg hover:from-indigo-600 hover:to-purple-700 
                         focus:outline-none focus:ring-2 focus:ring-offset-2 
                         focus:ring-indigo-500 dark:focus:ring-offset-gray-800"
              >
                {isLoading ? (
                  <>
                    <span className="spinner-border spinner-border-sm me-2" 
                          role="status" 
                          aria-hidden="true"></span>
                    {isEditMode ? 'Updating...' : 'Creating...'}
                  </>
                ) : (
                  isEditMode ? `Update ${name}` : `Create ${name}`
                )}
              </button>
            </div>
          </form>
        </motion.div>
      </motion.div>
    </AnimatePresence>
  );
};

export default Modal;