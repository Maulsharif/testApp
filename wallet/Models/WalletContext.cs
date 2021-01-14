using Microsoft.EntityFrameworkCore;

namespace wallet.Models
{
    public class WalletContext:DbContext
    {
        public WalletContext(DbContextOptions<WalletContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new {UserId = 1},
                new {UserId = 2});
            
            modelBuilder.Entity<Wallet>().HasData(
                new  {Id =1, UserId=1, Currency="USD", Sum=2000m},
                new  {Id =2, UserId=1, Currency="EUR", Sum=5000m},  
                new  {Id =3, UserId=1, Currency="GBP", Sum=50000m},
                new  {Id =4, UserId=2, Currency="USD", Sum=0m},
                new  {Id =5, UserId=2, Currency="EUR", Sum =20000m},  
                new  {Id =6, UserId=2, Currency="GBP", Sum = 56000m}
                
            );
        }
    }
}