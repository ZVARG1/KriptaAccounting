using SQLite;
using System;

public class Contract {
    [PrimaryKey, AutoIncrement] public int ContractID { get; set; }
    public int ClientID { get; set; }
    public string Number { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public string Status { get; set; }
}