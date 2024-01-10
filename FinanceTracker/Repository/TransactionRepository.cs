using FinanceTracker.Data;
using FinanceTracker.Data.DbDataContext;
using FinanceTracker.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Security.Principal;

namespace FinanceTracker.Repository
{
    public class TransactionRepository : ITransaction
    {
        private readonly DataContext _context;
        public TransactionRepository(DataContext context)
        {
            _context = context;
        }

        public bool create(Transaction transaction)
        {
            //var model = _context.Accounts.Where(r => r.Id == id).FirstOrDefault();
            //transaction.User = model;
            _context.Transactions.Add(transaction);
            return Save();
        }

        public bool delete(Transaction transaction)
        {
            if (find(transaction.Id))
            {
                var model = GetTransactionById(transaction.Id);
                _context.Transactions.Remove(transaction);

            }

            return Save();
        }

        public bool find(int Id)
        {
            return _context.Transactions.Any(a => a.Id == Id);
        }

        public Transaction Get(Transaction transaction)
        {
            return _context.Transactions.Where(r => r.Name == transaction.Name).FirstOrDefault();
        }

        public ICollection<Transaction> GetAll()
        {

            return _context.Transactions.Include(r=>r.Category).ToList();
        }

        public Transaction GetTransactionById(int transID)
        {
            return _context.Transactions.Where(r => r.Id == transID).FirstOrDefault();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();

            return save > 0 ? true : false;
        }

        public bool update(Transaction transaction)
        {
            
                _context.Transactions.Update(transaction);

            return Save();
        }
    }
}
