using ExpenseTrackerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        // Veritabanında oluşacak tablomuzun adı "Transactions" olacak
        public DbSet<Transaction> Transactions { get; set; }
    }
}