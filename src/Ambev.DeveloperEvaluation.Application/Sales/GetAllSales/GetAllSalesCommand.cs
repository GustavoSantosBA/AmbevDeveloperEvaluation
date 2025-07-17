using MediatR;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales
{
    public class GetAllSalesCommand : IRequest<List<GetAllSalesResult>>
    {
    }
}
