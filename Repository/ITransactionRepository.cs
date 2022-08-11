using System.Threading.Tasks;
using Domain;
namespace Repository;

public interface ITransactionRepository {
    Task CreateTransaction(Transaction transaction);
    Task<List<Transaction>> GetAllTransactionsByAccount(Guid accountId);
    Task DeleteTransactionsByAccount(int transactionId);
    Task DeleteTransactionById(int transactionId);
    Task<Transaction> GetTransactionById(int transactionId);
}