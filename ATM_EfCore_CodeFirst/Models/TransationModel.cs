namespace ATM_EfCore_CodeFirst.Models
{
    public class TransationModel
    {
        public int Id { get; set; }

        public int AccountNumber { get; set; }

        public DateTime Trans_Time { get; set; }

        public string Amount { get;set; }

       
    }
}
