# ExpenseTrackerApi – Kişisel Gider Takip API'si

Gelir ve giderlerin kaydedildiği, LINQ ile finansal özet ve kategori bazlı raporlar üreten bir ASP.NET Core Web API projesi.

## Özellikler

- Gelir (`Income`) ve gider (`Expense`) işlemlerini kaydetme
- Toplam gelir, toplam gider ve bakiye özeti
- Giderlerin kategorilere göre gruplanmış raporu
- Data Annotations ile doğrulama (zorunlu alanlar, pozitif tutar kontrolü)

## Teknolojiler

- .NET 10 / ASP.NET Core Web API
- Entity Framework Core 10 (SQLite)
- LINQ
- Swagger (Swashbuckle)

## Veri Modeli (`Transaction`)

| Alan | Tip | Açıklama |
|------|-----|----------|
| `Id` | int | Birincil anahtar |
| `Amount` | decimal | Tutar (zorunlu, > 0) |
| `Type` | string | `Income` veya `Expense` (zorunlu) |
| `Category` | string | Örn. Market, Maaş, Fatura (zorunlu) |
| `Description` | string | Kısa açıklama |
| `Sate` | DateTime | İşlem tarihi |

## API Uç Noktaları

| Metot | Adres | Açıklama |
|-------|-------|----------|
| GET | `/api/Transactions` | Tüm işlemleri listeler |
| GET | `/api/Transactions/{id}` | Tek bir işlemi getirir |
| POST | `/api/Transactions` | Yeni işlem ekler |
| GET | `/api/Transactions/summary` | Toplam gelir, gider ve bakiye |
| GET | `/api/Transactions/by-category` | Kategorilere göre gider toplamları |

Örnek özet yanıtı:

```json
{ "totalIncome": 25000, "totalExpense": 8750.5, "balance": 16249.5 }
```

## Kurulum ve Çalıştırma

```bash
git clone https://github.com/smlmrt/expense-tracker-api.git
cd expense-tracker-api
dotnet restore
dotnet ef database update
dotnet run
```

Uygulama varsayılan olarak `http://localhost:5179` adresinde çalışır. Swagger arayüzü: `http://localhost:5179/swagger`

## Proje Yapısı

```
ExpenseTrackerApi/
├── Controllers/TransactionsController.cs
├── Data/AppDbContext.cs
├── Models/Transaction.cs
├── Migrations/
└── Program.cs
```
