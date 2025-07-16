using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string Customer { get; set; }
        public decimal TotalAmount { get; set; }
        public string Branch { get; set; }
        public SaleStatus Status { get; set; }
        public ICollection<SaleItem> Items { get; set; }

        public Sale()
        {
            Items = new List<SaleItem>();
            Status = SaleStatus.Active;
        }
    }
}
