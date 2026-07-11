using SQLite;
using System;

public class Invoice {
    [PrimaryKey, AutoIncrement] public int InvoiceID { get; set; }
    public int ContractID { get; set; }
    public DateTime DateIssued { get; set; }
    public string Amount { get; set; }
    public string Status { get; set; }
    public string Comment { get; set; }
}