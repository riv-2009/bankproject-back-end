using System.Threading.Tasks;
using Domain;
using Domain.DTO;
using Repository;

namespace Services;

// The service class is where business logic is taken care of. Any sort of computation or anything else
public class TransactionService : ITransactionService {

    private ITransactionRepository _transRepo;
    // haven't created this yet
    private IAccountRepository _accountRepo;

    // not you are able to inject more than one Repository into a Service
    public TransactionService(ITransactionRepository transRepo, IAccountRepository accountRepo) {
        _transRepo = transRepo;
        _accountRepo = accountRepo;
    }

    // Note this is the same as the interface. If we didn't use the interface we couldn't use DI (dependency injection)
    public async Task CreateTransaction(Transaction trans) {
        // update balance
        //I will fix that this doesn't have amount. But notice that this is where we can call the repository of other verticals.
        Transfer transfer = new Transfer();
        transfer.Amount = trans.Amount;
        transfer.ToAccount = trans.TransferAccountId;
        transfer.AccountId = trans.AccountId;
        await _accountRepo.TransferBalance(transfer);

        // create transaction record
        await _transRepo.CreateTransaction(trans);
    }

    public async Task<List<Transaction>> GetAllTransactionsByAccount(Guid accountId) {
        return await _transRepo.GetAllTransactionsByAccount(accountId);
    }
    public async Task DeleteTransactionsByAccount(int accountId){
        await _transRepo.DeleteTransactionsByAccount(accountId);
    }
    public async Task DeleteTransactionById(int transactionId){
        await _transRepo.DeleteTransactionById(transactionId);
    }
    public async Task<Transaction> GetTransactionById(int transactionId){
        return await _transRepo.GetTransactionById(transactionId);
    }

}