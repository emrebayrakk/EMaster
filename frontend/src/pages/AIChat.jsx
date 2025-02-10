import React, { useState, useRef, useEffect } from 'react';
import { aiChatAPI } from '../services/api';
import { motion, AnimatePresence } from 'framer-motion';

const AIChat = () => {
  const [message, setMessage] = useState('');
  const [chatHistory, setChatHistory] = useState([]);
  const [loading, setLoading] = useState(false);
  const messagesEndRef = useRef(null);
  const inputRef = useRef(null);

  const scrollToBottom = () => {
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
  };

  useEffect(() => {
    scrollToBottom();
    inputRef.current?.focus();
  }, [chatHistory]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!message.trim()) return;

    const userMessage = message;
    setMessage('');
    setLoading(true);

    setChatHistory(prev => [...prev, { type: 'user', content: userMessage }]);

    try {
      const result = await aiChatAPI.sendMessage(userMessage);
      setChatHistory(prev => [...prev, { type: 'ai', content: result.data }]);
    } catch (error) {
      console.error('Error fetching AI response:', error);
      setChatHistory(prev => [...prev, { 
        type: 'error', 
        content: 'Bir hata oluştu. Lütfen tekrar deneyin.' 
      }]);
    } finally {
      setLoading(false);
    }
  };

  const MessageBubble = ({ message }) => {
    const isUser = message.type === 'user';
    const bubbleClass = isUser
      ? 'bg-gradient-to-br from-blue-500 to-indigo-600 text-white ml-auto'
      : message.type === 'error'
      ? 'bg-gradient-to-br from-red-500 to-red-600 text-white'
      : 'bg-white dark:bg-gray-800 dark:text-white border border-gray-100 dark:border-gray-700';

    const avatarLetter = isUser ? 'S' : 'A';
    const avatarClass = isUser 
      ? 'bg-gradient-to-br from-blue-600 to-indigo-700'
      : 'bg-gradient-to-br from-violet-600 to-purple-700';

    return (
      <motion.div
        initial={{ opacity: 0, y: 20 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.3 }}
        className={`flex items-start gap-3 mb-6 ${isUser ? 'flex-row-reverse' : 'flex-row'}`}
      >
        <div className={`${avatarClass} w-10 h-10 rounded-full flex items-center justify-center text-white text-sm font-medium flex-shrink-0 shadow-lg`}>
          {avatarLetter}
        </div>
        <div className={`max-w-[80%] rounded-2xl px-6 py-4 shadow-md ${bubbleClass} 
          backdrop-blur-lg hover:shadow-lg transition-all duration-300`}>
          <p className="text-sm md:text-base leading-relaxed">{message.content}</p>
        </div>
      </motion.div>
    );
  };

  const LoadingIndicator = () => (
    <div className="flex items-center gap-2 justify-start my-6">
      <motion.div
        animate={{
          scale: [1, 1.2, 1],
          opacity: [0.5, 1, 0.5]
        }}
        transition={{
          duration: 1.5,
          repeat: Infinity,
          ease: "easeInOut"
        }}
        className="flex gap-2"
      >
        <span className="w-2 h-2 rounded-full bg-violet-500"></span>
        <span className="w-2 h-2 rounded-full bg-violet-500" style={{ animationDelay: "0.2s" }}></span>
        <span className="w-2 h-2 rounded-full bg-violet-500" style={{ animationDelay: "0.4s" }}></span>
      </motion.div>
    </div>
  );

  return (
    <div className="flex flex-col h-screen bg-gradient-to-br from-gray-50 to-white dark:from-gray-900 dark:to-gray-800">
      <div className="bg-white/80 dark:bg-gray-800/80 backdrop-blur-xl shadow-sm border-b border-gray-100 dark:border-gray-700">
        <div className="container mx-auto max-w-5xl py-4 px-4">
          <div className="flex items-center justify-between">
            <div className="flex items-center gap-4">
              <div className="w-12 h-12 rounded-2xl bg-gradient-to-br from-violet-500 to-purple-600 flex items-center justify-center shadow-lg">
                <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6 text-white" viewBox="0 0 24 24" fill="none" stroke="currentColor">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M8 10h.01M12 10h.01M16 10h.01M9 16H5a2 2 0 01-2-2V6a2 2 0 012-2h14a2 2 0 012 2v8a2 2 0 01-2 2h-5l-5 5v-5z" />
                </svg>
              </div>
              <div>
                <h1 className="text-2xl font-bold bg-gradient-to-r from-violet-600 to-purple-600 text-transparent bg-clip-text">
                  AI Sohbet Asistanı
                </h1>
                <p className="text-sm text-gray-500 dark:text-gray-400">
                  Yapay zeka destekli sohbet deneyimi
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div className="flex-1 overflow-y-auto px-4 py-6 scroll-smooth">
        <div className="container mx-auto max-w-5xl space-y-6">
          <AnimatePresence>
            {chatHistory.length === 0 && (
              <motion.div
                initial={{ opacity: 0, y: 20 }}
                animate={{ opacity: 1, y: 0 }}
                exit={{ opacity: 0, y: -20 }}
                className="text-center text-gray-500 dark:text-gray-400 mt-10 space-y-4"
              >
                <div className="text-7xl mb-6 animate-bounce">👋</div>
                <h2 className="text-2xl font-semibold text-gray-700 dark:text-gray-300">
                  Hoş Geldiniz!
                </h2>
                <p className="text-lg">Benimle sohbet etmeye başlayabilirsiniz.</p>
                <p className="text-sm text-gray-400">Her konuda size yardımcı olmaktan mutluluk duyarım</p>
              </motion.div>
            )}
            {chatHistory.map((msg, index) => (
              <MessageBubble key={index} message={msg} />
            ))}
          </AnimatePresence>
          {loading && <LoadingIndicator />}
          <div ref={messagesEndRef} />
        </div>
      </div>
      <div className="sticky bottom-0 left-0 right-0 bg-gradient-to-t from-white via-white to-transparent dark:from-gray-800 dark:via-gray-800 dark:to-transparent backdrop-blur-xl border-t border-gray-100 dark:border-gray-700 p-4">
        <form onSubmit={handleSubmit} className="container mx-auto max-w-5xl">
          <div className="flex gap-3 items-center bg-white dark:bg-gray-700 p-2 rounded-2xl shadow-lg hover:shadow-xl transition-all duration-300 border border-gray-100 dark:border-gray-600">
            <input
              ref={inputRef}
              type="text"
              value={message}
              onChange={(e) => setMessage(e.target.value)}
              placeholder="Mesajınızı yazın..."
              className="flex-1 p-4 bg-transparent border-none focus:outline-none dark:text-white placeholder-gray-400 dark:placeholder-gray-500 text-base"
            />
            <button
              type="submit"
              disabled={loading || !message.trim()}
              className="bg-gradient-to-r from-violet-500 to-purple-600 hover:from-violet-600 hover:to-purple-700 text-white px-6 py-4 rounded-xl shadow-lg hover:shadow-xl transition-all duration-300 disabled:opacity-50 disabled:cursor-not-allowed group flex items-center gap-2"
            >
              <span className="hidden sm:inline">Gönder</span>
              <svg 
                xmlns="http://www.w3.org/2000/svg" 
                viewBox="0 0 24 24" 
                fill="currentColor" 
                className="w-6 h-6 transform group-hover:translate-x-1 transition-transform duration-200"
              >
                <path d="M3.478 2.404a.75.75 0 00-.926.941l2.432 7.905H13.5a.75.75 0 010 1.5H4.984l-2.432 7.905a.75.75 0 00.926.94 60.519 60.519 0 0018.445-8.986.75.75 0 000-1.218A60.517 60.517 0 003.478 2.404z" />
              </svg>
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default AIChat;
