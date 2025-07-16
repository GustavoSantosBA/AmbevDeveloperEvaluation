using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Validation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;

        public CreateSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var items = new List<SaleItem>();
            foreach (var item in request.Items)
            {
                decimal discount = 0;

                if (item.Quantity > 20)
                    throw new Exception("Cannot sell more than 20 identical items.");
                else if (item.Quantity >= 10 && item.Quantity <= 20)
                    discount = item.UnitPrice * item.Quantity * 0.20m;
                else if (item.Quantity >= 4)
                    discount = item.UnitPrice * item.Quantity * 0.10m;

                items.Add(new SaleItem
                {
                    Product = item.Product,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = discount,
                    Total = (item.UnitPrice * item.Quantity) - discount,
                    IsCancelled = false
                });
            }

            var sale = new Sale
            {
                SaleNumber = request.SaleNumber,
                SaleDate = request.SaleDate,
                Customer = request.Customer,
                Branch = request.Branch,
                Items = items,
                TotalAmount = items.Where(i => !i.IsCancelled).Sum(i => i.Total),
                Status = Domain.Enums.SaleStatus.Active
            };

            SaleValidator.Validate(sale);

            await _saleRepository.AddAsync(sale);

            return new CreateSaleResult
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                SaleDate = sale.SaleDate,
                Customer = sale.Customer,
                Branch = sale.Branch,
                TotalAmount = sale.TotalAmount,
                Status = sale.Status.ToString(),
                Items = sale.Items.Select(i => new CreateSaleResultItem
                {
                    Id = i.Id,
                    Product = i.Product,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Discount = i.Discount,
                    Total = i.Total,
                    IsCancelled = i.IsCancelled
                }).ToList()
            };
        }
    }
}
