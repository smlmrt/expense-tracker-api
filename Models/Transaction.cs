using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApi.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tutar alanı zorunludur.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Tutar 0'dan büyük olmalıdır.")]
        public decimal Amount { get; set; } // İşlemin tutarı (Finansal verilerde her zaman decimal kullanılır)

        [Required(ErrorMessage = "İşlem Tipi zorunludur.")]
        public string Type { get; set; }  // "Income" (Gelir) veya "Expense" (Gider)

        [Required(ErrorMessage = "Kategori zorunludur.")]
        public string Category { get; set; }  // Örn: Market, Maaş, Fatura, Eğitim
        public string Description { get; set; }  // İşlemle ilgili kısa bir açıklama
        public DateTime Sate { get; set; }  // İşlemin gerçekleştiği tarih (Varsayılan olarak o anki zamanı alır)
    }
}