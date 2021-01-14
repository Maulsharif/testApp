using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wallet.Models;
using wallet.Services;

namespace wallet.Controllers
{ 
    [Route("[controller]/[action]")]
    [ApiController]
    public class WalletsController : Controller
    {
      private  static List<Wallet> wallets =new List<Wallet>(){  new Wallet()
          {
              Id = 1, Accounts = new List<Account>()
              {new Account(){Currency = "EUR", Sum = 2000}, 
                  new Account(){Currency = "USD", Sum = 300},
                  new Account(){Currency = "RUB", Sum = 15000} }
          },
          new Wallet()
          {
              Id = 2, Accounts = new List<Account>()
              {new Account(){ Currency = "EUR", Sum = 0}, 
                  new Account(){Currency = "USD", Sum = 3000},
                  new Account(){  Currency = "RUB", Sum = 45000} }
          }};
      
      
        [HttpGet("{id}")]
        public ActionResult<Wallet> Get(int id)
        {
            var res= wallets.FirstOrDefault(p => p.Id == id);

            if (res == null)
            {
                return NotFound();
            }

            return res;
        }
        
        
        [HttpPost("{id}/{sum}/{currency}")]
        public ActionResult Add(int id, decimal sum, int currencyCode)
        {
            var res= wallets.FirstOrDefault(p => p.Id == id)?.Accounts[0];
            if (res == null)
            {
                return NotFound();
            }
        
            res.Sum += sum;
        
            return StatusCode(200, "succsses!");
        }
        
        [HttpPost("{id}/{sum}/{currency}")]
        public ActionResult Withdraw(int id, decimal sum, int currencyCode)
        {
            var res= wallets.FirstOrDefault(p => p.Id == id)?.Accounts[0];
            if (res == null)
            {
                return NotFound();
            }
        
            res.Sum -= sum;
        
            return StatusCode(200, "succsses!");
        }
        
        [HttpPost("{id}/{sum}/{from}/{to}")]
        public ActionResult Transfer(int id, decimal sum, int from,  int to)
        {
            var res= wallets.FirstOrDefault(p => p.Id == id)?.Accounts[0];
            if (res == null)
            {
                return NotFound();
            }

             var data =CurrentRate.GetRateAsync("USD", "RUB");
             RateDeserilizer rate =new RateDeserilizer(data.Result.Content,"USD", "RUB");
             Console.WriteLine( rate.Rate);
          
        
            return StatusCode(200, "succsses!");
        }
        
       
        
        
        
    }
}