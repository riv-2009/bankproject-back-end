using System.Threading.Tasks;
using Domain;
using Domain.DTO;
namespace Services;

public interface IAccountService {
    Task CreateAccount(Account account);
    Task<List<Account>> GetAccountByUserId(Guid userId);
    Task UpdateAccountBalance(int amount, int accountId, Guid userId, string accountType);
    Task UpdateAccountData(Account account);
    Task DeleteAccount(int accountId);
    Task TransferBalance(Transfer transfer);
    Task<List<AccountType>> GetAccountTypes();
}