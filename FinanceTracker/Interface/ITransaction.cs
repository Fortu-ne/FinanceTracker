﻿using FinanceTracker.Data;

namespace FinanceTracker.Interface
{
    public interface ITransaction
    {
        List<Transaction> GetAll();
        bool create(Transaction transaction);
        bool update(Transaction transaction);
        bool delete(Transaction transaction);

        Transaction Get(Transaction transaction);
        bool find(int Id);
        bool Save();
    }
}