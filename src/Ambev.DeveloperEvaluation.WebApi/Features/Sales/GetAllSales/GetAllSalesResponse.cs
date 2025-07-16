using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales
{
    public class GetAllSalesResponse
    {
        public List<GetAllSalesSaleResponse> Sales { get; set; }
    }

    public class GetAllSalesSaleResponse
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string Customer { get; set; }
        public string Branch { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
}
