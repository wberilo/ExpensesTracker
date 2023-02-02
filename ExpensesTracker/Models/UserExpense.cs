namespace ExpensesTracker.Models
{
    public class UserExpense
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public String PlaceOfPurchase { get; set; }
        public Decimal AmountIncludingVAT { get; set; }
        public Decimal VAT { get; set; }
        public String Reason { get; set; }
        public String Members { get; set; }
        public String Comment { get; set; }
        public UserExpense()
        {

        }
    }
}
