﻿using Model.EntityModels.ExpenseModels;
using Repository.IBaseRepository;

namespace Repository.IRepositories.IExpenseRepositories
{
    public interface IExpenseRepository : IBaseRepository<Expense>
    {
    }
}
