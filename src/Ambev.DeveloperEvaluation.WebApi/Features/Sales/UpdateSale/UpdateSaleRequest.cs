using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequest
    {
        public string Customer { get; set; }
        public string Branch { get; set; }
        public List<UpdateSaleItemRequest> Items { get; set; }
    }

    public class UpdateSaleItemRequest
    {
        public Guid? Id { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
