namespace BankChurnPredictor.Models
{
    public class CustomerData
    {
        public int CreditScore { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int Tenure { get; set; }
        public decimal Balance { get; set; }
        public int ProductsNumber { get; set; }
        public bool HasCreditCard { get; set; }
        public bool IsActiveMember { get; set; }
        public decimal EstimatedSalary { get; set; }
    }
}
