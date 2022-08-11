using System.Threading.Tasks;
using Domain;
namespace Repository;

public interface IAccountTypeRepository {
    Task CreateAccountType(AccountType accountType);
    Task<List<AccountType>> GetAccountType(int accountId);
}