using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wallet.Models;
using wallet.Services;

namespace wallet.Controllers
{
    [Route("[action]/")]
    [ApiController]
    public class WalletsController : Controller
    {
        private readonly WalletContext _context;
        public WalletsController(WalletContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WalletModel>> Get(int id)
        {
            try
            {
                var list = await _context.Wallets.Where(p => p.UserId == id).ToListAsync();
                return GetModel(list);
            }
            catch (ArgumentOutOfRangeException e)
            {
                return NoContent();
            }
        }

        [HttpPost("{id}/{sum}/{cur}")]
        public async Task<IActionResult> Add(int id, decimal sum, string cur)
        {
            Wallet wallet = _context.Wallets.FirstOrDefault(p => p.UserId == id && p.Currency== cur.ToUpper());
            
            if (wallet == null)
            {
                return BadRequest("Wallet wasn't found");
            }

            wallet.Sum += sum;
            _context.Entry(wallet).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Get", "Wallets", new {id = id});
        }

        [HttpPost("{id}/{sum}/{cur}")]
        public async Task<IActionResult> Withdraw(int id, decimal sum, string cur)
        {
            Wallet wallet = _context.Wallets.FirstOrDefault(p => p.UserId == id &&  p.Currency== cur.ToUpper());
            if (wallet == null)
            {
                return NotFound();
            }

            if (wallet.Sum < sum)
            {
                return BadRequest("Not enough money!");
            }

            wallet.Sum -= sum;
            _context.Entry(wallet).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Get", "Wallets", new {id = id});
        }

        [HttpPost("{id}/{sum}/{from}/{to}")]
        public async Task<IActionResult> Transfer(int id, decimal sum, string from, string to)
        {
            Wallet walletFrom = _context.Wallets.FirstOrDefault(p => p.UserId == id && p.Currency == from.ToUpper());
            Wallet walletTo = _context.Wallets.FirstOrDefault(p => p.UserId == id && p.Currency == to.ToUpper());
            if (walletFrom == null || walletTo == null)
            {
                return BadRequest("User wasn't found!");
            }

            if (walletFrom.Sum < sum)
            {
                return BadRequest("Not enough money!");
            }

            var res = CurrentRate.GetRateAsync(from.ToUpper(), to.ToUpper()).Result.Content;
            Deserializer deserializer = new Deserializer(res, from.ToUpper(), to.ToUpper());

            walletFrom.Sum -= sum;
            walletTo.Sum += sum * deserializer.Rate;
            _context.Entry(walletFrom).State = EntityState.Modified;
            _context.Entry(walletTo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Get", "Wallets", new {id = id});
        }

        public WalletModel GetModel(List<Wallet> wallets)
        {
            WalletModel model = new WalletModel()
                {UserId = wallets[0].UserId, AccountModels = new AccountModel[wallets.Count]};
            for (var i = 0; i < wallets.Count; i++)
            {
                model.AccountModels[i] = new AccountModel() {Currency = wallets[i].Currency, Sum = wallets[i].Sum};
            }
            
            return model;
        }
    }
}