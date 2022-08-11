using System;
namespace Domain;

public class Transaction {
    public int Id {get; set;}
    public Guid UserId {get; set;}
    public int Amount {get; set;}
    public string TransactionType {get; set;}
    public DateTime TransactionDate {get; set;}
    public int AccountId {get; set;}
    public string AccountName {get; set;}
    public int TransferAccountId {get; set;}
    public string TransferAccountName {get; set;}
    public int Deleted {get; set;}
}