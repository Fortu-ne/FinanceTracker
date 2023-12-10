namespace FinanceTracker.Dto
{
    public class TransactionDto
    {
        public String Name { get; set; }
        public DateTime Date { get; set; }
        public Double Amount { get; set; }
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
       
    }

}
