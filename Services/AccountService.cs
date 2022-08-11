using Repository;
using Domain;
using Domain.DTO;
using System.Threading.Tasks;
namespace Services;

public class AccountService : IAccountService {

    private IAccountRepository _accountRepo;
    private ITransactionRepository _transRepo;

    public AccountService(IAccountRepository accountRepo, ITransactionRepository transRepo) {
        _accountRepo = accountRepo;
        _transRepo = transRepo;
    }

    public async Task CreateAccount(Account account) {
        await _accountRepo.CreateAccount(account);
    }

    public async Task<List<Account>> GetAccountByUserId(Guid userId) {
        return await _accountRepo.GetAccountByUserId(userId);
    }

    public async Task UpdateAccountBalance(int amount, int accountId, Guid userId, string accountType) {
        var transType = "Deposit";
        if (amount < 0) {
            transType = "Withdrawl";
        }
        Transaction trans = new Transaction();
        trans.AccountId = accountId;
        trans.Amount = amount;
        trans.AccountName = accountType;
        trans.TransactionType = "";
        trans.TransferAccountId = 0;
        trans.TransferAccountName = transType;
        trans.UserId = userId;

	    await _transRepo.CreateTransaction(trans);
        await _accountRepo.UpdateAccountBalance(amount, accountId);
    }

    public async Task UpdateAccountData(Account account) {
        await _accountRepo.UpdateAccountData(account);
    }
    
    public async Task DeleteAccount(int accountId) {
        await _accountRepo.DeleteAccount(accountId);
    }

    public async Task TransferBalance(Transfer transfer) {
        await _accountRepo.TransferBalance(transfer);
    }

    public async Task<List<AccountType>> GetAccountTypes() {
        return await _accountRepo.GetAccountTypes();
    }
}