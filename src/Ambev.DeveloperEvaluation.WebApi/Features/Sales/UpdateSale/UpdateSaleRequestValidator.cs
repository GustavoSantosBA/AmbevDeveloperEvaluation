﻿using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
    {
        public UpdateSaleRequestValidator()
        {
            RuleFor(x => x.Customer).NotEmpty();
            RuleFor(x => x.Branch).NotEmpty();
            RuleFor(x => x.Items).NotEmpty();
            RuleForEach(x => x.Items).SetValidator(new UpdateSaleItemRequestValidator());
        }
    }

    public class UpdateSaleItemRequestValidator : AbstractValidator<UpdateSaleItemRequest>
    {
        public UpdateSaleItemRequestValidator()
        {
            RuleFor(x => x.Product).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.UnitPrice).GreaterThan(0);
        }
    }
}
