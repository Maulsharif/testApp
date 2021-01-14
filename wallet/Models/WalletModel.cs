using System.Collections.Generic;

namespace wallet.Models
{
    public class WalletModel
    {
        public int  UserId  { get; set; }
        public AccountModel[] AccountModels { get; set; }
    }

    public class AccountModel
    {
        public string  Currency  { get; set; }
        public decimal  Sum  { get; set; }
    }
}