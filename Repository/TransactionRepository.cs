using System.Threading.Tasks;
using Database;
using Domain;
using System.Data;
using Microsoft.EntityFrameworkCore;
namespace Repository;

// This is the class that talks to the database
public class TransactionRepository : ITransactionRepository {
    private MyDbContext _dbContext;

    // This is the database context. It allows us to access the database
    public TransactionRepository(MyDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task CreateTransaction(Transaction trans) {
        trans.TransactionDate = DateTime.Now;
        // Make sure the method that you use is Async
        await _dbContext.Transaction.AddAsync(trans);
        // Make sure to save changes if you add or update the database
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Transaction>> GetAllTransactionsByAccount(Guid accountId) {
        var result = await _dbContext.Transaction.Where((t) => t.UserId.ToString() == accountId.ToString()).ToListAsync();
        return result; 
    }

    public async Task DeleteTransactionsByAccount(int accountId){
        var dbAccount = await _dbContext.Transaction.Where((a) => a.Id == accountId).ToListAsync();
        //throw exception if there isnt any transactions to the account id
        if (dbAccount == null){
            throw new Exception("The Account transaction could not be found");
        }
        //delete all transaction for the account
        foreach (var trans in dbAccount)
        {
            trans.Deleted = 1;
        }
        //save changes
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteTransactionById(int transactionId){
        //get transaction
        var dbTransaction = await _dbContext.Transaction.Where((a) => a.Id == transactionId).FirstOrDefaultAsync();
        //throw exception if its null
        if (dbTransaction == null){
            throw new Exception("The Transaction could not be found");
        }
        // "delete" the transaction
        dbTransaction.Deleted = 1;
        //save changes
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Transaction> GetTransactionById(int transactionId){
        //get transaction
        var dbTransaction = await _dbContext.Transaction.Where((a) => a.Id == transactionId).FirstOrDefaultAsync();
        //throw exception if its null
        if (dbTransaction == null){
            throw new Exception("The Transaction could not be found");
        }
        //return the transaction
        return dbTransaction;
    }

    

}
