using Domain;
using System.Threading.Tasks;
namespace Services;

public interface ITransactionService {
    Task CreateTransaction(Transaction trans);
    Task<List<Transaction>> GetAllTransactionsByAccount(Guid accountId);
    Task DeleteTransactionsByAccount(int accountId);
    Task DeleteTransactionById(int transactionId);
    Task<Transaction> GetTransactionById(int transactionId);
}