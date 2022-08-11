using System;

namespace Domain;
public class Account {
    public int Id {get; set;}
    public Guid UserId {get; set;}
    public int Type {get; set;}
    public int Balance {get; set;}
    public int Deleted {get; set;}
}