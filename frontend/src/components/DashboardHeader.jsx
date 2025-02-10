const DashboardHeader = ({ user }) => {
  return (
    <div className="relative overflow-hidden bg-gradient-to-br from-violet-600 via-indigo-600 to-purple-600 rounded-[2rem] shadow-[0_8px_30px_rgb(0,0,0,0.12)] mb-8">
          <div className="absolute inset-0 bg-[url('data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMjAwIiBoZWlnaHQ9IjIwMCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48ZGVmcz48cGF0dGVybiBpZD0iYSIgcGF0dGVyblVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgd2lkdGg9IjQwIiBoZWlnaHQ9IjQwIiBwYXR0ZXJuVHJhbnNmb3JtPSJyb3RhdGUoNDUpIj48cGF0aCBkPSJNLTEwIDMwaDYwViIgZmlsbD0ibm9uZSIgc3Ryb2tlPSIjZmZmIiBzdHJva2Utb3BhY2l0eT0iLjA1IiBzdHJva2Utd2lkdGg9IjEuNSIvPjwvcGF0dGVybj48L2RlZnM+PHJlY3QgZmlsbD0idXJsKCNhKSIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIvPjwvc3ZnPg==')] opacity-50"></div>
          
          <div className="relative z-10 p-8">
            <div className="flex flex-col md:flex-row items-start md:items-center gap-8 mb-8">
              <div className="flex items-center gap-6">
                <div className="relative group">
                  <div className="absolute inset-0 bg-white/30 rounded-full blur-xl transition-all duration-300 group-hover:blur-2xl"></div>
                  <div className="relative h-20 w-20 rounded-full bg-white/10 backdrop-blur-xl flex items-center justify-center border border-white/20 transition-transform duration-300 group-hover:scale-110">
                    <span className="text-3xl font-bold text-white">
                      {user?.firstName?.charAt(0).toUpperCase()}
                    </span>
                  </div>
                </div>
                
                <div className="space-y-1">
                  <div className="flex items-center gap-3">
                    <h1 className="text-4xl font-bold text-white">
                      Welcome back {user?.firstName} {user?.lastName},
                    </h1>
                  </div>
                  <p className="text-lg text-indigo-100 font-medium">
                    Here's your financial overview for today
                  </p>
                </div>
              </div>
            </div>
          </div>
        </div>
  );
};

export default DashboardHeader;
