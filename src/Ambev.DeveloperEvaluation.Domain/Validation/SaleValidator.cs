using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using System.Linq;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public static class SaleValidator
    {
        public static void Validate(Sale sale)
        {
            foreach (var item in sale.Items)
            {
                if (item.Quantity > 20)
                    throw new SaleDomainException("Cannot sell more than 20 identical items.");

                if (item.Quantity >= 10 && item.Quantity <= 20)
                {
                    var expectedDiscount = item.UnitPrice * item.Quantity * 0.20m;
                    if (item.Discount != expectedDiscount)
                        throw new SaleDomainException("Discount for 10-20 items must be 20%.");
                }
                else if (item.Quantity >= 4)
                {
                    var expectedDiscount = item.UnitPrice * item.Quantity * 0.10m;
                    if (item.Discount != expectedDiscount)
                        throw new SaleDomainException("Discount for 4-9 items must be 10%.");
                }
                else if (item.Quantity < 4 && item.Discount > 0)
                {
                    throw new SaleDomainException("Discount not allowed for less than 4 items.");
                }
            }
        }
    }
}
