﻿using EMaster.Domain.Entities;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Domain.Interfaces.EntityFramework
{
    public interface IExpenseRepo : IGenericRepo<Expense,ExpenseRequest,ExpenseResponse>
    {
        decimal GetTotalExpenseAmount(int companyId);
        decimal MountlyExpenseAmount(int companyId);
        public List<GetExpenseMonthlyCategoryAmount> GetExpenseMonthlyCategoryAmount(int companyId);
    }
}
