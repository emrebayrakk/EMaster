const Spinner = () => {
    return (
      <div className="flex justify-center items-center mt-10">
        <div className="animate-spin rounded-full h-12 w-12 border-t-4 border-b-4 border-blue-500"></div>
        <span className="ml-4 text-xl text-blue-500">Loading...</span>
      </div>
    );
  };
  
  export default Spinner;