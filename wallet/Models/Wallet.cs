using System.ComponentModel.DataAnnotations.Schema;

namespace wallet.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public decimal Sum { get; set; } = 0;
        public int UserId { get; set; }
        public User User{ get; set; }
    }
}