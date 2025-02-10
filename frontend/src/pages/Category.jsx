import React, { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { categoryAPI } from "../services/api";
import { FiPlus, FiEdit2, FiTrash2 } from "react-icons/fi";
import {
  setCategories,
  addCategory,
  updateCategory as updateCategoryAction,
  deleteCategory as deleteCategoryAction,
  setLoading,
  setError,
} from "../redux/categorySlice";
import Spinner from "../components/Spinner";
import DeleteConfirmationModal from '../components/DeleteConfirmationModal';
import { motion } from "framer-motion";
import { selectCurrentUser } from "../redux/authSlice";

const Category = () => {
  const dispatch = useDispatch();
  const { categories, loading, error } = useSelector((state) => state.category);
  const [isEditing, setIsEditing] = useState(null);
  const [editName, setEditName] = useState("");
  const [newCategoryName, setNewCategoryName] = useState({
    companyId: useSelector(selectCurrentUser)?.companyId,
    name: "",
  });
  const [showAddForm, setShowAddForm] = useState(false);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize] = useState(6);
  const [totalCount, setTotalCount] = useState(0);
  const [deleteModal, setDeleteModal] = useState({
    isOpen: false,
    categoryId: null
  });

  var user = useSelector(selectCurrentUser);
  if(!user){
    const data = localStorage.getItem("user");
    user = JSON.parse(data);
  }
  useEffect(() => {
    const fetchCategories = async () => {
      dispatch(setLoading(true));
      try {
        const response = await categoryAPI.getCategories({companyId: user?.companyId,pageNumber, pageSize});
        dispatch(setCategories(response.data));
        setTotalCount(response.totalCount);
      } catch (error) {
        dispatch(
          setError(
            error.response?.data?.message || error.message || "An error occurred"
          )
        );
      }
    };

    fetchCategories();
  }, [dispatch, pageNumber, pageSize]);

  const totalPages = Math.ceil(totalCount / pageSize);

  const handlePageChange = (newPage) => {
    setPageNumber(newPage);
  };

  const handleDeleteClick = (id) => {
    setDeleteModal({
      isOpen: true,
      categoryId: id
    });
  };

  const handleDeleteConfirm = async () => {
    try {
      await categoryAPI.deleteCategory(deleteModal.categoryId);
      dispatch(deleteCategoryAction(deleteModal.categoryId));
      setDeleteModal({ isOpen: false, categoryId: null });
    } catch (error) {
      dispatch(setError("Failed to delete the category."));
    }
  };

  const startEditing = (category) => {
    setIsEditing(category.id);
    setEditName(category.name);
  };

  const handleUpdate = async (id) => {
    try {
      const updateData = { companyId: user?.companyId, id: id, name: editName };
      await categoryAPI.updateCategory(updateData);
      dispatch(updateCategoryAction(updateData));
      setIsEditing(null);
      setEditName("");
    } catch (error) {
      dispatch(setError("Failed to update the category."));
    }
  };

  const handleAdd = async () => {
    if (newCategoryName.name.trim().length < 3) {
      alert("Category name must be at least 3 characters long.");
      return;
    }

    try {
      const response = await categoryAPI.addCategory({companyId: user?.companyId,name: newCategoryName.name});
      dispatch(addCategory(response.data));
      setNewCategoryName({ name: "" });
      setShowAddForm(false);
    } catch (error) {
      dispatch(
        setError(
          error.response?.data?.message || error.message || "An error occurred"
        )
      );
    }
  };

  if (loading) {
    return <Spinner />;
  }

  if (error) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50 dark:bg-gray-900">
        <div className="bg-red-100 dark:bg-red-900/30 border border-red-400 dark:border-red-800 text-red-700 dark:text-red-400 px-4 py-3 rounded-lg">
          Error loading categories: {error}
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50 dark:bg-gray-900">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        {/* Header Section */}
        <div className="mb-8">
          <motion.div
            initial={{ opacity: 0, y: -20 }}
            animate={{ opacity: 1, y: 0 }}
            className="text-center space-y-2"
          >
            <h2 className="text-3xl font-bold text-gray-900 dark:text-white">
              Kategori Yönetimi
            </h2>
            <p className="text-gray-500 dark:text-gray-400">
              Kategorileri düzenleyin ve yönetin
            </p>
          </motion.div>
        </div>

        {/* Add Category Button */}
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ delay: 0.1 }}
          className="flex justify-end mb-6"
        >
          <button
            onClick={() => setShowAddForm(true)}
            className="inline-flex items-center px-6 py-3 text-sm font-medium
            bg-gradient-to-r from-indigo-500 to-purple-600 text-white rounded-xl
            hover:from-indigo-600 hover:to-purple-700 transform hover:scale-105
            transition-all duration-200 shadow-lg hover:shadow-xl"
          >
            <FiPlus className="h-5 w-5 mr-2" />
            Yeni Kategori Ekle
          </button>
        </motion.div>

        {/* Add Category Form */}
        {showAddForm && (
          <motion.div
            initial={{ opacity: 0, scale: 0.95 }}
            animate={{ opacity: 1, scale: 1 }}
            className="mb-8"
          >
            <div className="max-w-2xl mx-auto bg-white dark:bg-gray-800 rounded-xl border border-gray-200 dark:border-gray-700 shadow-lg p-6">
              <h3 className="text-lg font-semibold text-gray-900 dark:text-white mb-4">
                Yeni Kategori Oluştur
              </h3>
              <div className="space-y-4">
                <input
                  type="text"
                  value={newCategoryName.name}
                  onChange={(e) => setNewCategoryName({ name: e.target.value })}
                  placeholder="Kategori adını giriniz"
                  className="w-full px-4 py-3 rounded-xl border border-gray-300 dark:border-gray-600
                  bg-white dark:bg-gray-700 text-gray-900 dark:text-white
                  focus:border-purple-500 focus:ring-2 focus:ring-purple-500/20
                  transition-all duration-200"
                />
                <div className="flex gap-3">
                  <button
                    onClick={handleAdd}
                    className="flex-1 px-4 py-3 bg-gradient-to-r from-indigo-500 to-purple-600
                    text-white rounded-xl hover:from-indigo-600 hover:to-purple-700
                    transform hover:scale-105 transition-all duration-200"
                  >
                    Ekle
                  </button>
                  <button
                    onClick={() => setShowAddForm(false)}
                    className="flex-1 px-4 py-3 bg-gray-100 dark:bg-gray-700 text-gray-700 dark:text-gray-300
                    rounded-xl hover:bg-gray-200 dark:hover:bg-gray-600
                    transform hover:scale-105 transition-all duration-200"
                  >
                    İptal
                  </button>
                </div>
              </div>
            </div>
          </motion.div>
        )}

        {/* Categories Grid */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {categories.map((category, index) => (
            <motion.div
              initial={{ opacity: 0, y: 20 }}
              animate={{ opacity: 1, y: 0 }}
              transition={{ delay: index * 0.1 }}
              key={category.id}
              className="bg-white dark:bg-gray-800 rounded-xl border border-gray-200 dark:border-gray-700
              shadow-lg hover:shadow-xl transition-all duration-300"
            >
              <div className="p-6">
                <div className="flex items-center justify-between">
                  <div className="flex items-center space-x-3">
                    <div className="flex-shrink-0">
                      <div className="w-10 h-10 rounded-full bg-gradient-to-r from-indigo-500 to-purple-600
                        flex items-center justify-center text-white font-semibold text-lg">
                        {category.name.charAt(0).toUpperCase()}
                      </div>
                    </div>
                    <div>
                      {isEditing === category.id ? (
                        <input
                          type="text"
                          value={editName}
                          onChange={(e) => setEditName(e.target.value)}
                          className="w-full px-3 py-2 rounded-lg border border-gray-300 dark:border-gray-600
                          bg-white dark:bg-gray-700 text-gray-900 dark:text-white
                          focus:border-purple-500 focus:ring-2 focus:ring-purple-500/20
                          transition-all duration-200"
                        />
                      ) : (
                        <h3 className="text-lg font-semibold text-gray-900 dark:text-white">
                          {category.name}
                        </h3>
                      )}
                      <p className="text-sm text-gray-500 dark:text-gray-400">
                        Kategori #{category.id}
                      </p>
                    </div>
                  </div>
                  <div className="flex items-center space-x-2">
                    {isEditing === category.id ? (
                      <>
                        <button
                          onClick={() => handleUpdate(category.id)}
                          className="p-2 text-green-600 hover:bg-green-50 dark:hover:bg-green-900/20
                          rounded-lg transition-colors duration-200"
                        >
                          Kaydet
                        </button>
                        <button
                          onClick={() => setIsEditing(null)}
                          className="p-2 text-gray-600 dark:text-gray-400 hover:bg-gray-50 dark:hover:bg-gray-700
                          rounded-lg transition-colors duration-200"
                        >
                          İptal
                        </button>
                      </>
                    ) : (
                      <>
                        <button
                          onClick={() => startEditing(category)}
                          className="p-2 text-indigo-600 dark:text-indigo-400 hover:bg-indigo-50 dark:hover:bg-indigo-900/20
                          rounded-lg transition-colors duration-200"
                        >
                          <FiEdit2 size={18} />
                        </button>
                        <button
                          onClick={() => handleDeleteClick(category.id)}
                          className="p-2 text-red-600 dark:text-red-400 hover:bg-red-50 dark:hover:bg-red-900/20
                          rounded-lg transition-colors duration-200"
                        >
                          <FiTrash2 size={18} />
                        </button>
                      </>
                    )}
                  </div>
                </div>
              </div>
            </motion.div>
          ))}
        </div>

        {/* Empty State */}
        {categories.length === 0 && !loading && (
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            className="text-center py-12 bg-white dark:bg-gray-800 rounded-xl border
            border-gray-200 dark:border-gray-700 shadow-lg"
          >
            <div className="mb-4">
              <div className="mx-auto w-16 h-16 bg-gray-100 dark:bg-gray-700 rounded-full
                flex items-center justify-center">
                <svg className="h-8 w-8 text-gray-400 dark:text-gray-500" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2}
                    d="M20 13V6a2 2 0 00-2-2H6a2 2 0 00-2 2v7m16 0v5a2 2 0 01-2 2H6a2 2 0 01-2-2v-5m16 0h-2.586a1 1 0 00-.707.293l-2.414 2.414a1 1 0 01-.707.293h-3.172a1 1 0 01-.707-.293l-2.414-2.414A1 1 0 006.586 13H4" />
                </svg>
              </div>
            </div>
            <h3 className="text-lg font-medium text-gray-900 dark:text-white mb-1">
              Henüz kategori bulunmuyor
            </h3>
            <p className="text-gray-500 dark:text-gray-400">
              Yeni bir kategori ekleyerek başlayabilirsiniz
            </p>
          </motion.div>
        )}

        {/* Pagination */}
        {totalCount > 0 && (
          <div className="mt-8 flex justify-center">
            <nav className="flex items-center space-x-2">
              <button
                onClick={() => handlePageChange(pageNumber - 1)}
                disabled={pageNumber === 1}
                className={`px-4 py-2 rounded-lg text-sm font-medium transition-all duration-200 ${
                  pageNumber === 1
                    ? "bg-gray-100 dark:bg-gray-800 text-gray-400 dark:text-gray-600 cursor-not-allowed"
                    : "bg-white dark:bg-gray-800 text-gray-700 dark:text-gray-300 hover:bg-gray-50 dark:hover:bg-gray-700"
                }`}
              >
                Önceki
              </button>
              
              <div className="flex space-x-1">
                {[...Array(totalPages)].map((_, index) => (
                  <button
                    key={index + 1}
                    onClick={() => handlePageChange(index + 1)}
                    className={`px-4 py-2 rounded-lg text-sm font-medium transition-all duration-200 ${
                      pageNumber === index + 1
                        ? "bg-gradient-to-r from-indigo-500 to-purple-600 text-white"
                        : "bg-white dark:bg-gray-800 text-gray-700 dark:text-gray-300 hover:bg-gray-50 dark:hover:bg-gray-700"
                    }`}
                  >
                    {index + 1}
                  </button>
                ))}
              </div>

              <button
                onClick={() => handlePageChange(pageNumber + 1)}
                disabled={pageNumber === totalPages}
                className={`px-4 py-2 rounded-lg text-sm font-medium transition-all duration-200 ${
                  pageNumber === totalPages
                    ? "bg-gray-100 dark:bg-gray-800 text-gray-400 dark:text-gray-600 cursor-not-allowed"
                    : "bg-white dark:bg-gray-800 text-gray-700 dark:text-gray-300 hover:bg-gray-50 dark:hover:bg-gray-700"
                }`}
              >
                Sonraki
              </button>
            </nav>
          </div>
        )}
      </div>

      {/* Delete Confirmation Modal */}
      <DeleteConfirmationModal
        isOpen={deleteModal.isOpen}
        onClose={() => setDeleteModal({ isOpen: false, categoryId: null })}
        onConfirm={handleDeleteConfirm}
        title="Kategoriyi Sil"
        message="Bu kategoriyi silmek istediğinizden emin misiniz? Bu işlem geri alınamaz."
      />
    </div>
  );
};

export default Category;
