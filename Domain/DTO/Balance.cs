namespace Domain.DTO;

public class Balance {
	public int Amount {get; set;}
	public int AccountId {get; set;}
	public Guid UserId {get; set;}
	public string AccountType {get; set;}
}