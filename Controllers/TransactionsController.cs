using ExpenseTrackerApi.Data;
using ExpenseTrackerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        // GET: api/transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // POST: api/transactions
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
        }

        // GET: api/transactions/summary (Finansal Özet Raporu)
        [HttpGet("summary")]
        public async Task<ActionResult<object>> GetSummary()
        {
            // Tüm işlemleri veritabanından çekiyoruz
            var transactions = await _context.Transactions.ToListAsync();

            // LINQ sorguları ile gelirleri ve giderleri ayrı ayrı topluyoruz
            var totalIncome = transactions.Where(t => t.Type == "Income").Sum(t => t.Amount);
            var totalExpense = transactions.Where(t => t.Type == "Expense").Sum(t => t.Amount);

            // Kalan bakiyeyi hesaplıyoruz
            var balance = totalIncome - totalExpense;

            // Özel bir JSON objesi (Anonim nesne) olarak sonucu döndürüyoruz
            return new
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Balance = balance
            };
        }


        // GET: api/transactions/by-category (Kategorilere Göre Gider Raporu)
        [HttpGet("by-category")]
        public async Task<ActionResult<object>> GetExpensesByCategory()
        {
            // Tüm işlemleri veritabanından çekiyoruz
            var transactions = await _context.Transactions.ToListAsync();

            // LINQ ile sadece giderleri filtreleyip, kategorilerine göre grupluyoruz
            var categoryReport = transactions
                .Where(t => t.Type == "Expense")
                .GroupBy(t => t.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    TotalAmount = g.Sum(t => t.Amount)
                })
                .ToList();

            return categoryReport;
        }
    }
}