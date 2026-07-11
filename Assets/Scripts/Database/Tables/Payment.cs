using SQLite;
using System;

public class Payment {
    [PrimaryKey, AutoIncrement] public int PaymentID { get; set; }
    public int InvoiceID { get; set; }
    public DateTime DatePayment { get; set; }
    public string Amount { get; set; }
    public string Method { get; set; }
    public string Confirmation { get; set; }
}