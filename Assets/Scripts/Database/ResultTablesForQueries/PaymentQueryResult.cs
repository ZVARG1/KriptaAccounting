public class PaymentQueryResult
{
    public int PaymentID { get; set; }

    public int InvoiceID { get; set; }

    public string DatePayment { get; set; }

    public string Amount { get; set; }

    public string Method { get; set; }

    public string Confirmation { get; set; }
}