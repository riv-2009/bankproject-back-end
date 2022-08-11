using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;
using Domain;
using Domain.DTO;
namespace Repository;

public class AccountRepository : IAccountRepository {

    private MyDbContext _dbContext;

    public AccountRepository(MyDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task CreateAccount(Account account) {
        await _dbContext.Account.AddAsync(account);
        await _dbContext.SaveChangesAsync(); 
    }

    public async Task<List<Account>> GetAccountByUserId(Guid userId) {
        return await _dbContext.Account.Where((a) => a.UserId.ToString() == userId.ToString() && a.Deleted == 0).ToListAsync();
    }

    public async Task UpdateAccountBalance(int amount, int accountId) {
        var account = await _dbContext.Account.Where((a) => a.Id == accountId && a.Deleted == 0).FirstOrDefaultAsync();
        if (account == null) {
            throw new Exception("The account could not be found");
        }

        account.Balance += amount;
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAccountData(Account account) 
    {
        var dbAccount = await _dbContext.Account.Where((a) => a.Id == account.Id && a.Deleted == 0).FirstOrDefaultAsync();
        if (dbAccount == null) {
            throw new Exception("The Account coudl not be found");
        }
        dbAccount.Type = account.Type;
        dbAccount.Balance = account.Balance;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAccount(int accountId) {
        var dbAccount = await _dbContext.Account.Where((a) => a.Id == accountId).FirstOrDefaultAsync();
        if (dbAccount == null) {
            throw new Exception("The Account could not be found");
        }
        dbAccount.Deleted = 1;
        await _dbContext.SaveChangesAsync();
    }

    public async Task TransferBalance(Transfer transfer) {
        var fromAccount = await _dbContext.Account.Where((a) => a.Id == transfer.AccountId).FirstOrDefaultAsync();
        var toAccount = await _dbContext.Account.Where((a) => a.Id == transfer.ToAccount).FirstOrDefaultAsync();
        fromAccount.Balance = fromAccount.Balance - transfer.Amount;
        toAccount.Balance = toAccount.Balance + transfer.Amount;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<AccountType>> GetAccountTypes() {
        return await _dbContext.Type.ToListAsync();
    }
}