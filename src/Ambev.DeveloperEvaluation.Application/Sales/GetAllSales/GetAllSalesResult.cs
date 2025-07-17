using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

public class GetAllSalesResult
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public string Customer { get; set; }
    public string Branch { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
    public List<SaleItem> Items { get; set; }
}

