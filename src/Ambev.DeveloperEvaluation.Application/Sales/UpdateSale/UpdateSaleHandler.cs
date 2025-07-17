using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Validation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;

        public UpdateSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.Id);
            if (sale == null) return null;

            sale.Customer = request.Customer;
            sale.Branch = request.Branch;

            // Atualiza itens
            sale.Items.Clear();
            foreach (var item in request.Items)
            {
                decimal discount = 0;
                if (item.Quantity > 20)
                    throw new System.Exception("Cannot sell more than 20 identical items.");
                else if (item.Quantity >= 10 && item.Quantity <= 20)
                    discount = item.UnitPrice * item.Quantity * 0.20m;
                else if (item.Quantity >= 4)
                    discount = item.UnitPrice * item.Quantity * 0.10m;

                sale.Items.Add(new Domain.Entities.SaleItem
                {
                    Product = item.Product,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = discount,
                    Total = (item.UnitPrice * item.Quantity) - discount,
                    IsCancelled = false
                });
            }

            sale.TotalAmount = sale.Items.Where(i => !i.IsCancelled).Sum(i => i.Total);

            SaleValidator.Validate(sale);

            await _saleRepository.UpdateAsync(sale);

            return new UpdateSaleResult
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                SaleDate = sale.SaleDate,
                Customer = sale.Customer,
                Branch = sale.Branch,
                TotalAmount = sale.TotalAmount,
                Status = sale.Status.ToString()
            };
        }
    }
}
