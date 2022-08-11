using Domain;
using Domain.DTO;
using System.Threading.Tasks;
namespace Repository;

public interface IAccountRepository {
    Task CreateAccount(Account account);
    Task<List<Account>> GetAccountByUserId(Guid userId);
    Task UpdateAccountBalance(int amount, int accountId);
    Task UpdateAccountData(Account account);
    Task DeleteAccount(int accountId);
    Task TransferBalance(Transfer transfer);
    Task<List<AccountType>> GetAccountTypes();
}